using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace NetworkAdapterManager
{
    public partial class MainForm : Form
    {
        private Dictionary<string, NetworkInterface> adapters;
        private Dictionary<string, List<AdapterConfiguration>> adapterConfigurations;
        private static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NetworkAdapterManager");

        public MainForm()
        {
            InitializeComponent();
            LoadNetworkAdapters();
            LoadConfigurations();
            SetEditMode(false);
        }

        private void LoadNetworkAdapters()
        {
            adapters = NetworkInterface.GetAllNetworkInterfaces()
                .ToDictionary(adapter => adapter.Name, adapter => adapter);

            comboBoxAdapters.Items.AddRange(adapters.Keys.ToArray());
        }

        private void LoadConfigurations()
        {
            adapterConfigurations = new Dictionary<string, List<AdapterConfiguration>>();

            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }

            foreach (var adapter in adapters.Keys)
            {
                var configFilePath = Path.Combine(ConfigDirectory, adapter + ".xml");
                if (File.Exists(configFilePath))
                {
                    using (var stream = new FileStream(configFilePath, FileMode.Open))
                    {
                        var serializer = new XmlSerializer(typeof(List<AdapterConfiguration>));
                        var configurations = (List<AdapterConfiguration>)serializer.Deserialize(stream);
                        adapterConfigurations[adapter] = configurations;
                    }
                }
                else
                {
                    adapterConfigurations[adapter] = new List<AdapterConfiguration>();
                }
            }
        }

        private void SaveConfigurations(string adapterName)
        {
            var configFilePath = Path.Combine(ConfigDirectory, adapterName + ".xml");
            using (var stream = new FileStream(configFilePath, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(List<AdapterConfiguration>));
                serializer.Serialize(stream, adapterConfigurations[adapterName]);
            }
        }

        private void comboBoxAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                var properties = adapter.GetIPProperties();
                var ipv4Properties = properties.UnicastAddresses
                    .FirstOrDefault(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                if (ipv4Properties != null)
                {
                    textBoxIPAddress.Text = ipv4Properties.Address.ToString();
                    textBoxSubnetMask.Text = ipv4Properties.IPv4Mask.ToString();
                    textBoxGateway.Text = properties.GatewayAddresses.FirstOrDefault()?.Address.ToString();
                }

                // Load saved configurations
                listBoxConfigurations.Items.Clear();
                if (adapterConfigurations.TryGetValue(adapterName, out var configurations))
                {
                    foreach (var config in configurations)
                    {
                        listBoxConfigurations.Items.Add(config.Name);
                    }
                }
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                var configName = textBoxConfigName.Text;
                if (string.IsNullOrWhiteSpace(configName))
                {
                    MessageBox.Show("Please enter a configuration name.");
                    return;
                }

                var newConfig = new AdapterConfiguration
                {
                    Name = configName,
                    IPAddress = textBoxIPAddress.Text,
                    SubnetMask = textBoxSubnetMask.Text,
                    Gateway = textBoxGateway.Text
                };

                if (!adapterConfigurations.ContainsKey(adapterName))
                {
                    adapterConfigurations[adapterName] = new List<AdapterConfiguration>();
                }

                var existingConfig = adapterConfigurations[adapterName].FirstOrDefault(c => c.Name == configName);
                if (existingConfig != null)
                {
                    adapterConfigurations[adapterName].Remove(existingConfig);
                }

                if (adapterConfigurations[adapterName].Count >= 4)
                {
                    MessageBox.Show("You can only save up to 4 configurations per adapter.");
                    return;
                }

                adapterConfigurations[adapterName].Add(newConfig);
                SaveConfigurations(adapterName);

                listBoxConfigurations.Items.Clear();
                foreach (var config in adapterConfigurations[adapterName])
                {
                    listBoxConfigurations.Items.Add(config.Name);
                }
            }
        }

        private void buttonApplySettings_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                if (listBoxConfigurations.SelectedItem is string configName)
                {
                    var config = adapterConfigurations[adapterName].FirstOrDefault(c => c.Name == configName);
                    if (config != null)
                    {
                        ApplyNetworkSettings(adapterName, config);
                    }
                }
            }
        }

        private void buttonLoadConfig_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                if (listBoxConfigurations.SelectedItem is string configName)
                {
                    var config = adapterConfigurations[adapterName].FirstOrDefault(c => c.Name == configName);
                    if (config != null)
                    {
                        textBoxIPAddress.Text = config.IPAddress;
                        textBoxSubnetMask.Text = config.SubnetMask;
                        textBoxGateway.Text = config.Gateway;
                    }
                }
            }
        }

        private void buttonActivateConfig_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                if (listBoxConfigurations.SelectedItem is string configName)
                {
                    var config = adapterConfigurations[adapterName].FirstOrDefault(c => c.Name == configName);
                    if (config != null)
                    {
                        ApplyNetworkSettings(adapterName, config);
                    }
                }
            }
        }

        private void buttonEnableEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        private void buttonDeleteConfig_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName && adapters.TryGetValue(adapterName, out var adapter))
            {
                if (listBoxConfigurations.SelectedItem is string configName)
                {
                    var config = adapterConfigurations[adapterName].FirstOrDefault(c => c.Name == configName);
                    if (config != null)
                    {
                        adapterConfigurations[adapterName].Remove(config);
                        SaveConfigurations(adapterName);

                        listBoxConfigurations.Items.Clear();
                        foreach (var cfg in adapterConfigurations[adapterName])
                        {
                            listBoxConfigurations.Items.Add(cfg.Name);
                        }
                    }
                }
            }
        }

        private void buttonSetDHCP_Click(object sender, EventArgs e)
        {
            if (comboBoxAdapters.SelectedItem is string adapterName)
            {
                SetDHCP(adapterName);
            }
        }

        private void SetEditMode(bool enabled)
        {
            textBoxIPAddress.ReadOnly = !enabled;
            textBoxSubnetMask.ReadOnly = !enabled;
            textBoxGateway.ReadOnly = !enabled;
            buttonSaveSettings.Enabled = enabled;
            buttonDeleteConfig.Enabled = enabled;
        }

        private void ApplyNetworkSettings(string adapterName, AdapterConfiguration config)
        {
            string arguments = $"interface ip set address \"{adapterName}\" static {config.IPAddress} {config.SubnetMask} {config.Gateway}";
            ProcessStartInfo psi = new ProcessStartInfo("netsh", arguments)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Verb = "runas" // Ensure the process runs with admin privileges
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("Network settings applied successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to apply network settings.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void SetDHCP(string adapterName)
        {
            string arguments = $"interface ip set address \"{adapterName}\" dhcp";
            ProcessStartInfo psi = new ProcessStartInfo("netsh", arguments)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Verb = "runas" // Ensure the process runs with admin privileges
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("DHCP settings applied successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to apply DHCP settings.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }

    [Serializable]
    public class AdapterConfiguration
    {
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public string SubnetMask { get; set; }
        public string Gateway { get; set; }
    }
}
