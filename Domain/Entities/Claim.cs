namespace Domain.Entities
{
    public class Claim
    {
        public int Id { get; set; }
        public string Ucr { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int ClaimTypeId { get; set; }

        public virtual ClaimType ClaimType { get; set; }
        public virtual Company Company { get; set; }
    }
}
