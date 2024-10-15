namespace NetworkAdapterManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxAdapters;
        private TextBox textBoxIPAddress;
        private TextBox textBoxSubnetMask;
        private TextBox textBoxGateway;
        private TextBox textBoxConfigName;
        private ListBox listBoxConfigurations;
        private Button buttonSaveSettings;
        private Button buttonApplySettings;
        private Button buttonLoadConfig;
        private Button buttonActivateConfig;
        private Button buttonEnableEdit;
        private Button buttonDeleteConfig;
        private Button buttonSetDHCP;
        private Label CC;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            comboBoxAdapters = new ComboBox();
            textBoxIPAddress = new TextBox();
            textBoxSubnetMask = new TextBox();
            textBoxGateway = new TextBox();
            textBoxConfigName = new TextBox();
            listBoxConfigurations = new ListBox();
            buttonSaveSettings = new Button();
            buttonApplySettings = new Button();
            buttonLoadConfig = new Button();
            buttonActivateConfig = new Button();
            buttonEnableEdit = new Button();
            buttonDeleteConfig = new Button();
            buttonSetDHCP = new Button();
            SuspendLayout();
            // 
            // comboBoxAdapters
            // 
            comboBoxAdapters.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAdapters.FormattingEnabled = true;
            comboBoxAdapters.Location = new Point(12, 12);
            comboBoxAdapters.Name = "comboBoxAdapters";
            comboBoxAdapters.Size = new Size(260, 23);
            comboBoxAdapters.TabIndex = 0;
            comboBoxAdapters.SelectedIndexChanged += comboBoxAdapters_SelectedIndexChanged;
            // 
            // textBoxIPAddress
            // 
            textBoxIPAddress.Location = new Point(12, 50);
            textBoxIPAddress.Name = "textBoxIPAddress";
            textBoxIPAddress.PlaceholderText = "IP Address";
            textBoxIPAddress.Size = new Size(260, 23);
            textBoxIPAddress.TabIndex = 1;
            // 
            // textBoxSubnetMask
            // 
            textBoxSubnetMask.Location = new Point(12, 90);
            textBoxSubnetMask.Name = "textBoxSubnetMask";
            textBoxSubnetMask.PlaceholderText = "Subnet Mask";
            textBoxSubnetMask.Size = new Size(260, 23);
            textBoxSubnetMask.TabIndex = 2;
            // 
            // textBoxGateway
            // 
            textBoxGateway.Location = new Point(12, 130);
            textBoxGateway.Name = "textBoxGateway";
            textBoxGateway.PlaceholderText = "Gateway";
            textBoxGateway.Size = new Size(260, 23);
            textBoxGateway.TabIndex = 3;
            // 
            // textBoxConfigName
            // 
            textBoxConfigName.Location = new Point(12, 170);
            textBoxConfigName.Name = "textBoxConfigName";
            textBoxConfigName.PlaceholderText = "Configuration Name";
            textBoxConfigName.Size = new Size(260, 23);
            textBoxConfigName.TabIndex = 4;
            // 
            // listBoxConfigurations
            // 
            listBoxConfigurations.FormattingEnabled = true;
            listBoxConfigurations.ItemHeight = 15;
            listBoxConfigurations.Location = new Point(12, 210);
            listBoxConfigurations.Name = "listBoxConfigurations";
            listBoxConfigurations.Size = new Size(260, 94);
            listBoxConfigurations.TabIndex = 5;
            // 
            // buttonSaveSettings
            // 
            buttonSaveSettings.Location = new Point(12, 320);
            buttonSaveSettings.Name = "buttonSaveSettings";
            buttonSaveSettings.Size = new Size(120, 23);
            buttonSaveSettings.TabIndex = 6;
            buttonSaveSettings.Text = "Save Settings";
            buttonSaveSettings.UseVisualStyleBackColor = true;
            buttonSaveSettings.Click += buttonSaveSettings_Click;
            // 
            // buttonApplySettings
            // 
            buttonApplySettings.Location = new Point(152, 320);
            buttonApplySettings.Name = "buttonApplySettings";
            buttonApplySettings.Size = new Size(120, 23);
            buttonApplySettings.TabIndex = 7;
            buttonApplySettings.Text = "Apply Settings";
            buttonApplySettings.UseVisualStyleBackColor = true;
            buttonApplySettings.Click += buttonApplySettings_Click;
            // 
            // buttonLoadConfig
            // 
            buttonLoadConfig.Location = new Point(12, 350);
            buttonLoadConfig.Name = "buttonLoadConfig";
            buttonLoadConfig.Size = new Size(120, 23);
            buttonLoadConfig.TabIndex = 8;
            buttonLoadConfig.Text = "Load Config";
            buttonLoadConfig.UseVisualStyleBackColor = true;
            buttonLoadConfig.Click += buttonLoadConfig_Click;
            // 
            // buttonActivateConfig
            // 
            buttonActivateConfig.Location = new Point(152, 350);
            buttonActivateConfig.Name = "buttonActivateConfig";
            buttonActivateConfig.Size = new Size(120, 23);
            buttonActivateConfig.TabIndex = 9;
            buttonActivateConfig.Text = "Activate Config";
            buttonActivateConfig.UseVisualStyleBackColor = true;
            buttonActivateConfig.Click += buttonActivateConfig_Click;
            // 
            // buttonEnableEdit
            // 
            buttonEnableEdit.Location = new Point(12, 380);
            buttonEnableEdit.Name = "buttonEnableEdit";
            buttonEnableEdit.Size = new Size(120, 23);
            buttonEnableEdit.TabIndex = 10;
            buttonEnableEdit.Text = "Enable Edit";
            buttonEnableEdit.UseVisualStyleBackColor = true;
            buttonEnableEdit.Click += buttonEnableEdit_Click;
            // 
            // buttonDeleteConfig
            // 
            buttonDeleteConfig.Location = new Point(152, 379);
            buttonDeleteConfig.Name = "buttonDeleteConfig";
            buttonDeleteConfig.Size = new Size(120, 23);
            buttonDeleteConfig.TabIndex = 11;
            buttonDeleteConfig.Text = "Delete Config";
            buttonDeleteConfig.UseVisualStyleBackColor = true;
            buttonDeleteConfig.Click += buttonDeleteConfig_Click;
            // 
            // buttonSetDHCP
            // 
            buttonSetDHCP.Location = new Point(12, 409);
            buttonSetDHCP.Name = "buttonSetDHCP";
            buttonSetDHCP.Size = new Size(120, 23);
            buttonSetDHCP.TabIndex = 12;
            buttonSetDHCP.Text = "Set DHCP";
            buttonSetDHCP.UseVisualStyleBackColor = true;
            buttonSetDHCP.Click += buttonSetDHCP_Click;
            // 
            // button1
            // 
            CC.Location = new Point(100, 438);
            CC.Name = "button1";
            CC.Size = new Size(75, 23);
            CC.TabIndex = 0;
            CC.Text = "Set DHCP";
            // 
            // MainForm
            // 
            ClientSize = new Size(284, 485);
            Controls.Add(comboBoxAdapters);
            Controls.Add(textBoxIPAddress);
            Controls.Add(textBoxSubnetMask);
            Controls.Add(textBoxGateway);
            Controls.Add(textBoxConfigName);
            Controls.Add(listBoxConfigurations);
            Controls.Add(buttonSaveSettings);
            Controls.Add(buttonApplySettings);
            Controls.Add(buttonLoadConfig);
            Controls.Add(buttonActivateConfig);
            Controls.Add(buttonEnableEdit);
            Controls.Add(buttonDeleteConfig);
            Controls.Add(buttonSetDHCP);
            Name = "MainForm";
            Text = "Network Adapter Manager";
            ResumeLayout(false);
            PerformLayout();
        }

     
    }
}
