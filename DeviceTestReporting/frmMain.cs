using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DeviceTestApplication;
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

        protected IList<Device> CollDevices;
        protected IList<TestListResult> CollTestListResults;
        protected IList<TestItemResult> CollTestItemResults;

        public IList<Device> Devices { get { return CollDevices;  } }

        public FrmMain()
        {
            InitializeComponent();
        
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
                CollDevices = crit.List<Device>();
                dataGridViewDevice.DataSource = CollDevices;

                Cursor.Current = c;
            } catch(Exception ex)
            {
                Logger.Warn("Exception in loading: " + ex.Message);
            }
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
                    CollTestListResults = crit.List<TestListResult>();
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
                    CollTestItemResults = crit.List<TestItemResult>();
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
            CollDevices = crit.List<Device>();
            dataGridViewDevice.DataSource = CollDevices;

            Cursor.Current = c;
        }

        private static void ExitToolStripMenuItemExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UseDevDatabaseToolStripMenuItemCheckedChanged(object sender, EventArgs e)
        {
           FrmMainLoad(this, null);
        }

    }
}
