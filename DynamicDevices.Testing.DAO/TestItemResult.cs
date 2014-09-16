using System;

namespace DynamicDevices.Testing.DAO
{
    public class TestItemResult
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Test item for which this is a result
        /// </summary>
        public virtual TestItem TestItem { get; set; }

        /// <summary>
        /// Result list that owns this result
        /// </summary>
        public virtual TestListResult TestListResult { get; set; }

        /// <summary>
        /// Device under test
        /// </summary>
        public virtual Device Device { get; set; }
    
        /// <summary>
        /// Success/Failure    
        /// </summary>
        public virtual bool Result { get; set; }

        /// <summary>
        /// Notes relating to test
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Creation date/time of this object
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        public override string ToString()
        {
            var s = Result.ToString();
            if (!String.IsNullOrEmpty(Notes))
                s += " (" + Notes + ")";
            return s;
        }
    }
}

