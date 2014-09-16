using System;
using System.Windows.Forms;
using System.Threading;
using NHibernate;

using DynamicDevices.Testing.DAO;

namespace DeviceTestApplication
{
    public partial class frmAdmin : Form
    {
        public ISession Session { get; set; }
        private Thread t;
        private Cursor cursor;

        public frmAdmin()
        {
            InitializeComponent();
        }

        private void buttonInitialiseData_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            timer1.Enabled = true;
            cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            t = new Thread(new ThreadStart(delegate
                                                      {
                                                          try
                                                          {

                                                              var address1 = new Address
                                                                                 {
                                                                                     Address1 = "Address 1",
                                                                                     Address2 = "Address 2",
                                                                                     Area = "Area",
                                                                                     City = "City",
                                                                                     Country = "UK",
                                                                                     Email = "info@dynamicdevices.co.uk",
                                                                                     Fax = "",
                                                                                     Phone = "",
                                                                                     PostCode = ""
                                                                                 };

                                                              var address2 = new Address
                                                              {
                                                                  Address1 = "Address 1",
                                                                  Address2 = "Address 2",
                                                                  Area = "Area",
                                                                  City = "City",
                                                                  Country = "UK",
                                                                  Email = "info@dynamicdevices.co.uk",
                                                                  Fax = "",
                                                                  Phone = "",
                                                                  PostCode = ""
                                                              };

                                                              var address3 = new Address
                                                              {
                                                                  Address1 = "Address 1",
                                                                  Address2 = "Address 2",
                                                                  Area = "Area",
                                                                  City = "City",
                                                                  Country = "UK",
                                                                  Email = "info@dynamicdevices.co.uk",
                                                                  Fax = "",
                                                                  Phone = "",
                                                                  PostCode = ""
                                                              };

                                                              var company1 = new Company
                                                                                 {
                                                                                     Name = "Dynamic Devices Ltd",
                                                                                     Notes = "Some notes...",
                                                                                     Address = address1
                                                                                 };
                                                              var company2 = new Company
                                                                                 {
                                                                                     Name = "Dynamic Devices Ltd",
                                                                                     Notes = "Some notes...",
                                                                                     Address = address1
                                                                                 };

                                                              var company3 = new Company
                                                              {
                                                                  Name = "Dynamic Devices Ltd",
                                                                  Notes = "Some notes...",
                                                                  Address = address1
                                                              };

                                                              var employee1 = new Employee
                                                                                  {
                                                                                      Enabled = true,
                                                                                      Company = company1,
                                                                                      Email =
                                                                                          "ajlennon@dynamicdevices.co.uk",
                                                                                      Forename = "Alex",
                                                                                      Surname = "Lennon",
                                                                                      Login = "ajlennon",
                                                                                      Password = "1234",
                                                                                      Phone = "",
                                                                                      RoleNumber = "001"
                                                                                  };

                                                              var employee2 = new Employee
                                                                                  {
                                                                                      Enabled = true,
                                                                                      Company = company1,
                                                                                      Email =
                                                                                          "ajlennon@dynamicdevices.co.uk",
                                                                                      Forename = "Alex",
                                                                                      Surname = "Lennon",
                                                                                      Login = "ajlennon",
                                                                                      Password = "1234",
                                                                                      Phone = "",
                                                                                      RoleNumber = "001"
                                                                                  };

                                                              var employee3 = new Employee
                                                              {
                                                                  Enabled = true,
                                                                  Company = company1,
                                                                  Email =
                                                                      "ajlennon@dynamicdevices.co.uk",
                                                                  Forename = "Alex",
                                                                  Surname = "Lennon",
                                                                  Login = "ajlennon",
                                                                  Password = "1234",
                                                                  Phone = "",
                                                                  RoleNumber = "001"
                                                              };

                                                              var employee4 = new Employee
                                                              {
                                                                  Enabled = true,
                                                                  Company = company1,
                                                                  Email =
                                                                      "ajlennon@dynamicdevices.co.uk",
                                                                  Forename = "Alex",
                                                                  Surname = "Lennon",
                                                                  Login = "ajlennon",
                                                                  Password = "1234",
                                                                  Phone = "",
                                                                  RoleNumber = "001"
                                                              };

                                                              var deviceClass1 = new DeviceClass
                                                                                    {
                                                                                      Enabled = true,
                                                                                      DefaultHost = "192.168.1.2",
                                                                                      DefaultLogin = "root",
                                                                                      DefaultPassword = "password",
                                                                                      Description = "A board",
                                                                                      Name = "Board v1.x",
                                                                                      ScriptURL =
                                                                                      "ftp://user:password@server/path/files/demotestscript-1.0.xml",
                                                                                      SoftwareURL =
                                                                                      "ftp://user:password@server/path/files/demotestarchive-1.0.xml",
                                                                                      ResultsURL =
                                                                                      "ftp://user:password@server/path/results",
                                                                                      ResourcesURL =
                                                                                      "ftp://user:password@server/path/files/demoresources-1.0.xml",
                                                                                      MinCustomerSerial = "1",
                                                                                      MinProducerSerial = "1",
                                                                                    };

                                                              var deviceClass2 = new DeviceClass
                                                              {
                                                                  Enabled = true,
                                                                  DefaultHost = "192.168.1.2",
                                                                  DefaultLogin = "root",
                                                                  DefaultPassword = "password",
                                                                  Description = "A board (dev)",
                                                                  Name = "Board v1.x (dev)",
                                                                  ScriptURL =
                                                                  "ftp://user:password@server/path/files/demotestscript-1.0.xml",
                                                                  SoftwareURL =
                                                                  "ftp://user:password@server/path/files/demotestarchive-1.0.xml",
                                                                  ResultsURL =
                                                                  "ftp://user:password@server/path/results",
                                                                  ResourcesURL =
                                                                  "ftp://user:password@server/path/files/demoresources-1.0.xml",
                                                                  MinCustomerSerial = "1",
                                                                  MinProducerSerial = "1",
                                                              };

                                                              var testList1 = new TestList
                                                              {
                                                                  CreationDate = DateTime.Now,
                                                                  Enabled = true,
                                                                  Name = "Board Production Tests v1.0",
                                                                  Description =
                                                                      "Initial set of board tests",
                                                                  DeviceClass =  deviceClass1
                                                              };

                                                              var testList2 = new TestList
                                                              {
                                                                  CreationDate = DateTime.Now,
                                                                  Enabled = true,
                                                                  Name = "Board Tests (Development) v1.0",
                                                                  Description =
                                                                      "Initial set of board tests (dev)",
                                                                  DeviceClass = deviceClass2
                                                              };

                                                              var test1 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 1,
                                                                  Name = "Serial Number",
                                                                  Description =
                                                                      "Auto programs next serial number available or set.",
                                                                  TestList = testList1
                                                              };
                                                              var test2 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 2,
                                                                  Name = "USB Device",
                                                                  Description =
                                                                      "SSH/FTP, check function",
                                                                  TestList = testList1
                                                              };
                                                              var test3 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 3,
                                                                  Name = "OLED Display",
                                                                  Description =
                                                                      "Display test screen, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test4 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 4,
                                                                  Name = "Backlight",
                                                                  Description =
                                                                      "Dim display to off, set back to full, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test5 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 5,
                                                                  Name = "Keypad Left",
                                                                  Description =
                                                                      "Check each button functions, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test6 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 6,
                                                                  Name = "Keypad Right",
                                                                  Description =
                                                                      "Check each button functions, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test7 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 7,
                                                                  Name = "Keypad Backlight",
                                                                  Description =
                                                                      "Set LED backlighting to full, dim to off, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test8 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 8,
                                                                  Name = "Audio",
                                                                  Description =
                                                                      "Play test wav file at full volume, set volume to quiet, set volume back to full, ask operator to confirm by button press.",
                                                                  TestList = testList1
                                                              };
                                                              var test9 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 9,
                                                                  Name = "Accelerometer",
                                                                  Description =
                                                                      "Check communications with accelermoeter, as operator to move device, if movement in any direction then pass.",
                                                                  TestList = testList1
                                                              };
                                                              var test10 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 10,
                                                                  Name = "RTC",
                                                                  Description =
                                                                      "Check can read RTC, if can read then pass and move on to next test.",
                                                                  TestList = testList1
                                                              };
                                                              var test11 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 11,
                                                                  Name = "Power Management Charging",
                                                                  Description =
                                                                      "Check device is charging. Ask tester to determine charge current is correct. Store current/voltage",
                                                                  TestList = testList1
                                                              };
                                                              var test12 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 12,
                                                                  Name = "GPS Data",
                                                                  Description =
                                                                      "Check NMEA data present, if can read then pass.",
                                                                  TestList = testList1
                                                              };
                                                              var test13 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 13,
                                                                  Name = "GPS Lock",
                                                                  Description =
                                                                      "Check GPS gets lock, if lock then pass",
                                                                  TestList = testList1
                                                              };
                                                              var test14 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 14,
                                                                  Name = "GSM +CSQ",
                                                                  Description =
                                                                      "Check can talk to GSM modem, check CSQ level, setattable pass level of 1 - 32 if higher than that set then pass.",
                                                                  TestList = testList1
                                                              };
                                                              var test15 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 15,
                                                                  Name = "GSM Audio",
                                                                  Description =
                                                                      "Check audio call mic input and speaker output.",
                                                                  TestList = testList1
                                                              };
                                                              var test16 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 16,
                                                                  Name = "GSM SIM IMSI",
                                                                  Description =
                                                                      "Check SIM card present, read IMSI number and record.",
                                                                  TestList = testList1
                                                              };

                                                              var test17 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 17,
                                                                  Name = "GSM SIM IMEI",
                                                                  Description =
                                                                      "Check SIM card present, read IMEI number and record.",
                                                                  TestList = testList1
                                                              };

                                                              var test18= new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 18,
                                                                  Name = "GSM SIM ICCID",
                                                                  Description =
                                                                      "Check SIM card present, read ICCID number and record",
                                                                  TestList = testList1
                                                              };

                                                              var test19 = new TestItem
                                                              {
                                                                  Enabled = false,
                                                                  ExecutionOrder = 19,
                                                                  Name = "Debug Port Output",
                                                                  Description =
                                                                      "Test debug serial port output working.",
                                                                  TestList = testList1
                                                              };

                                                              var testD1 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 1,
                                                                  Name = "Serial Number",
                                                                  Description =
                                                                      "Auto programs next serial number available or set.",
                                                                  TestList = testList2
                                                              };
                                                              var testD2 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 2,
                                                                  Name = "USB Device",
                                                                  Description =
                                                                      "SSH/FTP, check function",
                                                                  TestList = testList2
                                                              };
                                                              var testD3 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 3,
                                                                  Name = "OLED Display",
                                                                  Description =
                                                                      "Display test screen, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD4 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 4,
                                                                  Name = "Backlight",
                                                                  Description =
                                                                      "Dim display to off, set back to full, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD5 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 5,
                                                                  Name = "Keypad Left",
                                                                  Description =
                                                                      "Check each button functions, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD6 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 6,
                                                                  Name = "Keypad Right",
                                                                  Description =
                                                                      "Check each button functions, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD7 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 7,
                                                                  Name = "Keypad Backlight",
                                                                  Description =
                                                                      "Set LED backlighting to full, dim to off, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD8 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 8,
                                                                  Name = "Audio",
                                                                  Description =
                                                                      "Play test wav file at full volume, set volume to quiet, set volume back to full, ask operator to confirm by button press.",
                                                                  TestList = testList2
                                                              };
                                                              var testD9 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 9,
                                                                  Name = "Accelerometer",
                                                                  Description =
                                                                      "Check communications with accelermoeter, as operator to move device, if movement in any direction then pass.",
                                                                  TestList = testList2
                                                              };
                                                              var testD10 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 10,
                                                                  Name = "RTC",
                                                                  Description =
                                                                      "Check can read RTC, if can read then pass and move on to next test.",
                                                                  TestList = testList2
                                                              };
                                                              var testD11 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 11,
                                                                  Name = "Power Management Charging",
                                                                  Description =
                                                                      "Check device is charging. Ask tester to determine charge current is correct. Store current/voltage",
                                                                  TestList = testList2
                                                              };
                                                              var testD12 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 12,
                                                                  Name = "GPS Data",
                                                                  Description =
                                                                      "Check NMEA data present, if can read then pass.",
                                                                  TestList = testList2
                                                              };
                                                              var testD13 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 13,
                                                                  Name = "GPS Lock",
                                                                  Description =
                                                                      "Check GPS gets lock, if lock then pass",
                                                                  TestList = testList2
                                                              };
                                                              var testD14 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 14,
                                                                  Name = "GSM +CSQ",
                                                                  Description =
                                                                      "Check can talk to GSM modem, check CSQ level, setattable pass level of 1 - 32 if higher than that set then pass.",
                                                                  TestList = testList2
                                                              };
                                                              var testD15 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 15,
                                                                  Name = "GSM Audio",
                                                                  Description =
                                                                      "Check audio call mic input and speaker output.",
                                                                  TestList = testList2
                                                              };
                                                              var testD16 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 16,
                                                                  Name = "GSM SIM IMSI",
                                                                  Description =
                                                                      "Check SIM card present, read IMSI number and record.",
                                                                  TestList = testList2
                                                              };

                                                              var testD17 = new TestItem
                                                              {
                                                                  Enabled = true,
                                                                  ExecutionOrder = 17,
                                                                  Name = "GSM SIM IMEI",
                                                                  Description =
                                                                      "Check SIM card present, read IMEI number and record.",
                                                                  TestList = testList2
                                                              };

                                                              var testD18 = new TestItem
                                                              {
                                                                  Enabled = false,
                                                                  ExecutionOrder = 18,
                                                                  Name = "Debug Port Output",
                                                                  Description =
                                                                      "Test debug serial port output working.",
                                                                  TestList = testList2
                                                              };

                                                              var testLocation1 = new TestLocation
                                                                                      {
                                                                                          Company = company1,
                                                                                          Name = "Dynamic Devices Testing"
                                                                                      };
                                                              var testLocation2 = new TestLocation
                                                                                      {
                                                                                          Company = company2,
                                                                                          Name = "Dynamic Devices Testing 2"
                                                                                      };

                                                              var testLocation3 = new TestLocation
                                                              {
                                                                  Company = company3,
                                                                  Name = "Dynamic Devices Testing 3"
                                                              };

                                                              Session.SaveOrUpdate(address1);
                                                              Session.SaveOrUpdate(company1);
                                                              Session.SaveOrUpdate(employee1);
                                                              Session.SaveOrUpdate(testLocation1);
                                                              Session.SaveOrUpdate(address2);
                                                              Session.SaveOrUpdate(company2);
                                                              Session.SaveOrUpdate(employee2);
                                                              Session.SaveOrUpdate(employee3);
                                                              Session.SaveOrUpdate(testLocation2);
                                                              Session.SaveOrUpdate(address3);
                                                              Session.SaveOrUpdate(company3);
                                                              Session.SaveOrUpdate(employee4);
                                                              Session.SaveOrUpdate(testLocation3);
                                                              Session.SaveOrUpdate(employee4);

                                                              Session.SaveOrUpdate(deviceClass1);
                                                              Session.SaveOrUpdate(deviceClass2);
                                                              Session.SaveOrUpdate(testList1);
                                                              Session.SaveOrUpdate(testList2);

                                                              Session.SaveOrUpdate(test1);
                                                              Session.SaveOrUpdate(test2);
                                                              Session.SaveOrUpdate(test3);
                                                              Session.SaveOrUpdate(test4);
                                                              Session.SaveOrUpdate(test5);
                                                              Session.SaveOrUpdate(test6);
                                                              Session.SaveOrUpdate(test7);
                                                              Session.SaveOrUpdate(test8);
                                                              Session.SaveOrUpdate(test9);
                                                              Session.SaveOrUpdate(test10);
                                                              Session.SaveOrUpdate(test11);
                                                              Session.SaveOrUpdate(test12);
                                                              Session.SaveOrUpdate(test13);
                                                              Session.SaveOrUpdate(test14);
                                                              Session.SaveOrUpdate(test15);
                                                              Session.SaveOrUpdate(test16);
                                                              Session.SaveOrUpdate(test17);
                                                              Session.SaveOrUpdate(test18);
                                                              Session.SaveOrUpdate(test19);

                                                              Session.SaveOrUpdate(testD1);
                                                              Session.SaveOrUpdate(testD2);
                                                              Session.SaveOrUpdate(testD3);
                                                              Session.SaveOrUpdate(testD4);
                                                              Session.SaveOrUpdate(testD5);
                                                              Session.SaveOrUpdate(testD6);
                                                              Session.SaveOrUpdate(testD7);
                                                              Session.SaveOrUpdate(testD8);
                                                              Session.SaveOrUpdate(testD9);
                                                              Session.SaveOrUpdate(testD10);
                                                              Session.SaveOrUpdate(testD11);
                                                              Session.SaveOrUpdate(testD12);
                                                              Session.SaveOrUpdate(testD13);
                                                              Session.SaveOrUpdate(testD14);
                                                              Session.SaveOrUpdate(testD15);
                                                              Session.SaveOrUpdate(testD16);
                                                              Session.SaveOrUpdate(testD17);
                                                              Session.SaveOrUpdate(testD18);
                                                              
                                                              Session.Flush();
                                                          }
                                                          catch(Exception ex)
                                                          {
                                                              MessageBox.Show(ex.InnerException.ToString());
                                                          }
                                                      }
                                      ));
            t.Start();
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            if(t.IsAlive)
            {
                progressBar1.Increment(1);
                if (progressBar1.Value == 100)
                    progressBar1.Value = 0;
            }
            else
            {
                progressBar1.Value = 0;
                timer1.Enabled = false;
                Cursor.Current = cursor;
                Close();
            }
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmAdminShown(object sender, EventArgs e)
        {
            // Do auto initialise...
            var criteria = Session.CreateCriteria(typeof(Employee));
            if (criteria.List<Employee>().Count == 0)
                buttonInitialiseData_Click(this, null);
            else
                Close();
        }
    }
}
