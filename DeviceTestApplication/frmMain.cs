using System;
using System.Text;
using System.Configuration;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DeviceTestApplication.Scripting;
using DeviceTestApplication.Utilities;
using log4net;
using log4net.Config;
using NHibernate;
using NHibernate.Criterion;
using Renci.SshNet;
using DynamicDevices.Testing.DAO;

namespace DeviceTestApplication
{
    public partial class FrmMain : Form
    {
        #region Fields

        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        protected static NHibernate.Cfg.Configuration NhibernateCfg;
        protected static ISessionFactory Nhibernatefactory;
        protected ISession Session;

        protected SshClient Client;

        protected bool _bTestsDownloaded;
        protected bool _bTestsInitialisedOnTarget;

        #endregion

        public FrmMain()
        {
            InitializeComponent();

#if DEBUG
//            useDevDatabaseToolStripMenuItem.Checked = true;
#endif
            //
            // For now delete the cache each time we run the app
            //
#if false
            try
            {
                if (Directory.Exists(Globals.CacheFolder))
                    Directory.Delete(Globals.CacheFolder, true);
            } catch(Exception e)
            {
                Logger.Warn("Problem deleting cache folder: " + e.Message);
            }
#endif

            textBoxIPAddress.Enabled = false;
            textBoxSSHLogin.Enabled = false;
            textBoxSSHPassword.Enabled = false;

            toolStripStatusLabel1.Text = "OK";

            textBoxProducerSerial.Enabled = false;
            
            pictureBoxSerials.Image = imageList1.Images["cross"];
            pictureBoxRunTest.Image = imageList1.Images["cross"];
            pictureBoxTestResult.Image = null;
            labelTestResultText1.Text = "";
            labelTestResultText2.Text = "";

            buttonTestForDevice.Enabled = false;
            buttonCheckSerial.Enabled = false;
            textBoxEndUserSerial.Enabled = false;

#if RELEASE
            buttonGo.Enabled = false;
#endif

            // Get executable name
            string strExeName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            // Configure log4net
            XmlConfigurator.ConfigureAndWatch(new FileInfo(strExeName + ".exe.config"));

            Version objVersion = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            Text += string.Format(" - Version {0}", objVersion);

            Logger.Info("Application starting, code version is " + objVersion);

            Logger.Info("Application data is in: " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            // Ensure device is set to a dummy in case we do local testing
            Globals.Device = new Device
                                 {
                                     CustomerSerialNumber = "00000000",
                                     ProducerSerialNumber = "00000000",
                                     DeviceClass = new DeviceClass
                                                       {
                                                           Name
                                                               =
                                                               "Local Testing"
                                                       }
                                 };
            Globals.ProducerSerialNumber = "00000000";
            Globals.CustomerSerialNumber = "00000000";
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        ~FrmMain()
        {
            if(Session != null)
            {
                Session.Close();
                Session = null;
            }
        }

        /// <summary>
        /// Initial setup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormLoad(object sender, EventArgs e)
        {
            // Store handle so that the MB can centre dialogs on us
            MessageBoxEx.Owner = Handle;

            if(!InitialiseDB())
            {
                MessageBoxEx.Show("Couldn't connect to database. Exiting.");
                Application.Exit();
            }

#if DEBUG
            ButtonAdminClick();

            labelDatabase.Text = NhibernateCfg.GetProperty(NHibernate.Cfg.Environment.ConnectionString);
            labelDatabase.Visible = true;
#endif

            bool success = false;
            var f = new frmLogin();
            do
            {
                if (ConfigurationManager.AppSettings["debug_login"] == null
                    || ConfigurationManager.AppSettings["debug_password"] == null
#if RELEASE
                    || true
#endif
                    )
                {
                    DialogResult dr = f.ShowDialog();

                    if (dr == DialogResult.Cancel)
                    {
                        Close();
                        return;
                    }
                    Globals.LoggedInUser = GetUser(f.Login, f.Password);
                }
                else
                {
                    Globals.LoggedInUser = GetUser(ConfigurationManager.AppSettings["debug_login"], ConfigurationManager.AppSettings["debug_password"]);                    
                }

                if (Globals.LoggedInUser == null)
                {
                    MessageBoxEx.Show("Login failed. Please try again.");
                    continue;
                }

                // Initialise UI
                labelAgency.Text = Globals.LoggedInUser.Company.Name;
                labelTester.Text = Globals.LoggedInUser.Forename + " " + Globals.LoggedInUser.Surname;
                if (Globals.LoggedInUser.Email != null)
                    labelTester.Text += " (" + Globals.LoggedInUser.Email + ")";

                comboBoxTestLocation.DataSource = Globals.LoggedInUser.Company.TestLocations;
                comboBoxTestLocation.DisplayMember = "Name";
                comboBoxTestLocation.ValueMember = "Name";
                comboBoxDeviceClass.ResetBindings();

                if (Globals.LoggedInUser.Company.TestLocations.Count > 1)
                {
                    comboBoxTestLocation.SelectedIndex = -1;
                    pictureBoxPing.Image = imageList1.Images["cross"];
                }
                else
                {
                    if (Globals.LoggedInUser.Company.TestLocations.Count > 0)
                    {
                        comboBoxTestLocation.SelectedIndex = 0;
                        pictureBoxPing.Image = imageList1.Images["cross"];
                    }
                    else
                    {
                        MessageBoxEx.Show("No test location set - please contact d/b administrator");
                        Close();
                        return;
                    }
                }

                // Set up device classes selection box
                var criteria = Session.CreateCriteria(typeof (DeviceClass));
                criteria.Add(Restrictions.Eq("Enabled", true));
                var list = criteria.List<DeviceClass>();
                comboBoxDeviceClass.DataSource = list;
                comboBoxDeviceClass.DisplayMember = "Name";
                comboBoxDeviceClass.ValueMember = "Name";
                comboBoxDeviceClass.ResetBindings();

                if (list.Count > 1)
                {
                    comboBoxDeviceClass.SelectedIndex = -1;
                    pictureBoxPing.Image = imageList1.Images["cross"];
                }
                else
                {
                    if (list.Count > 0)
                    {
                        comboBoxDeviceClass.SelectedIndex = 0;
                        pictureBoxPing.Image = imageList1.Images["alert"];
                    }
                    else
                    {
                        MessageBoxEx.Show("No test location set - please contact d/b administrator");
                        Close();
                        return;
                    }
                }

                success = true;

            } while (!success);
        }

        /// <summary>
        /// Setup NHibernate and connect to the database
        /// </summary>
        /// <returns></returns>
        private bool InitialiseDB()
        {
            //
            // Setup database connection
            //
            // (1) Configure NHibernate 
            //
            // - NB. We should be able to do this from a configuration object
            // - Also there may be versioning issues with log4net ?
            //
            //            string strDirectory = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName;           
            bool success = false;
            try
            {
                Logger.Debug("Bringing up NHibernate");
                NhibernateCfg = new NHibernate.Cfg.Configuration();

                if (useDevDatabaseToolStripMenuItem.Checked)
                {
                    if (!string.IsNullOrEmpty(Globals.DevelopmentDBURI))
                    {
                        Logger.Warn("*** OVERRIDING WITH DEV DATABASE ***");
                        NhibernateCfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, Globals.DevelopmentDBURI);
                    }
                    else
                    {
                        throw new ArgumentException("*** NO DEV DATABASE DEFINED ***");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Globals.ProductionDBURI))
                    {
                        NhibernateCfg.SetProperty(NHibernate.Cfg.Environment.ConnectionString, Globals.ProductionDBURI);
                    }
                    else
                    {
                        throw new ArgumentException("*** NO PRODUCTION DATABASE DEFINED ***");
                    }
                }

                NhibernateCfg.AddAssembly("DynamicDevices.Testing.DAO");

                Logger.Debug("Now building external DB session factory");
                Nhibernatefactory = NhibernateCfg.BuildSessionFactory();

                Session = Nhibernatefactory.OpenSession();

                success = true;
            } catch(Exception e)
            {
                Logger.Warn("Exception bringing up NHibernate: " + e.Message);
            }
            return success;
        }

        /// <summary>
        /// Authenticate against the d/b user for this login/password. 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        private Employee GetUser(string login, string pw)
        {
            try
            {
                    var criteria = Session.CreateCriteria(typeof(Employee));

                    criteria.Add(Restrictions.Eq("Login", login));
                    criteria.Add(Restrictions.Eq("Password", pw));
                    
                    var employee = criteria.List<Employee>();

                    if(employee.Count > 0)
                    {
                        if(!employee[0].Enabled)
                        {
                            MessageBoxEx.Show("Login is disabled. Please contact board vendor.");
                            return null;
                        }
                        
                        // Make sure this is updated 
                        Session.Refresh(employee[0]);

                        return employee[0];
                    }
                    return null;
            } catch(Exception e)
            {
                Logger.Warn("Exception in test login: " + e.Message);
            }
            return null;
        }

        /// <summary>
        /// Handle change in device class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxDeviceClassSelectedIndexChanged(object sender, EventArgs e)
        {
            labelTestList.Text = "";

            pictureBoxPing.Image = imageList1.Images["cross"];
            buttonGo.Enabled = false;
            buttonCheckSerial.Enabled = false;
            textBoxEndUserSerial.Enabled = false;

            if (comboBoxDeviceClass.SelectedIndex >= 0)
            {
                // Index changed - onto step "2", the IP address
                textBoxIPAddress.Enabled = true;
                textBoxSSHLogin.Enabled = true;
                textBoxSSHPassword.Enabled = true;
                pictureBoxPing.Enabled = true;
                pictureBoxDeviceClasses.Image = imageList1.Images["tick"];
                buttonTestForDevice.Enabled = true;

                Globals.DeviceClass = (DeviceClass)comboBoxDeviceClass.SelectedItem;

                // Set device class characteristics
                textBoxIPAddress.Text = Globals.DeviceClass.DefaultHost;
                textBoxSSHLogin.Text = Globals.DeviceClass.DefaultLogin;
                textBoxSSHPassword.Text = Globals.DeviceClass.DefaultPassword;

                // Set tests into datagrid

                //
                // AJL - For now for simplicity we'll say we should only have one TestList per DeviceClass
                //
                // TODO: Make sure we only check for enabled test lists...
                
                //_session.Refresh(Globals.DeviceClass);

                var testLists = Globals.DeviceClass.TestLists.FindAll(tl => tl.Enabled);
                if(testLists.Count == 0)
                {
                    MessageBoxEx.Show("There is no test list for the device class! (" + Globals.DeviceClass.Name + ") Contact board vendor");
                    return;
                }
                if(testLists.Count > 1)
                {
                    MessageBoxEx.Show("There are too many test lists for the device class! (" + Globals.DeviceClass.Name + ") Contact board vendor");
                    return;                    
                }
                Globals.TestList= testLists[0];

                labelTestList.Text = Globals.TestList.Name + " (" + Globals.TestList.CreationDate + ")";

                // AJL - Set unbound "status" variable to DISABLED if test item is disabled
                foreach (var testItem in Globals.TestList.TestItems)
                    testItem.Status = !testItem.Enabled ? EnumTestStatus.Disabled : EnumTestStatus.NotStarted;
                testItemBindingSource.DataSource = Globals.TestList.TestItems;

            }

            else
            {
                // Index changed - onto step "2", the IP address
                textBoxIPAddress.Enabled = false;
                textBoxSSHLogin.Enabled = false;
                textBoxSSHPassword.Enabled = false;
                pictureBoxPing.Enabled = false;
                pictureBoxDeviceClasses.Image = imageList1.Images["cross"];
                buttonTestForDevice.Enabled = false;

                textBoxIPAddress.Text = "";
                textBoxSSHLogin.Text = "";
                textBoxSSHPassword.Text = "";

                testItemBindingSource.Clear();
            }
        }

        /// <summary>
        /// Handle change in test location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxTestLocationSelectedIndexChanged(object sender, EventArgs e)
        {
            buttonTestForDevice.Enabled = false;
            pictureBoxPing.Image = imageList1.Images["cross"];
            buttonGo.Enabled = false;
            buttonCheckSerial.Enabled = false;
            textBoxEndUserSerial.Enabled = false;

            if (comboBoxTestLocation.SelectedIndex >= 0)
            {
                comboBoxDeviceClass.Enabled = true;
                pictureBoxTestLocation.Image = imageList1.Images["tick"];
                Globals.TestLocation = (TestLocation)comboBoxTestLocation.SelectedItem;
            }
            else
            {
                comboBoxDeviceClass.Enabled = false;
                pictureBoxTestLocation.Image = imageList1.Images["cross"];
            }
        }

        /// <summary>
        /// Check that device on test is accessible, by Pinging and making an SSH connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTestForDeviceClick(object sender, EventArgs e)
        {
            // Make sure tests transferred to target again
            _bTestsInitialisedOnTarget = false;

            var cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            toolStripStatusLabel1.Text = "Pinging target";

            pictureBoxSerials.Image = imageList1.Images["cross"];
            textBoxProducerSerial.Enabled = false;
            textBoxEndUserSerial.Enabled = false;
            buttonCheckSerial.Enabled = false;
            buttonGo.Enabled = false;
            checkBoxLocalTest.Enabled = false;

            // Check IP address
            try
            {
                Globals.DeviceAddress = IPAddress.Parse(textBoxIPAddress.Text);
            }
            catch
            {
                toolStripStatusLabel1.Text = "Invalid IP address";
                pictureBoxPing.Image = imageList1.Images["cross"];
                Cursor.Current = cursor;
                return;
            }
            pictureBoxPing.Image = imageList1.Images["tick"];

            try
            {
                var p = new Ping();
                var pr = p.Send(Globals.DeviceAddress);
                if (pr != null)
                    if (pr.Status == IPStatus.Success)
                        Globals.DevicePingable = true;
            }
            catch(Exception ex)
            {
                Logger.Warn("Exception pinging target: " + ex.Message);
            }

            if (Globals.DevicePingable)
                pictureBoxPing.Image = imageList1.Images["tick"];
            else
            {
                pictureBoxPing.Image = imageList1.Images["cross"];
                toolStripStatusLabel1.Text = "Can't ping device";
                Cursor.Current = cursor;
                return;
            }

            toolStripStatusLabel1.Text = "Connecting to target SSH server";

            // Make SSH connection
            bool connected = false;
            try
            {
                if (Client != null)
                {
                    try
                    {
                        if (!Client.IsConnected)
                        {
                            try
                            {
                                Client.Dispose();
                                Client = null;
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        Client = null;
                    }
                }

                if (Client == null)
                {
                    Client = new SshClient(textBoxIPAddress.Text, textBoxSSHLogin.Text, textBoxSSHPassword.Text);
                    
                    Client.Connect();

            //        _bTestsInitialisedOnTarget = false;
                }
                connected = Client.IsConnected;
            }
            catch (Exception ex)
            {
                Logger.Warn("Exception making SSH connection: " + ex.Message);
            }

            if(!connected)
            {
                if (Client != null)
                {
                    try
                    {
                        Client.Dispose();
                    } catch
                    {
                    }
                }

                Client = null;

                pictureBoxPing.Image = imageList1.Images["cross"];
                toolStripStatusLabel1.Text = "Can't SSH to device";
                Cursor.Current = cursor;
                return;
            }

            toolStripStatusLabel1.Text = "Server OK";                          
            pictureBoxSerials.Image = imageList1.Images["cross"];
            textBoxEndUserSerial.Enabled = true;
            buttonCheckSerial.Enabled = true;
            checkBoxLocalTest.Enabled = true;

            // Download tests
            if (!_bTestsDownloaded)
                DownloadTests();

            // Read serial number
            var script = new Script(Globals.ScriptFileName);
            if (script.ReadSerial != null)
            {
                string output;
                // TODO: Improve this
                script.ReadSerial.SshClient = Client;
                script.ReadSerial.Execute(textBoxIPAddress.Text, textBoxSSHLogin.Text,
                                          textBoxSSHPassword.Text, out output);
                if (!string.IsNullOrEmpty(output))
                {
                    // TODO: Need to make this generic
                    try
                    {
                        string parsedOutput = output.Replace("\n", "");
                        parsedOutput = parsedOutput.Replace(" ", "");
                        long iParsedOutput = long.Parse(parsedOutput);
                        if (iParsedOutput != 0)
                        {
                            Globals.CustomerSerialNumber = iParsedOutput.ToString();
                            textBoxEndUserSerial.Text = Globals.CustomerSerialNumber;
                        }
                    } catch(Exception ex)
                    {
                        DoLog("Exception parsing serial number: " + output + ": " + ex.Message);
                    }
                }
            }

            if (textBoxProducerSerial.Text.Length > 0 && textBoxEndUserSerial.Text.Length > 0)
            {
                buttonGo.Enabled = true;
                buttonGo.Select();
            }
            else
            {
                textBoxEndUserSerial.Select();
            }
        }

        /// <summary>
        /// Hidden admin function to initialise d/b with test data (only usable in DEBUG mode)
        /// </summary>
        private void ButtonAdminClick()
        {
            var f = new frmAdmin {Session = Session};
            f.ShowDialog();
        }

        /// <summary>
        /// Get the next free serial numbers for this device class from the d/b. Also check there are no devices with these serials in the d/b
        /// </summary>
        private void ButtonGetSerialNumbersClick()
        {
            Device device;

            buttonGo.Enabled = false;

            foreach (TestItem testItem in Globals.TestList.TestItems)
                testItem.Status = !testItem.Enabled ? EnumTestStatus.Disabled : EnumTestStatus.NotStarted;
            dataGridView1.Columns["Execute"].Visible = checkBoxLocalTest.Checked;
            dataGridView1.Refresh();

            //
            // Note: AJL: Need to think about whether somebody else could get the ID we are working with?
            //
            try
            {
                // Check customer serial is within range
                var serial = long.Parse(textBoxEndUserSerial.Text);
                bool ok;
                if (Globals.DeviceClass.MaxCustomerSerial != null)
                    ok = serial >= long.Parse(Globals.DeviceClass.MinCustomerSerial) ||
                         serial <= long.Parse(Globals.DeviceClass.MaxCustomerSerial);
                else
                    ok = serial >= long.Parse(Globals.DeviceClass.MinCustomerSerial);

                if (!ok)
                {
                    MessageBoxEx.Show("The serial number is outside the allowed range (" +
                                    Globals.DeviceClass.MinCustomerSerial + "-"
                                    + ((Globals.DeviceClass.MaxCustomerSerial != null) ? (Globals.DeviceClass.MaxCustomerSerial) : "\u221E")
                                    + ")");
                    return;
                }

                using (var transaction = Session.BeginTransaction())
                {
                    try
                    {
                        // Check if customer serial is in d/b
                        ICriteria criteria = Session.CreateCriteria(typeof (Device));
                        criteria.Add(
                            Restrictions.Eq("CustomerSerialNumber", textBoxEndUserSerial.Text)
                            );
                        var results = criteria.List<Device>();
                        if (results.Count > 1)
                        {
                            // Shouldn't happen
                            MessageBoxEx.Show(
                                "There are " + results.Count +
                                " devices in the database with this serial number. Please contact board vendor");
                            Logger.Warn("There are " + results.Count +
                                         " devices in the database with the entered serial number (" +
                                         textBoxEndUserSerial.Text + ")");
                            transaction.Rollback();
                            return;
                        }
                        if (results.Count == 1)
                        {
                            // There have been tests for this customer serial number.
                            // Check if any of those tests have been successful 
                            int tests = 0;
                            int testSuccesses = 0;
                            foreach (var d in results)
                            {
                                foreach (var tlr in d.TestListResults)
                                {
                                    if (tlr.Result)
                                        testSuccesses++;
                                    tests++;
                                }
                            }
                            if (testSuccesses > 0)
                            {
                                DialogResult dr = MessageBoxEx.Show(
                                    "This customer serial number has already tested successfully " + testSuccesses + "/" +
                                    tests +
                                    " time(s). Please confirm you are retesting.", "Retesting?", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                                if (dr != DialogResult.Yes)
                                {
                                    transaction.Rollback();
                                    return;
                                }
                            }

                            if (tests > 0)
                            {
                                DialogResult dr = MessageBoxEx.Show(
                                    "This serial number has failed testing " + tests +
                                    " time(s). Please confirm you are retesting.", "Retesting?", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                                if (dr != DialogResult.Yes)
                                {
                                    transaction.Rollback();
                                    return;
                                }
                            }
                            device = results[0];

                            // Set existing board vendor serial
                            textBoxProducerSerial.Text = device.ProducerSerialNumber;
                        }
                        else
                        {
                            // Ensure device class is refreshed
                            Session.Refresh(Globals.DeviceClass);

                            // Set new board vendor serial number
                            var lastProducerSerial = Globals.DeviceClass.LastProducerSerial;

                            // TBD: Check this doesn't exist?

                            int lvs;

                            if (lastProducerSerial == null)
                            {
                                lastProducerSerial = Globals.DeviceClass.MinProducerSerial;
                                lvs = int.Parse(lastProducerSerial);
                            }
                            else
                            {
                                lvs = int.Parse(lastProducerSerial);
                                lvs = lvs + 1;
                            }

                            if (Globals.DeviceClass.MaxProducerSerial != null)
                            {
                                if (lvs > int.Parse(Globals.DeviceClass.MaxProducerSerial))
                                {
                                    MessageBoxEx.Show(
                                        "Problem - there are no free producer serial numbers. Contact board board vendor");
                                    transaction.Rollback();
                                    return;
                                }
                            }
                            textBoxProducerSerial.Text = lvs.ToString();

                            // Update last serials
                            Globals.DeviceClass.LastCustomerSerial = textBoxEndUserSerial.Text;
                            Globals.DeviceClass.LastProducerSerial = textBoxProducerSerial.Text;
                            Session.SaveOrUpdate(Globals.DeviceClass);

                            // Create new device
                            device = new Device
                                         {
                                             CreationDate = DateTime.Now,
                                             DeviceClass = Globals.DeviceClass,
                                             CustomerSerialNumber = textBoxEndUserSerial.Text,
                                             ProducerSerialNumber = textBoxProducerSerial.Text,
                                             Creator = Globals.LoggedInUser,
                                         };
                        }

                        // Whatever we just did, make sure last test date/person are correct
                        device.LastTestDate = DateTime.Now;
                        device.LastTester = Globals.LoggedInUser;

                        // - Store it to d/b
                        Session.SaveOrUpdate(device);

                        if (Globals.Device != null)
                        {
                            //                        Debug.Assert(Globals.Device.Equals(device), "Problem as devices different?");
                            Session.Evict(Globals.Device);
                        }
                        // Store new device
                        Globals.Device = device;

                        // All done
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn("Exception dealing with serial numbers: " + ex.Message);
                        transaction.Rollback();
                        return;
                    }
                }

                // Update Globals
                Globals.ProducerSerialNumber = textBoxProducerSerial.Text;
                Globals.CustomerSerialNumber = textBoxEndUserSerial.Text;

                // Update UI
                pictureBoxSerials.Image = imageList1.Images["tick"];
                pictureBoxRunTest.Image = imageList1.Images["tick"];
                buttonGo.Enabled = true;
                buttonGo.Select();
            }
            catch (Exception e)
            {
                Logger.Warn("Exception getting/dealing with serial numbers: " + e.Message);
            }
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExitClick(object sender, EventArgs e)
        {
            Close();
        }

 
        /// <summary>
        /// Start running the tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonGoClick(object sender, EventArgs e)
        {
            buttonExit.Enabled = false;

            buttonGo.Enabled = false;
            pictureBoxSerials.Image = imageList1.Images["cross"];
            pictureBoxRunTest.Image = imageList1.Images["cross"];
            textBoxProducerSerial.Enabled = false;
            textBoxEndUserSerial.Enabled = false;
            buttonCheckSerial.Enabled = false;

            pictureBoxTestResult.Image = imageList1.Images["alert"];
            labelTestResultText1.Text = "";
            labelTestResultText2.Text = "... working ...";

            textBoxLog.Clear();

            // Need to make this thread-safe and implement in background
            try
            {
                DoWork(true, -1);

                textBoxEndUserSerial.Enabled = true;
                textBoxEndUserSerial.Text = "";
                textBoxProducerSerial.Text = "";

                // AJL - Don't reeenable this as we want them to go to check connection
                //       Otherwise the archive and script won't get copied, which is
                //       confusing for the user
                //buttonCheckSerial.Enabled = true;
                
                buttonExit.Enabled = true;

                buttonTestForDevice.Select();
            } catch
            {
                toolStripStatusLabel1.Text = "Couldn't connect to SSH server";
            }
        }

        /// <summary>
        /// Log to UI textbox and to Log4net logger
        /// </summary>
        /// <param name="text"></param>
        protected void DoLog(string text)
        {
            Logger.Info(text);
            textBoxLog.Text += text + "\r\n";
            textBoxLog.SelectionStart = textBoxLog.Text.Length - 1;
            textBoxLog.ScrollToCaret();
            Update();
        }

        /// <summary>
        /// Run a single test for debugging - no database update
        /// </summary>
        /// <param name="script"></param>
        /// <param name="client"></param>
        /// <param name="testNum"></param>
        protected void RunLocalTest(Script script, SshClient client, int  testNum)
        {
            TestItem testItem = Globals.TestList.TestItems[testNum];
            if(testItem == null)
            {
                MessageBoxEx.Show("Problem retrieving test. Contact board vendor");
                return;
            }

            DoLog("Running test " + testNum + ": " + testItem.Name);
            toolStripStatusLabel1.Text = "Running test " + testNum + ": " + testItem.Name;

            Test testScript;
            if (script.Tests.ContainsKey(testItem.Name.ToLower()))
                testScript = script.Tests[testItem.Name.ToLower()];
            else
            {
                DoLog("Couldn't find script for test item. Not running test.");
                testItem.Status = EnumTestStatus.NoScript;
                Refresh();
                Application.DoEvents();
                return;
            }

            testItem.Status = EnumTestStatus.Running;
            Refresh();
            Application.DoEvents();

            testScript.OnUpdateUI -= testScript_OnUpdateUI;
            testScript.OnUpdateUI += testScript_OnUpdateUI;

            //
            // Now actually run the test
            //
            string output;
            testScript.SshClient = client;
            bool testStat = testScript.Execute(textBoxIPAddress.Text, textBoxSSHLogin.Text, textBoxSSHPassword.Text, out output);
            if (testStat)
            {
                DoLog("- test SUCCEEDED ☑ ");
                DoLog("- output: " + output);
                testItem.Status = EnumTestStatus.Success;
                Session.Update(testItem);
                Refresh();
                Application.DoEvents();

            }
            else
            {
                DoLog("- test FAILED ☒ on command: '" + testScript.LastCommand.Log + "', with output '" +
                      output + "'");
                if (!Logger.IsInfoEnabled)
                    Logger.Warn("- test FAILED on command: '" + testScript.LastCommand.Log + "', with output '" +
                                output + "'");

                testItem.Status = EnumTestStatus.Failed;
                Refresh();
                Application.DoEvents();

            }
        }

        /// <summary>
        /// Run scripted tests
        /// </summary>
        /// <param name="script"></param>
        /// <param name="client"></param>
        protected void RunTests(Script script, SshClient client)
        {
            var originalCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            //
            // - Create the test list results
            //
            var testListResult = new TestListResult
                                     {
                                         CreationDate = DateTime.Now,
                                         Device = Globals.Device,
                                         Employee = Globals.LoggedInUser,
                                         TestList = Globals.TestList,
                                         TestLocation = Globals.TestLocation,
                                         ResultURL = Globals.RemoteResultFile,
                                     };

            // Save device
            Session.Save(testListResult);
            Session.Refresh(Globals.Device);

            // Do tests...        
            bool success = true;
            int testNum = 0;
            foreach (var testItem in Globals.TestList.TestItems)
            {
                testNum++;

                // Don't run disabled tests
                if (testItem.Status == EnumTestStatus.Disabled)
                    continue;

                Refresh();
                Application.DoEvents();

                // Get correct test from script
                try
                {
                    // Create new test item result
                    var testItemResult = new TestItemResult
                                             {
                                                 CreationDate = DateTime.Now,
                                                 Device = Globals.Device,
                                                 TestItem = testItem,
                                                 TestListResult = testListResult
                                             };
                    Session.Save(testItemResult);

                    DoLog("Running test " + testNum + ": " + testItem.Name);
                    toolStripStatusLabel1.Text = "Running test " + testNum + ": " + testItem.Name;

                    Test testScript;
                    if(script.Tests.ContainsKey(testItem.Name.ToLower()))
                        testScript = script.Tests[testItem.Name.ToLower()];
                    else
                    {
                        DoLog("Couldn't find script for test item. Not running test.");
                        testItem.Status = EnumTestStatus.NoScript;
                        Refresh();
                        Application.DoEvents();

                        // Can't succeed if not all tests ran...
                        success = false;

                        continue;
                    }

                    testItem.Status = EnumTestStatus.Running;
                    Refresh();
                    Application.DoEvents();

                    testScript.OnUpdateUI -= testScript_OnUpdateUI;
                    testScript.OnUpdateUI += testScript_OnUpdateUI;

                    //
                    // Now actually run the test
                    //
                    string output;
                    testScript.SshClient = client;
                    bool testStat = testScript.Execute(textBoxIPAddress.Text, textBoxSSHLogin.Text, textBoxSSHPassword.Text, out output);
                    if(testStat)
                    {
                        DoLog("- test SUCCEEDED ☑ ");
                        DoLog("- output: " + output);
                        testItem.Status = EnumTestStatus.Success;
                        Session.Update(testItem);
                        Refresh();
                        Application.DoEvents();

                        testItemResult.Result = true;
                        if (!string.IsNullOrEmpty(output))
                            testItemResult.Notes = output;
                        Session.Update(testItemResult);

                        continue;
                    }

                    DoLog("- test FAILED ☒ on command: '" + testScript.LastCommand.Log + "', with output '" +
                          output + "'");
                    if (!Logger.IsInfoEnabled)
                        Logger.Warn("- test FAILED on command: '" + testScript.LastCommand.Log + "', with output '" +
                                     output + "'");

                    testItem.Status = EnumTestStatus.Failed;
                    Refresh();
                    Application.DoEvents();

                    testItemResult.Result = false;
                    if(!string.IsNullOrEmpty(output))
                        testItemResult.Notes = output ;
                    Session.Update(testItemResult);

                    // Failed ...
                    success = false;
                    continue;

                } catch(Exception e)
                {
                    DoLog("*** exception running test: " + testItem.Name + ": " + e.Message);
                    if(!Logger.IsInfoEnabled)
                        Logger.Warn("Exception running test: " + testItem.Name + ": " + e.Message);
                }
            }

            Refresh();
            Application.DoEvents();

            // Update test list based on results
            Session.Refresh(testListResult);

            //
            // All tests dealt with now. Did we succeed?
            //
            if (success)
            {
                pictureBoxTestResult.Image = imageList1.Images["tick"];
                labelTestResultText1.Text = "SUCCESS";
                labelTestResultText2.Text = "- All tests completed correctly";
            }
            else
            {
                pictureBoxTestResult.Image = imageList1.Images["cross"];
                labelTestResultText1.Text = "FAILED";
                labelTestResultText2.Text = "- One or more tests failed. See details above";
            }
            Refresh();
            Application.DoEvents();

            DoLog("Updating database with results");
            Application.DoEvents();
            
            // Update the d/b with status
            testListResult.Result = success;
            Session.Update(testListResult);
            Session.Flush();

            // Now update the device object 
            //
            // NOTE: This should have been updated I would think, but we seem to need to do it
            //
            Session.Refresh(Globals.Device);

            if (!Directory.Exists(Globals.ResultFolder))
                Directory.CreateDirectory(Globals.ResultFolder);

            Refresh();
            Application.DoEvents();

            DoLog("Creating local results file");
            Application.DoEvents();
            
            // Update the test results file locally

            PersistResultsXML(Globals.Device);

            Refresh();

            DoLog("*** Complete ***");
            Application.DoEvents();

            Cursor.Current = originalCursor;
        }

        void testScript_OnUpdateUI(object sender, string info)
        {
            DoLog(info);
        }

        private void TextBoxEndUserSerialKeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBoxEndUserSerial.Text.Length == 0 || e.KeyChar != '\r')
                return;

            // Got a new customer serial number...
            buttonCheckSerial.Enabled = false;
            ButtonGetSerialNumbersClick();
            buttonCheckSerial.Enabled = true;
        }

        private void ButtonCheckSerialClick(object sender, EventArgs e)
        {
            if (textBoxEndUserSerial.Text.Length == 0)
                return;

            // Got a new customer serial number...
            buttonCheckSerial.Enabled = false;
            ButtonGetSerialNumbersClick();
            buttonCheckSerial.Enabled = true;
        }

        /// <summary>
        /// Handle user serial number changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxEndUserSerialTextChanged(object sender, EventArgs e)
        {
            pictureBoxRunTest.Image = imageList1.Images["cross"];
            buttonGo.Enabled = false;

            // AJL - store the number to make sure this is used for local testing
            Globals.CustomerSerialNumber = textBoxEndUserSerial.Text;
        }

        /// <summary>
        /// Store test results to local XML file
        /// </summary>
        /// <param name="device"></param>
        private static void PersistResultsXML(Device device)
        {
            var strDoc = new StringBuilder();
            
            strDoc.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n\r\n");

            strDoc.Append("<device>\r\n");
            strDoc.Append("  <creationdate>" + device.CreationDate + "</creationdate>\r\n"); 
            strDoc.Append("  <creator>" + XmlTextEncoder.Encode(device.Creator.Forename + " " + device.Creator.Surname) + "</creator>\r\n");
            strDoc.Append("  <customerserialnumber>" + XmlTextEncoder.Encode(device.CustomerSerialNumber) + "</customerserialnumber>\r\n");
            strDoc.Append("  <deviceclass>" + XmlTextEncoder.Encode(device.DeviceClass.Name) + "</deviceclass>\r\n");
            strDoc.Append("  <lasttestdate>" + XmlTextEncoder.Encode(device.LastTestDate.ToString()) + "</lasttestdate>\r\n");
            strDoc.Append("  <lasttester>" + XmlTextEncoder.Encode(device.LastTester.Forename + " " + device.LastTester.Surname) + "</lasttester>\r\n");
            strDoc.Append("  <producerserialnumber>" + XmlTextEncoder.Encode(device.ProducerSerialNumber) + "</producerserialnumber>\r\n");

            strDoc.Append("  <testlistresults>\r\n");

            var tl = device.TestListResults[device.TestListResults.Count - 1];

            //foreach (var tl in device.TestListResults)
            {
                strDoc.Append("    <testlistresult>\r\n");
                strDoc.Append("      <creationdate>" + XmlTextEncoder.Encode(tl.CreationDate.ToString()) + "</creationdate>\r\n");
                strDoc.Append("      <result>" + XmlTextEncoder.Encode(tl.Result.ToString()) + "</result>\r\n");
                strDoc.Append("      <testlist>" + XmlTextEncoder.Encode(tl.TestList.Name) + "</testlist>\r\n");
                strDoc.Append("      <testlocation>" + XmlTextEncoder.Encode(tl.TestLocation.Name) + "</testlocation>\r\n");
                strDoc.Append("      <tester>" + XmlTextEncoder.Encode(tl.Employee.Forename + " " + tl.Employee.Surname) + "</tester>\r\n");
                strDoc.Append("      <company>" + XmlTextEncoder.Encode(tl.Employee.Company.Name) + "</company>\r\n");

                strDoc.Append("      <testitemresults>\r\n");
                foreach (var ti in tl.TestItemResults)
                {
                    strDoc.Append("        <testitemresult>\r\n");
                    strDoc.Append("          <creationdate>" + XmlTextEncoder.Encode(ti.CreationDate.ToString()) + "</creationdate>\r\n");
                    strDoc.Append("          <name>" + XmlTextEncoder.Encode(ti.TestItem.Name) + "</name>\r\n");
                    strDoc.Append("          <result>" + XmlTextEncoder.Encode(ti.Result.ToString()) + "</result>\r\n");
                    strDoc.Append("          <notes>" + XmlTextEncoder.Encode(ti.Notes) + "</notes>\r\n");
                    strDoc.Append("        </testitemresult>\r\n");
                }
                strDoc.Append("      </testitemresults>\r\n");
                strDoc.Append("    </testlistresult>\r\n");
            }

            strDoc.Append("  </testlistresults>\r\n");

            strDoc.Append("</device>\r\n");

            using (var fs = new FileStream(Globals.LocalResultFile, FileMode.OpenOrCreate))
            {
                var writer = new StreamWriter(fs);

                writer.Write(strDoc.ToString());

                writer.Close();
            }
        }

        private void DownloadTests()
        {
            DoLog("Initialising tests");

            // Check cache exists            
            if (!Directory.Exists(Globals.CacheFolder))
                Directory.CreateDirectory(Globals.CacheFolder);

            // Need to get test archive and script here...
            DoLog("- Checking cache for: " + Globals.DeviceClass.SoftwareURL);
            if (!File.Exists(Globals.SoftwareArchiveFileName))
            {
                var uri = new Uri(Globals.DeviceClass.SoftwareURL);
                string scheme = uri.Scheme.ToLower();
                if (!scheme.Equals("ftp"))
                {
                    MessageBoxEx.Show("URI scheme is not supported (" + scheme + ") Contact board vendor");
                    return;
                }
                DoLog("- Downloading " + Globals.SoftwareArchiveFileName + " archive from: " + Globals.DeviceClass.SoftwareURL);
                if (!RemoteFileManager.DownloadFile(uri, Globals.SoftwareArchiveFileName))
                {
                    DoLog("Couldn't download file - please contact board vendor");
                    return;
                }
                DoLog("- Success");
            }
            else
            {
                DoLog("- Using cached file");
            }

            DoLog("- Checking cache for: " + Globals.DeviceClass.ResourcesURL);
            if (!File.Exists(Globals.ResourcesArchiveFileName))
            {
                var uri = new Uri(Globals.DeviceClass.ResourcesURL);
                string scheme = uri.Scheme.ToLower();
                if (!scheme.Equals("ftp"))
                {
                    MessageBoxEx.Show("URI scheme is not supported (" + scheme + ") Contact board vendor");
                    return;
                }
                DoLog("- Downloading " + Globals.ResourcesArchiveFileName + " archive from: " + Globals.DeviceClass.ResourcesURL);
                if (!RemoteFileManager.DownloadFile(uri, Globals.ResourcesArchiveFileName))
                {
                    DoLog("Couldn't download file - please contact board vendor");
                    return;
                }

                // Untar/Extract

                if (Globals.ResourcesArchiveFileName.ToLower().EndsWith(".tgz") || Globals.ResourcesArchiveFileName.ToLower().EndsWith(".tar.gz"))
                {
                    bool stat1 = ArchiveHelper.ExtractTgz(Globals.ResourcesArchiveFileName);
                    if (!stat1)
                    {
                        DoLog("Problem extracting archive " + Globals.ResourcesArchiveFileName + ". Please contact board vendor");
                        return;
                    }
                }
                DoLog("- Success");
            }
            else
            {
                DoLog("- Using cached file");
            }

            DoLog("- Checking cache for: " + Globals.DeviceClass.ScriptURL);
            if (!File.Exists(Globals.ScriptFileName))
            {
                var uri= new Uri(Globals.DeviceClass.ScriptURL);
                string scheme = uri.Scheme.ToLower();
                if (!scheme.Equals("ftp"))
                {
                    MessageBoxEx.Show("URI scheme is not supported (" + scheme + ") Contact board vendor");
                    return;
                }
                DoLog("- Downloading control script from: " + Globals.DeviceClass.ScriptURL);
                if (!RemoteFileManager.DownloadFile(uri, Globals.ScriptFileName))
                {
                    DoLog("Couldn't download file - please contact board vendor");
                    return;
                }
                DoLog("- Success");
            }
            else
            {
                DoLog("- Using cached file");
            }

            _bTestsDownloaded = true;
        }

        private void DoWork(bool runAllTests, int testNum)
        {
            if (!_bTestsDownloaded)
                DownloadTests();

            //
            // Scripting
            //
            Script script;
            try
            {
                script = new Script(Globals.ScriptFileName);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("Problem with test script' " + Globals.ScriptFileName + "'. Contact board vendor.");
                Logger.Warn("Exception loading test script: " + ex.Message);
                return;
            }

            // Setup SSH connection for tests
            if (Client != null)
            {
                try
                {
                    if (!Client.IsConnected)
                    {
                        try
                        {
                            Client.Dispose();
                        }
                        catch
                        {
                        }
                        Client = null;
                    }
                } catch(ObjectDisposedException)
                {
                    Client = null;
                }
            }

            if (Client == null)
            {
                Client = new SshClient(textBoxIPAddress.Text, textBoxSSHLogin.Text, textBoxSSHPassword.Text);
                Client.Connect();
            }

            // AJL - We seem to have a problem with the initialisation scripts. Add a short delay to see if
            //       we need to wait after connecting
            Thread.Sleep(1000);

            bool stat;
            string output;

            if (!_bTestsInitialisedOnTarget)
            {
                DoLog("Performing pre-test initialisation");
                toolStripStatusLabel1.Text = "Performing pre-test initialisation";
                script.Initialisation.SshClient = Client;
                script.Initialisation.OnUpdateUI -= testScript_OnUpdateUI;
                script.Initialisation.OnUpdateUI += testScript_OnUpdateUI;

                stat = script.Initialisation.Execute(textBoxIPAddress.Text, textBoxSSHLogin.Text,
                                                     textBoxSSHPassword.Text, out output);

                if (!stat)
                {
                    toolStripStatusLabel1.Text = "Initialisation commands failed!";
                    DoLog("Test initialisation commands failed. Contact board vendor. ");
                    MessageBoxEx.Show("Initialisation commands failed. Contact board vendor.");
                    return;
                }

                _bTestsInitialisedOnTarget = true;
            }

            // Run commands...
            if (runAllTests)
            {
                RunTests(script, Client);
            }
            else
            {                
                // Run single test
                RunLocalTest(script, Client, testNum);
            }

            if (runAllTests)
            {
                DoLog("Performing post-test cleanup commands");
                toolStripStatusLabel1.Text = "Performing post-test cleanup commands";

                script.End.SshClient = Client;
                script.Initialisation.OnUpdateUI -= testScript_OnUpdateUI;
                script.Initialisation.OnUpdateUI += testScript_OnUpdateUI;
                stat = script.End.Execute(textBoxIPAddress.Text, textBoxSSHLogin.Text, textBoxSSHPassword.Text,
                                          out output);
                if (!stat)
                {
                    toolStripStatusLabel1.Text = "Post-test cleanup commands failed!";
                    DoLog("Post-test cleanup commands failed. Contact board vendor.");
                    MessageBoxEx.Show("Post-test cleanup commands failed. Contact board vendor.");
                }
                else
                {
                    toolStripStatusLabel1.Text = "Completed";
                    DoLog("Completed");
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Completed";
                DoLog("Completed");
            }
                
            try
            {
                if (Client.IsConnected)
                    Client.Disconnect();
                Client.Dispose();
            }
            catch (Exception e)
            {
                Logger.Warn("Exception disposing client: " + e.Message);
            }
        }

        private void UseDevDatabaseToolStripMenuItemClick(object sender, EventArgs e)
        {
            var cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            MainFormLoad(this, null);

            Cursor.Current = cursor;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check this is the "execute" check button
            if(e.ColumnIndex == dataGridView1.Columns["Execute"].Index)
            {
                int testNum = e.RowIndex;

                Application.DoEvents();

                DoWork(false, testNum);
            }
        }

        private void CheckBoxLocalTestCheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["Execute"].Visible = checkBoxLocalTest.Checked;

            // Reset statusses
            foreach (var testItem in Globals.TestList.TestItems)
                testItem.Status = !testItem.Enabled ? EnumTestStatus.Disabled : EnumTestStatus.NotStarted;
            testItemBindingSource.DataSource = Globals.TestList.TestItems;
        }

        private void clearCacheFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dr = MessageBoxEx.Show("Are you sure?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr != DialogResult.OK)
                return;

            if(Directory.Exists(Globals.CacheFolder))
            {
                var files = Directory.GetFiles(Globals.CacheFolder);
                foreach(var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    } catch
                    {
                    }
                }
            }

        }

    }
}
