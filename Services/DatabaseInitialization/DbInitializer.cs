using SteamQueue.Context;
using Microsoft.EntityFrameworkCore;
using SteamQueue.Entities;

namespace SteamQueue.Services.DatabaseInitialization
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly DateTime currentDate = DateTime.Now.Date;
        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    context!.Database.Migrate();
                }
            }
        }


        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DatabaseContext>())
                {
                    SteamAccount steamAccountLine1 = new() 
                    { 
                        Login = "AwesomeLogin!", 
                        Password = "ComplicatedPassword!", 
                        Id = new Guid("b12b4e1d-f7b6-428e-8967-f25b89320bfe") 
                    };

                    /* Queue Section */
                    var lines = new List<Line>
                    {
                        new Line
                        {
                            Id = new Guid("aff9f7f2-84ca-4d22-8d86-a14a1821c89c"),
                            Name = "Steam Line 1",
                            PositionPeriod = 72000000000,
                            LineStart = DateTimeOffset.Now + new TimeSpan(10, 0, 0),
                            LineFinish = DateTimeOffset.Now + new TimeSpan(23, 0, 0),
                            SteamAccount = steamAccountLine1
                        }
                    };
                    if (!context!.Lines!.Any())
                    {

                        context.AddRange(lines);
                        context!.SaveChanges();

                    }
                }
            }
        }
    }
}
