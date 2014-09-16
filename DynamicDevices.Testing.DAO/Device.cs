using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class Device
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<TestListResult> _testListResults = new HashedSet<TestListResult>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Internal producer serial number
        /// </summary>
        public virtual string ProducerSerialNumber { get; set; }

        /// <summary>
        /// External customer serial number (if applicable)
        /// </summary>
        public virtual string CustomerSerialNumber { get; set; }

        /// <summary>
        /// Class of device 
        /// </summary>
        public virtual DeviceClass DeviceClass { get; set; }

        /// <summary>
        /// Creation date/time of this object
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Who created this device in the system? (i.e. who was testing it)
        /// </summary>
        public virtual Employee Creator { get; set; }

        /// <summary>
        /// Last person to test
        /// </summary>
        public virtual Employee LastTester { get; set; }

        /// <summary>
        /// Date/time of last test
        /// </summary>
        public virtual DateTime LastTestDate { get; set; }

        /// <summary>
        /// Test lists
        /// </summary>
        public virtual List<TestListResult> TestListResults
        {
            get
            {
                return new List<TestListResult>(_testListResults);
            }
        }

        public override string ToString()
        {
            return ProducerSerialNumber;
        }
    }
}
