namespace DynamicDevices.Testing.DAO
{
    public class Address
    {
        /// <summary>
        /// Unique ID
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// Address Line 1
        /// </summary>
        public virtual string Address1 { get; set; }

        /// <summary>
        /// Address Line 2
        /// </summary>
        public virtual string Address2 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Area
        /// </summary>

        public virtual string Area { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public virtual string Country{ get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        public virtual string PostCode{ get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email { get; set; }

        public override string ToString()
        {
            return Address1 + "," + Address2 + "," + City + "," + Area + "," + PostCode + "," + Country + "," + Phone + "," + Fax + "," + Email;
        }
    }
}
