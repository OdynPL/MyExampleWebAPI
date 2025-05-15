using Bogus;

namespace MyExampleWebAPI.Models
{
    /// <summary>
    /// Basic Seed() class to initialize some dummy data inside database
    /// </summary>
    public class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Check if we alreayd have any data inside database
            if (!context.MembersDTO.Any())
            {
                // Clear Initial data
                context.MembersDTO.RemoveRange(context.MembersDTO);
                context.SaveChanges();

                var faker = new Faker();

                var members = Enumerable.Range(1, 500).Select(i =>
                {
                    var weight = Math.Round(faker.Random.Decimal(50, 120), 1);
                    var heightCm = Math.Round(faker.Random.Decimal(150, 200), 2);
                    var heightM = heightCm / 100;

                    return new Member
                    {
                        Id = Guid.NewGuid(),
                        Email = faker.Internet.Email(),
                        FirstName = faker.Name.FirstName(),
                        LastName = faker.Name.LastName(),
                        CurrentWeight = weight,
                        Age = faker.Random.Int(18, 70),
                        Height = heightCm,
                        Gender = faker.PickRandom<GenderType>(),
                        JoinDate = faker.Date.Past(10),
                        IsActive = faker.Random.Bool(),
                        BMI = Math.Round(weight / (heightM * heightM), 2)
                    };
                }).ToList();

                context.MembersDTO.AddRange(members);
                context.SaveChanges();

                // Populate additional Weight Entries to each member used in future
                var weightEntries = new List<WeightEntry>();

                foreach (var member in members)
                {
                    var currentWeight = member.CurrentWeight;

                    for (int j = 0; j < 10; j++)
                    {
                        var daysAgo = faker.Random.Int(1, 1000);
                        var fluctuation = faker.Random.Decimal(-3, 3);
                        currentWeight = Math.Round(currentWeight + fluctuation, 1);

                        weightEntries.Add(new WeightEntry
                        {
                            Id = Guid.NewGuid(),
                            MemberId = member.Id,
                            Date = DateTime.UtcNow.AddDays(-daysAgo),
                            Weight = Math.Max(40, currentWeight)
                        });
                    }
                }

                context.WeightEntriesDTO.AddRange(weightEntries);
                context.SaveChanges();
            }
        }

    }
}
