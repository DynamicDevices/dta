using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class DeviceClass
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<TestList> _testLists = new HashedSet<TestList>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Name of class
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Default host/ip address for device
        /// </summary>
        public virtual string DefaultHost { get; set; }

        /// <summary>
        /// Default SSH/SCP login to use to provision/run test to/on device
        /// </summary>
        public virtual string DefaultLogin { get; set; }

        /// <summary>
        /// Default SSH/SCP password to use to provision/run test to/on device
        /// </summary>
        public virtual string DefaultPassword { get; set; }

        /// <summary>
        /// URL from which software archive is downloaded
        /// </summary>
        public virtual string SoftwareURL { get; set; }

        /// <summary>
        /// URL from which software test script is downloaded
        /// </summary>
        public virtual string ScriptURL { get; set; }

        /// <summary>
        /// URL to which to send results files
        /// </summary>
        public virtual string ResultsURL { get; set; }

        /// <summary>
        /// URL from which to retrieve resources files
        /// </summary>
        public virtual string ResourcesURL { get; set; }

        /// <summary>
        /// Description of class, 
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Last producer serial number used
        /// </summary>
        public virtual string LastProducerSerial { get; set; }

        /// <summary>
        /// Last customer serial used
        /// </summary>
        public virtual string LastCustomerSerial { get; set; }

        /// <summary>
        /// Maximum producer serial that can be used within this device class
        /// </summary>
        public virtual string MinProducerSerial { get; set; }

        /// <summary>
        /// Minimum customer serial that can be used within this device class
        /// </summary>
        public virtual string MinCustomerSerial { get; set; }

        /// <summary>
        /// Maximum producer serial that can be used within this device class
        /// </summary>
        public virtual string MaxProducerSerial { get; set; }

        /// <summary>
        /// Minimum customer serial that can be used within this device class
        /// </summary>
        public virtual string MaxCustomerSerial { get; set; }

        /// <summary>
        /// Test lists
        /// </summary>
        public virtual List<TestList> TestLists
        {
            get
            {
                return new List<TestList>(_testLists);
            }
        }

        /// <summary>
        /// Whether this device class is enabled
        /// </summary>
        public virtual bool Enabled { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
