namespace Models
{
    public class ClaimResponse
    {
        public int Id { get; set; }
        public string Ucr { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int ClaimAgeDays { get; set; }
        public string ClaimType { get; set; }
    }
}
