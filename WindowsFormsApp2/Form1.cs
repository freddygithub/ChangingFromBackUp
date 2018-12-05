using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        string IP = null, cName = null, cNameRaw = null;
        DirectoryInfo target = new DirectoryInfo("C:\\TompkinsRobotics");

        public Form1()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            upper.MouseEnter += OnMouseEnterButton1;
            upper.MouseLeave += OnMouseLeaveButton1;
            lower.MouseEnter += OnMouseEnterButton2;
            lower.MouseLeave += OnMouseLeaveButton2;
            spare.MouseEnter += OnMouseEnterButton3;
            spare.MouseLeave += OnMouseLeaveButton3;

            setup();
        }

        public void setup()
        {
            try
            {
                IP = GetLocalIPAddress();

                for (int i = (IP.Length - 1); i > 0; i--)
                {
                    if (IP[i] == '.')
                        break;

                    IP = IP.Remove(IP.Length - 1);
                }

                cName = System.Environment.MachineName;
                cNameRaw = cName;
                if (cName.Contains("-SP"))
                {
                    cName = cName.Remove(cName.Length - 1, 1);
                    cName = cName.Remove(cName.Length - 1, 1);
                }
                else
                {
                    cName = cName.Remove(cName.Length - 1, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public void SetIP(string ipAddress, string subnetMask, string gateway)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(managementObject => (bool)managementObject["IPEnabled"]))
                    {
                        using (var newIP = managementObject.GetMethodParameters("EnableStatic"))
                        {
                            // Set new IP address and subnet if needed
                            if ((!String.IsNullOrEmpty(ipAddress)) || (!String.IsNullOrEmpty(subnetMask)))
                            {
                                if (!String.IsNullOrEmpty(ipAddress))
                                {
                                    newIP["IPAddress"] = new[] { ipAddress };
                                }

                                if (!String.IsNullOrEmpty(subnetMask))
                                {
                                    newIP["SubnetMask"] = new[] { subnetMask };
                                }

                                managementObject.InvokeMethod("EnableStatic", newIP, null);
                            }

                            // Set mew gateway if needed
                            if (!String.IsNullOrEmpty(gateway))
                            {
                                using (var newGateway = managementObject.GetMethodParameters("SetGateways"))
                                {
                                    newGateway["DefaultIPGateway"] = new[] { gateway };
                                    newGateway["GatewayCostMetric"] = new[] { 1 };
                                    managementObject.InvokeMethod("SetGateways", newGateway, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public static bool SetMachineName(string newName)
        {
            RegistryKey key = Registry.LocalMachine;

            string activeComputerName = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ActiveComputerName";
            RegistryKey activeCmpName = key.CreateSubKey(activeComputerName);
            activeCmpName.SetValue("ComputerName", newName);
            activeCmpName.Close();
            string computerName = "SYSTEM\\CurrentControlSet\\Control\\ComputerName\\ComputerName";
            RegistryKey cmpName = key.CreateSubKey(computerName);
            cmpName.SetValue("ComputerName", newName);
            cmpName.Close();
            string _hostName = "SYSTEM\\CurrentControlSet\\services\\Tcpip\\Parameters\\";
            RegistryKey hostName = key.CreateSubKey(_hostName);
            hostName.SetValue("Hostname", newName);
            hostName.SetValue("NV Hostname", newName);
            hostName.Close();
            return true;
        }

        public void logIT(string broke, string changed)
        {
            using (var tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"Log.txt", true))
            {
                tw.WriteLine(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz") + "A Backup Switch happened: the computer "+
                    changed + ", changed to computer " + broke + ".");
            }
        }

        //Upper
        private void upper_Click(object sender, EventArgs e)
        {

            if (PingHost(IP + "101"))
            {
                MessageBox.Show("That IP is already in use. I was able to Ping: " + IP + "101.",
                    "Error: Network conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetIP((IP + "101"), "255.255.0.0", "192.168.2.1");
                var source = new DirectoryInfo("C:\\TompkinsRoboticsUpperBackup");
                CopyFilesRecursively(source, target);
                SetMachineName(cName + "U");

                logIT("UPPER", "SPARE");

                MessageBox.Show("Please Restart Computer",
                    "Message Important", MessageBoxButtons.OK);

                System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            }
        }
        private void OnMouseEnterButton1(object sender, EventArgs e)
        {
            upper.BackColor = SystemColors.ButtonHighlight;
        }
        private void OnMouseLeaveButton1(object sender, EventArgs e)
        {
            upper.BackColor = Color.Blue;
        }

        //Lower
        private void lower_Click(object sender, EventArgs e)
        {
            if (PingHost(IP + "102"))
            {
                MessageBox.Show("That IP is already in use. I was able to Ping: " + IP + "102.",
                    "Error: Network conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetIP((IP + "102"), "255.255.0.0", "192.168.2.1");
                var source = new DirectoryInfo("C:\\TompkinsRoboticsLowerBackup");
                CopyFilesRecursively(source, target);
                SetMachineName(cName + "L");

                logIT("LOWER", "SPARE");

                System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            }
        }
        private void OnMouseEnterButton2(object sender, EventArgs e)
        {
            lower.BackColor = SystemColors.ButtonHighlight;
        }
        private void OnMouseLeaveButton2(object sender, EventArgs e)
        {
            lower.BackColor = Color.OrangeRed;
        }

        //Spare
        private void spare_Click(object sender, EventArgs e)
        {
            if (PingHost(IP + "103"))
            {
                MessageBox.Show("That IP is already in use. I was able to Ping: " + IP + "103.",
                    "Error: Network conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetIP((IP + "103"), "255.255.0.0", "192.168.2.1");

                logIT(cNameRaw, "SPARE");

                SetMachineName(cName + "SP");

                System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            }
        }
        private void OnMouseEnterButton3(object sender, EventArgs e)
        {
            spare.BackColor = Color.Gray;
        }
        private void OnMouseLeaveButton3(object sender, EventArgs e)
        {
            spare.BackColor = Color.White;
        }
    }
}
