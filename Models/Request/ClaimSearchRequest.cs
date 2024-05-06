namespace Models.Request
{
    public class ClaimSearchRequest
    {
        public ClaimSearchRequest(int claimId)
        {
            ClaimId = claimId;
        }

        public int ClaimId { get; set; }
    }
}
