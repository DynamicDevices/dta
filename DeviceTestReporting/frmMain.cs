using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Be.Timvw.Framework.ComponentModel;
using log4net;
using NHibernate;
using NHibernate.Criterion;
using DynamicDevices.Testing.DAO;

namespace DeviceTestReporting
{
    public partial class FrmMain : Form
    {
        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected static NHibernate.Cfg.Configuration NhibernateCfg;
        protected static ISessionFactory Nhibernatefactory;
        protected ISession Session;

        protected SortableBindingList<Device> CollDevices;
        protected SortableBindingList<TestListResult> CollTestListResults;
        protected SortableBindingList<TestItemResult> CollTestItemResults;
        protected SortableBindingList<Employee> CollEmployees;

        public SortableBindingList<Device> Devices { get { return CollDevices; } }

        private readonly frmLoading _frmLoading;
            
        public FrmMain()
        {
            InitializeComponent();

            _frmLoading = new frmLoading();
            _frmLoading.Show();
            _frmLoading.BringToFront();
            
            Application.DoEvents();
        }

        /// <summary>
        /// Setup NHibernate and connect to the database
        /// </summary>
        /// <returns></returns>
        private void InitialiseDB()
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
            }
            catch (Exception e)
            {
                Logger.Warn("Exception bringing up NHibernate: " + e.Message);
            }
            return;
        }

        private void FrmMainLoad(object sender, EventArgs e)
        {
            try
            {
                InitialiseDB();

                Cursor c = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                ICriteria crit = Session.CreateCriteria(typeof (Device));
                CollDevices = new SortableBindingList<Device>(crit.List<Device>());
                dataGridViewDevice.DataSource = CollDevices;

                foreach (DataGridViewColumn column in dataGridViewDevice.Columns)
                    column.SortMode = DataGridViewColumnSortMode.Automatic;

                crit = Session.CreateCriteria(typeof (Employee));
                CollEmployees = new SortableBindingList<Employee>(crit.List<Employee>());
                comboBoxUsers.DataSource = CollEmployees;

                Cursor.Current = c;
            } catch(Exception ex)
            {
                Logger.Warn("Exception in loading: " + ex.Message);
            }

            _frmLoading.Close();
        }

        private void DataGridViewDeviceSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDevice.CurrentRow == null)
                return;

            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            
            var id = (int)dataGridViewDevice.CurrentRow.Cells[0].Value;

            foreach(var d in CollDevices)
            {
                if (d.ID == id)
                {
                    dataGridViewTestItemResult.ClearSelection();

                    var crit = Session.CreateCriteria(typeof (TestListResult));
                    crit.Add(Restrictions.Eq("Device", d));
                    CollTestListResults = new SortableBindingList<TestListResult>(crit.List<TestListResult>());
                    dataGridViewTestListResult.DataSource = CollTestListResults;
                    break;
                }
            }

            Cursor.Current = c;
        }

        private void DataGridViewTestListResultSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTestListResult.CurrentRow == null)
                return;

            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            var id = (int)dataGridViewTestListResult.CurrentRow.Cells[0].Value;

            foreach (var tlr in CollTestListResults )
            {
                if (tlr.ID == id)
                {
                    var crit = Session.CreateCriteria(typeof(TestItemResult));
                    crit.Add(Restrictions.Eq("TestListResult", tlr));
                    CollTestItemResults = new SortableBindingList<TestItemResult>(crit.List<TestItemResult>());
                    dataGridViewTestItemResult.DataSource = CollTestItemResults;
                    break;
                }
            }

            Cursor.Current = c;
        }

        private void RefreshToolStripMenuItemRefreshClick(object sender, EventArgs e)
        {
            Cursor c = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            ICriteria crit = Session.CreateCriteria(typeof(Device));
            CollDevices = new SortableBindingList<Device>(crit.List<Device>());
            dataGridViewDevice.DataSource = CollDevices;

            Cursor.Current = c;
        }

        private void ExitToolStripMenuItemExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UseDevDatabaseToolStripMenuItemCheckedChanged(object sender, EventArgs e)
        {
           FrmMainLoad(this, null);
        }

        private void ComboBoxUsersSelectedIndexChanged(object sender, EventArgs e)
        {

            var employee = CollEmployees[comboBoxUsers.SelectedIndex];
            
            var crit = Session.CreateCriteria(typeof(Device)).Add(Restrictions.Eq("LastTester", employee));

            CollDevices = new SortableBindingList<Device>(crit.List<Device>());
            dataGridViewDevice.DataSource = CollDevices;

        }
    }
}
