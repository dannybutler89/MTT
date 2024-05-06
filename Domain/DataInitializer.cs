using Domain.Entities;

namespace Domain
{
    public static class DataInitializer
    {
        public static async Task Initialize(MarkelDbContext context)
        {
            context.Database.EnsureCreated();

            await context.Companies.AddRangeAsync(new List<Company>
            {
                new Company
                {
                    Id = 1,
                    Address1 = "Company 1",
                    Address2 = "Building 1",
                    Address3 = "Street 1",
                    Postcode = "LS1 1SL",
                    Country = "England",
                    Active = true,
                    Name = "Company 1",
                    InsuranceEndDate = DateTime.Today.AddMonths(6),
                    Claims = new List<Claim>
                    {
                        new Claim
                        {
                            Id = 1,
                            ClaimDate = DateTime.Today.AddDays(-5),
                            LossDate = DateTime.Today.AddDays(-10),
                            AssuredName = "Insurance Company",
                            Closed = false,
                            CompanyId = 1,
                            IncurredLoss = 1000m,
                            Ucr = "???",
                            ClaimTypeId = 2
                        }
                    }
                },
                new Company
                {
                    Id = 2,
                    Address1 = "Company 2",
                    Address2 = "Building 2",
                    Address3 = "Street 2",
                    Postcode = "LS1 1SL",
                    Country = "England",
                    Active = true,
                    Name = "Company 2",
                    InsuranceEndDate = DateTime.Today.AddMonths(3),
                    Claims = new List<Claim>
                    {
                        new Claim
                        {
                            Id = 2,
                            ClaimDate = DateTime.Today.AddDays(-25),
                            LossDate = DateTime.Today.AddDays(-35),
                            AssuredName = "Insurance Company",
                            Closed = false,
                            CompanyId = 1,
                            IncurredLoss = 3200m,
                            Ucr = "???",
                            ClaimTypeId = 2
                        },
                        new Claim
                        {
                            Id = 3,
                            ClaimDate = DateTime.Today.AddMonths(-4),
                            LossDate = DateTime.Today.AddMonths(-4).AddDays(-15),
                            AssuredName = "Insurance Company",
                            Closed = true,
                            CompanyId = 1,
                            IncurredLoss = 1500m,
                            Ucr = "???",
                            ClaimTypeId = 1
                        }
                    }
                },
                new Company
                {
                    Id = 3,
                    Address1 = "Company 3",
                    Address2 = "Building 3",
                    Address3 = "Street 3",
                    Postcode = "LS1 1SL",
                    Country = "England",
                    Active = true,
                    Name = "Company 3",
                    InsuranceEndDate = DateTime.Today.AddMonths(-2),
                    Claims = new List<Claim>()
                }
            });

            await context.ClaimType.AddRangeAsync(new List<ClaimType>
            {
                new ClaimType
                {
                    Id = 1,
                    Name = "Property Damage"
                },
                new ClaimType
                {
                    Id = 2,
                    Name = "Personal Injury"
                }
            });

            await context.SaveChangesAsync();
        }
    }
}