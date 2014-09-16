using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class TestList
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<TestItem> _testItems = new HashedSet<TestItem>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Name of test list 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Device Class this test list is implemented against      
        /// </summary>
        public virtual DeviceClass DeviceClass { get; set; }

        public virtual List<TestItem> TestItems
        {
            get
            {
                return new List<TestItem>(_testItems);                
            }
        }

        /// <summary>
        /// Description of this test list
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Creation date/time of this object
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Whether this test list is enabled
        /// </summary>
        public virtual bool Enabled { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
