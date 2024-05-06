namespace Models.Request
{
    public class CompanySearchRequest
    {
        public CompanySearchRequest(int companyId)
        {
            CompanyId = companyId;
        }

        public int CompanyId { get; set; }
    }
}
