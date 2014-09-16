namespace DynamicDevices.Testing.DAO
{
    public class TestItem
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Name of this test
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Ordering within list to run test
        /// </summary>
        public virtual int ExecutionOrder { get; set; }

        /// <summary>
        /// Description of this test
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Test list owning this test
        /// </summary>
        public virtual TestList TestList { get; set; }

        /// <summary>
        /// Is this test enabled?
        /// </summary>
        public virtual bool Enabled { get; set; }

        // Not mapped
        public virtual EnumTestStatus Status { get; set; }


        public override string ToString()
        {
            return Name + " (" + Description + ")";
        }
    }
}
