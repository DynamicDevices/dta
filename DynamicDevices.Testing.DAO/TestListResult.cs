using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace DynamicDevices.Testing.DAO
{
    public class TestListResult
    {
// ReSharper disable FieldCanBeMadeReadOnly.Local
        private Iesi.Collections.Generic.ISet<TestItemResult> _testItemResults = new HashedSet<TestItemResult>();
// ReSharper restore FieldCanBeMadeReadOnly.Local

        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Employee running test
        /// </summary>
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Location of test (e.g. PC)
        /// </summary>
        public virtual TestLocation TestLocation { get; set; }

        /// <summary>
        /// Test list that was excecuted
        /// </summary>
        public virtual TestList TestList { get; set; }

        /// <summary>
        /// Test list results 
        /// </summary>
        public virtual List<TestItemResult> TestItemResults
        {
            get
            {
                return new List<TestItemResult>(_testItemResults);
            }
        }

        /// <summary>
        /// Device that was tested
        /// </summary>
        public virtual Device Device { get; set; }

        /// <summary>
        /// Overall result of test 
        /// </summary>
        public virtual bool Result { get; set; }

        /// <summary>
        /// URL where more details results are stored (if applicable)
        /// </summary>
        public virtual string ResultURL { get; set; }

        /// <summary>
        /// Test list notes
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Creation date/time of this object
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        public override string ToString()
        {
            var s = Result.ToString();
            if(!String.IsNullOrEmpty(Notes))
                s += " (" + Notes + ")";
            return s;
        }
    }
}
