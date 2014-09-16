using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class Employee
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<Device> _devicesCreated = new HashedSet<Device>();
// ReSharper restore FieldCanBeMadeReadOnly.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<Device> _devicesTested = new HashedSet<Device>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public virtual string Forename { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public virtual string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Company role number / employee id
        /// </summary>
        public virtual string RoleNumber { get; set; }

        /// <summary>
        /// Direct phone number
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// Direct email address
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Line manager of this employee
        /// </summary>
        public virtual Employee Manager { get; set; }

        /// <summary>
        /// Employer
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Whether this test list is enabled
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// Devices created 
        /// </summary>
        public virtual List<Device> DevicesCreated
        {
            get
            {
                return new List<Device>(_devicesCreated);
            }
        }

        /// <summary>
        /// Devices tested
        /// </summary>
        public virtual List<Device> DevicesTested
        {
            get
            {
                return new List<Device>(_devicesTested);
            }
        }

        public override string ToString()
        {
            return Forename + "," + Surname + " (" + Email + ")";
        }

    }
}
