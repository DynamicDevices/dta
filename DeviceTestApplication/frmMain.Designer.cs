namespace DeviceTestApplication
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.labelCompanyTitle = new System.Windows.Forms.Label();
            this.labelTesterTitle = new System.Windows.Forms.Label();
            this.labelAgency = new System.Windows.Forms.Label();
            this.labelTester = new System.Windows.Forms.Label();
            this.labelSelectDeviceClass = new System.Windows.Forms.Label();
            this.comboBoxDeviceClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIPAddress = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.buttonTestForDevice = new System.Windows.Forms.Button();
            this.labelIPAddress = new System.Windows.Forms.Label();
            this.labelSSHlogin = new System.Windows.Forms.Label();
            this.textBoxSSHLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSSHPassword = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.comboBoxTestLocation = new System.Windows.Forms.ComboBox();
            this.pictureBoxTestLocation = new System.Windows.Forms.PictureBox();
            this.pictureBoxDeviceClasses = new System.Windows.Forms.PictureBox();
            this.pictureBoxPing = new System.Windows.Forms.PictureBox();
            this.timerTestComms = new System.Windows.Forms.Timer(this.components);
            this.labelSerialNumber = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEndUserSerial = new System.Windows.Forms.TextBox();
            this.labelRunTest = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.pictureBoxSerials = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Order = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testListDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Execute = new System.Windows.Forms.DataGridViewButtonColumn();
            this.testItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.labelTestListTitle = new System.Windows.Forms.Label();
            this.labelTestList = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.pictureBoxRunTest = new System.Windows.Forms.PictureBox();
            this.labelTestLog = new System.Windows.Forms.Label();
            this.labelResultTitle = new System.Windows.Forms.Label();
            this.pictureBoxTestResult = new System.Windows.Forms.PictureBox();
            this.labelTestResultText1 = new System.Windows.Forms.Label();
            this.labelTestResultText2 = new System.Windows.Forms.Label();
            this.backgroundWorkerTest = new System.ComponentModel.BackgroundWorker();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useDevDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxProducerSerial = new System.Windows.Forms.TextBox();
            this.labelProviderSerial = new System.Windows.Forms.Label();
            this.buttonCheckSerial = new System.Windows.Forms.Button();
            this.checkBoxLocalTest = new System.Windows.Forms.CheckBox();
            this.clearCacheFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTestLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeviceClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSerials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testItemBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRunTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTestResult)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCompanyTitle
            // 
            this.labelCompanyTitle.AutoSize = true;
            this.labelCompanyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompanyTitle.Location = new System.Drawing.Point(12, 39);
            this.labelCompanyTitle.Name = "labelCompanyTitle";
            this.labelCompanyTitle.Size = new System.Drawing.Size(99, 13);
            this.labelCompanyTitle.TabIndex = 0;
            this.labelCompanyTitle.Text = "Testing Agency:";
            // 
            // labelTesterTitle
            // 
            this.labelTesterTitle.AutoSize = true;
            this.labelTesterTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTesterTitle.Location = new System.Drawing.Point(12, 77);
            this.labelTesterTitle.Name = "labelTesterTitle";
            this.labelTesterTitle.Size = new System.Drawing.Size(47, 13);
            this.labelTesterTitle.TabIndex = 1;
            this.labelTesterTitle.Text = "Tester:";
            // 
            // labelAgency
            // 
            this.labelAgency.AutoSize = true;
            this.labelAgency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAgency.Location = new System.Drawing.Point(134, 39);
            this.labelAgency.Name = "labelAgency";
            this.labelAgency.Size = new System.Drawing.Size(111, 16);
            this.labelAgency.TabIndex = 2;
            this.labelAgency.Text = "Default Company";
            // 
            // labelTester
            // 
            this.labelTester.AutoSize = true;
            this.labelTester.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTester.Location = new System.Drawing.Point(134, 77);
            this.labelTester.Name = "labelTester";
            this.labelTester.Size = new System.Drawing.Size(82, 16);
            this.labelTester.TabIndex = 3;
            this.labelTester.Text = "Default User";
            // 
            // labelSelectDeviceClass
            // 
            this.labelSelectDeviceClass.AutoSize = true;
            this.labelSelectDeviceClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectDeviceClass.Location = new System.Drawing.Point(12, 172);
            this.labelSelectDeviceClass.Name = "labelSelectDeviceClass";
            this.labelSelectDeviceClass.Size = new System.Drawing.Size(164, 16);
            this.labelSelectDeviceClass.TabIndex = 4;
            this.labelSelectDeviceClass.Text = "2. Select Device Class";
            // 
            // comboBoxDeviceClass
            // 
            this.comboBoxDeviceClass.FormattingEnabled = true;
            this.comboBoxDeviceClass.Location = new System.Drawing.Point(15, 205);
            this.comboBoxDeviceClass.Name = "comboBoxDeviceClass";
            this.comboBoxDeviceClass.Size = new System.Drawing.Size(235, 21);
            this.comboBoxDeviceClass.TabIndex = 5;
            this.comboBoxDeviceClass.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDeviceClassSelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "3. Select Device IP Details and Test";
            // 
            // textBoxIPAddress
            // 
            this.textBoxIPAddress.Location = new System.Drawing.Point(15, 294);
            this.textBoxIPAddress.Name = "textBoxIPAddress";
            this.textBoxIPAddress.Size = new System.Drawing.Size(235, 20);
            this.textBoxIPAddress.TabIndex = 7;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tick");
            this.imageList1.Images.SetKeyName(1, "cross");
            this.imageList1.Images.SetKeyName(2, "alert");
            // 
            // buttonTestForDevice
            // 
            this.buttonTestForDevice.Location = new System.Drawing.Point(15, 398);
            this.buttonTestForDevice.Name = "buttonTestForDevice";
            this.buttonTestForDevice.Size = new System.Drawing.Size(119, 23);
            this.buttonTestForDevice.TabIndex = 14;
            this.buttonTestForDevice.Text = "Test Connection";
            this.buttonTestForDevice.UseVisualStyleBackColor = true;
            this.buttonTestForDevice.Click += new System.EventHandler(this.ButtonTestForDeviceClick);
            // 
            // labelIPAddress
            // 
            this.labelIPAddress.AutoSize = true;
            this.labelIPAddress.Location = new System.Drawing.Point(12, 278);
            this.labelIPAddress.Name = "labelIPAddress";
            this.labelIPAddress.Size = new System.Drawing.Size(58, 13);
            this.labelIPAddress.TabIndex = 0;
            this.labelIPAddress.Text = "IP Address";
            // 
            // labelSSHlogin
            // 
            this.labelSSHlogin.AutoSize = true;
            this.labelSSHlogin.Location = new System.Drawing.Point(12, 317);
            this.labelSSHlogin.Name = "labelSSHlogin";
            this.labelSSHlogin.Size = new System.Drawing.Size(77, 13);
            this.labelSSHlogin.TabIndex = 0;
            this.labelSSHlogin.Text = "SSH user login";
            // 
            // textBoxSSHLogin
            // 
            this.textBoxSSHLogin.Location = new System.Drawing.Point(15, 333);
            this.textBoxSSHLogin.Name = "textBoxSSHLogin";
            this.textBoxSSHLogin.Size = new System.Drawing.Size(235, 20);
            this.textBoxSSHLogin.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "SSH user password";
            // 
            // textBoxSSHPassword
            // 
            this.textBoxSSHPassword.Location = new System.Drawing.Point(15, 372);
            this.textBoxSSHPassword.Name = "textBoxSSHPassword";
            this.textBoxSSHPassword.Size = new System.Drawing.Size(235, 20);
            this.textBoxSSHPassword.TabIndex = 11;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLocation.Location = new System.Drawing.Point(12, 107);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(166, 16);
            this.labelLocation.TabIndex = 17;
            this.labelLocation.Text = "1. Select Test Location";
            // 
            // comboBoxTestLocation
            // 
            this.comboBoxTestLocation.FormattingEnabled = true;
            this.comboBoxTestLocation.Location = new System.Drawing.Point(15, 126);
            this.comboBoxTestLocation.Name = "comboBoxTestLocation";
            this.comboBoxTestLocation.Size = new System.Drawing.Size(235, 21);
            this.comboBoxTestLocation.TabIndex = 18;
            this.comboBoxTestLocation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTestLocationSelectedIndexChanged);
            // 
            // pictureBoxTestLocation
            // 
            this.pictureBoxTestLocation.Location = new System.Drawing.Point(256, 126);
            this.pictureBoxTestLocation.Name = "pictureBoxTestLocation";
            this.pictureBoxTestLocation.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxTestLocation.TabIndex = 19;
            this.pictureBoxTestLocation.TabStop = false;
            // 
            // pictureBoxDeviceClasses
            // 
            this.pictureBoxDeviceClasses.Location = new System.Drawing.Point(256, 205);
            this.pictureBoxDeviceClasses.Name = "pictureBoxDeviceClasses";
            this.pictureBoxDeviceClasses.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxDeviceClasses.TabIndex = 9;
            this.pictureBoxDeviceClasses.TabStop = false;
            // 
            // pictureBoxPing
            // 
            this.pictureBoxPing.Location = new System.Drawing.Point(256, 291);
            this.pictureBoxPing.Name = "pictureBoxPing";
            this.pictureBoxPing.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxPing.TabIndex = 8;
            this.pictureBoxPing.TabStop = false;
            // 
            // labelSerialNumber
            // 
            this.labelSerialNumber.AutoSize = true;
            this.labelSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSerialNumber.Location = new System.Drawing.Point(12, 437);
            this.labelSerialNumber.Name = "labelSerialNumber";
            this.labelSerialNumber.Size = new System.Drawing.Size(163, 16);
            this.labelSerialNumber.TabIndex = 20;
            this.labelSerialNumber.Text = "4. Enter Serial Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 466);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "End User Serial";
            // 
            // textBoxEndUserSerial
            // 
            this.textBoxEndUserSerial.Location = new System.Drawing.Point(12, 482);
            this.textBoxEndUserSerial.Name = "textBoxEndUserSerial";
            this.textBoxEndUserSerial.Size = new System.Drawing.Size(235, 20);
            this.textBoxEndUserSerial.TabIndex = 24;
            this.textBoxEndUserSerial.TextChanged += new System.EventHandler(this.TextBoxEndUserSerialTextChanged);
            this.textBoxEndUserSerial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxEndUserSerialKeyPress);
            // 
            // labelRunTest
            // 
            this.labelRunTest.AutoSize = true;
            this.labelRunTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRunTest.Location = new System.Drawing.Point(12, 563);
            this.labelRunTest.Name = "labelRunTest";
            this.labelRunTest.Size = new System.Drawing.Size(86, 16);
            this.labelRunTest.TabIndex = 25;
            this.labelRunTest.Text = "5. Run Test";
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(15, 582);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(235, 33);
            this.buttonGo.TabIndex = 26;
            this.buttonGo.Text = "START TESTS";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
            // 
            // pictureBoxSerials
            // 
            this.pictureBoxSerials.Location = new System.Drawing.Point(256, 437);
            this.pictureBoxSerials.Name = "pictureBoxSerials";
            this.pictureBoxSerials.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxSerials.TabIndex = 28;
            this.pictureBoxSerials.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Order,
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.testListDataGridViewTextBoxColumn,
            this.Status,
            this.Execute});
            this.dataGridView1.DataSource = this.testItemBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(339, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(645, 234);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Order
            // 
            this.Order.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Order.DataPropertyName = "ExecutionOrder";
            this.Order.HeaderText = "ExecutionOrder";
            this.Order.Name = "Order";
            this.Order.ReadOnly = true;
            this.Order.Width = 105;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // testListDataGridViewTextBoxColumn
            // 
            this.testListDataGridViewTextBoxColumn.DataPropertyName = "TestList";
            this.testListDataGridViewTextBoxColumn.HeaderText = "TestList";
            this.testListDataGridViewTextBoxColumn.Name = "testListDataGridViewTextBoxColumn";
            this.testListDataGridViewTextBoxColumn.ReadOnly = true;
            this.testListDataGridViewTextBoxColumn.Visible = false;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Execute
            // 
            this.Execute.HeaderText = "Execute";
            this.Execute.Name = "Execute";
            this.Execute.ReadOnly = true;
            this.Execute.Text = "Execute";
            this.Execute.UseColumnTextForButtonValue = true;
            this.Execute.Visible = false;
            this.Execute.Width = 50;
            // 
            // testItemBindingSource
            // 
            this.testItemBindingSource.DataSource = typeof(DynamicDevices.Testing.DAO.TestItem);
            // 
            // labelTestListTitle
            // 
            this.labelTestListTitle.AutoSize = true;
            this.labelTestListTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestListTitle.Location = new System.Drawing.Point(336, 110);
            this.labelTestListTitle.Name = "labelTestListTitle";
            this.labelTestListTitle.Size = new System.Drawing.Size(60, 13);
            this.labelTestListTitle.TabIndex = 30;
            this.labelTestListTitle.Text = "Test List:";
            // 
            // labelTestList
            // 
            this.labelTestList.AutoSize = true;
            this.labelTestList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestList.Location = new System.Drawing.Point(418, 110);
            this.labelTestList.Name = "labelTestList";
            this.labelTestList.Size = new System.Drawing.Size(0, 13);
            this.labelTestList.TabIndex = 31;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(339, 398);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(645, 181);
            this.textBoxLog.TabIndex = 32;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 33;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(899, 671);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(97, 24);
            this.buttonExit.TabIndex = 34;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExitClick);
            // 
            // pictureBoxRunTest
            // 
            this.pictureBoxRunTest.Location = new System.Drawing.Point(256, 563);
            this.pictureBoxRunTest.Name = "pictureBoxRunTest";
            this.pictureBoxRunTest.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxRunTest.TabIndex = 35;
            this.pictureBoxRunTest.TabStop = false;
            // 
            // labelTestLog
            // 
            this.labelTestLog.AutoSize = true;
            this.labelTestLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestLog.Location = new System.Drawing.Point(336, 379);
            this.labelTestLog.Name = "labelTestLog";
            this.labelTestLog.Size = new System.Drawing.Size(61, 13);
            this.labelTestLog.TabIndex = 37;
            this.labelTestLog.Text = "Test Log:";
            // 
            // labelResultTitle
            // 
            this.labelResultTitle.AutoSize = true;
            this.labelResultTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResultTitle.Location = new System.Drawing.Point(336, 606);
            this.labelResultTitle.Name = "labelResultTitle";
            this.labelResultTitle.Size = new System.Drawing.Size(111, 16);
            this.labelResultTitle.TabIndex = 38;
            this.labelResultTitle.Text = "5. Test Results";
            // 
            // pictureBoxTestResult
            // 
            this.pictureBoxTestResult.Location = new System.Drawing.Point(465, 606);
            this.pictureBoxTestResult.Name = "pictureBoxTestResult";
            this.pictureBoxTestResult.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxTestResult.TabIndex = 39;
            this.pictureBoxTestResult.TabStop = false;
            // 
            // labelTestResultText1
            // 
            this.labelTestResultText1.AutoSize = true;
            this.labelTestResultText1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestResultText1.Location = new System.Drawing.Point(517, 606);
            this.labelTestResultText1.Name = "labelTestResultText1";
            this.labelTestResultText1.Size = new System.Drawing.Size(79, 16);
            this.labelTestResultText1.TabIndex = 40;
            this.labelTestResultText1.Text = "SUCCESS";
            // 
            // labelTestResultText2
            // 
            this.labelTestResultText2.AutoSize = true;
            this.labelTestResultText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestResultText2.Location = new System.Drawing.Point(517, 625);
            this.labelTestResultText2.Name = "labelTestResultText2";
            this.labelTestResultText2.Size = new System.Drawing.Size(72, 16);
            this.labelTestResultText2.TabIndex = 41;
            this.labelTestResultText2.Text = "SUCCESS";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(348, 9);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(35, 13);
            this.labelDatabase.TabIndex = 42;
            this.labelDatabase.Text = "label4";
            this.labelDatabase.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useDevDatabaseToolStripMenuItem,
            this.clearCacheFolderToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // useDevDatabaseToolStripMenuItem
            // 
            this.useDevDatabaseToolStripMenuItem.CheckOnClick = true;
            this.useDevDatabaseToolStripMenuItem.Name = "useDevDatabaseToolStripMenuItem";
            this.useDevDatabaseToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.useDevDatabaseToolStripMenuItem.Text = "Use Dev Database";
            this.useDevDatabaseToolStripMenuItem.Click += new System.EventHandler(this.UseDevDatabaseToolStripMenuItemClick);
            // 
            // textBoxProducerSerial
            // 
            this.textBoxProducerSerial.Enabled = false;
            this.textBoxProducerSerial.Location = new System.Drawing.Point(12, 530);
            this.textBoxProducerSerial.Name = "textBoxProducerSerial";
            this.textBoxProducerSerial.Size = new System.Drawing.Size(235, 20);
            this.textBoxProducerSerial.TabIndex = 45;
            // 
            // labelProviderSerial
            // 
            this.labelProviderSerial.AutoSize = true;
            this.labelProviderSerial.Location = new System.Drawing.Point(9, 514);
            this.labelProviderSerial.Name = "labelProviderSerial";
            this.labelProviderSerial.Size = new System.Drawing.Size(75, 13);
            this.labelProviderSerial.TabIndex = 44;
            this.labelProviderSerial.Text = "Provider Serial";
            // 
            // buttonCheckSerial
            // 
            this.buttonCheckSerial.Location = new System.Drawing.Point(253, 482);
            this.buttonCheckSerial.Name = "buttonCheckSerial";
            this.buttonCheckSerial.Size = new System.Drawing.Size(48, 23);
            this.buttonCheckSerial.TabIndex = 25;
            this.buttonCheckSerial.Text = "Check";
            this.buttonCheckSerial.UseVisualStyleBackColor = true;
            this.buttonCheckSerial.Click += new System.EventHandler(this.ButtonCheckSerialClick);
            // 
            // checkBoxLocalTest
            // 
            this.checkBoxLocalTest.AutoSize = true;
            this.checkBoxLocalTest.Enabled = false;
            this.checkBoxLocalTest.Location = new System.Drawing.Point(878, 106);
            this.checkBoxLocalTest.Name = "checkBoxLocalTest";
            this.checkBoxLocalTest.Size = new System.Drawing.Size(106, 17);
            this.checkBoxLocalTest.TabIndex = 47;
            this.checkBoxLocalTest.Text = "Local Test Mode";
            this.checkBoxLocalTest.UseVisualStyleBackColor = true;
            this.checkBoxLocalTest.CheckedChanged += new System.EventHandler(this.CheckBoxLocalTestCheckedChanged);
            // 
            // clearCacheFolderToolStripMenuItem
            // 
            this.clearCacheFolderToolStripMenuItem.Name = "clearCacheFolderToolStripMenuItem";
            this.clearCacheFolderToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearCacheFolderToolStripMenuItem.Text = "Clear cache folder";
            this.clearCacheFolderToolStripMenuItem.Click += new System.EventHandler(this.clearCacheFolderToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.checkBoxLocalTest);
            this.Controls.Add(this.buttonCheckSerial);
            this.Controls.Add(this.textBoxProducerSerial);
            this.Controls.Add(this.labelProviderSerial);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelTestResultText2);
            this.Controls.Add(this.labelTestResultText1);
            this.Controls.Add(this.pictureBoxTestResult);
            this.Controls.Add(this.labelResultTitle);
            this.Controls.Add(this.labelTestLog);
            this.Controls.Add(this.pictureBoxRunTest);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.labelTestList);
            this.Controls.Add(this.labelTestListTitle);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBoxSerials);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.labelRunTest);
            this.Controls.Add(this.textBoxEndUserSerial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSerialNumber);
            this.Controls.Add(this.pictureBoxTestLocation);
            this.Controls.Add(this.comboBoxTestLocation);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.textBoxSSHPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSSHLogin);
            this.Controls.Add(this.labelSSHlogin);
            this.Controls.Add(this.labelIPAddress);
            this.Controls.Add(this.buttonTestForDevice);
            this.Controls.Add(this.pictureBoxDeviceClasses);
            this.Controls.Add(this.textBoxIPAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDeviceClass);
            this.Controls.Add(this.labelSelectDeviceClass);
            this.Controls.Add(this.labelTester);
            this.Controls.Add(this.labelAgency);
            this.Controls.Add(this.labelTesterTitle);
            this.Controls.Add(this.labelCompanyTitle);
            this.Controls.Add(this.pictureBoxPing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Device Testing";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTestLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeviceClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSerials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testItemBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRunTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTestResult)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCompanyTitle;
        private System.Windows.Forms.Label labelTesterTitle;
        private System.Windows.Forms.Label labelAgency;
        private System.Windows.Forms.Label labelTester;
        private System.Windows.Forms.Label labelSelectDeviceClass;
        private System.Windows.Forms.ComboBox comboBoxDeviceClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIPAddress;
        private System.Windows.Forms.PictureBox pictureBoxPing;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBoxDeviceClasses;
        private System.Windows.Forms.Button buttonTestForDevice;
        private System.Windows.Forms.Label labelIPAddress;
        private System.Windows.Forms.Label labelSSHlogin;
        private System.Windows.Forms.TextBox textBoxSSHLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSSHPassword;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.PictureBox pictureBoxTestLocation;
        private System.Windows.Forms.ComboBox comboBoxTestLocation;
        private System.Windows.Forms.Timer timerTestComms;
        private System.Windows.Forms.Label labelSerialNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEndUserSerial;
        private System.Windows.Forms.Label labelRunTest;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.PictureBox pictureBoxSerials;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource testItemBindingSource;
        private System.Windows.Forms.Label labelTestListTitle;
        private System.Windows.Forms.Label labelTestList;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.PictureBox pictureBoxRunTest;
        private System.Windows.Forms.Label labelTestLog;
        private System.Windows.Forms.Label labelResultTitle;
        private System.Windows.Forms.PictureBox pictureBoxTestResult;
        private System.Windows.Forms.Label labelTestResultText1;
        private System.Windows.Forms.Label labelTestResultText2;
        private System.ComponentModel.BackgroundWorker backgroundWorkerTest;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useDevDatabaseToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxProducerSerial;
        private System.Windows.Forms.Label labelProviderSerial;
        private System.Windows.Forms.Button buttonCheckSerial;
        private System.Windows.Forms.CheckBox checkBoxLocalTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Order;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn testListDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn Execute;
        private System.Windows.Forms.ToolStripMenuItem clearCacheFolderToolStripMenuItem;
    }
}

