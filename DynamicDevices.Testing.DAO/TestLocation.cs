namespace DynamicDevices.Testing.DAO
{
    public class TestLocation
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Identifying name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Company within which location exists
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// General notes
        /// </summary>
        public virtual string Notes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
