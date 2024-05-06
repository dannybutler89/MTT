namespace Domain.Entities
{
    public class ClaimType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Claim> Claims { get; set; }
    }
}
