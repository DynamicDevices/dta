using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class Company
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<Employee> _employees= new HashedSet<Employee>();
// ReSharper restore FieldCanBeMadeReadOnly.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<TestLocation> _testLocations = new HashedSet<TestLocation>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Employees
        /// </summary>
        public virtual List<Employee> Employees
        {
            get
            {
                return new List<Employee>(_employees);
            }
        }

        /// <summary>
        /// Test locations
        /// </summary>
        public virtual List<TestLocation> TestLocations
        {
            get
            {
                return new List<TestLocation>(_testLocations);
            }
        }

        /// <summary>
        /// Address
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public virtual string Notes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
