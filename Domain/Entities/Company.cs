﻿namespace Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public DateTime InsuranceEndDate { get; set; }

        public virtual List<Claim> Claims { get; set; }
    }
}
