using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Initializers;

public static class DbContextInitializer
{
    public static void AddDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();

        services
           .AddDbContext<AppDbContext>(options => options
               .UseSqlite($"Data Source={pathToDbFile}"));

        string GetPathToDbFile()
        {
            var applicationFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "CSharpClicker");

            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }

            return Path.Combine(applicationFolder, "CSharpClicker.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {
        appDbContext.Database.Migrate();

        AddMissingBoosts(appDbContext);
    }

    private static void AddMissingBoosts(AppDbContext appDbContext)
    {
        const string Boost1Name = "Рудокоп";
        const string Boost2Name = "Призрак";
        const string Boost3Name = "Стражник";
        const string Boost4Name = "Маг огня";
        const string Boost5Name = "Рудный барон";

        var existingBoosts = appDbContext.Boosts
            .ToArray();

        AddBoostIfNotExist(Boost1Name, price: 100, profit: 1);
        AddBoostIfNotExist(Boost2Name, price: 500, profit: 15);
        AddBoostIfNotExist(Boost3Name, price: 2000, profit: 60, isAuto: true);
        AddBoostIfNotExist(Boost4Name, price: 10000, profit: 400);
        AddBoostIfNotExist(Boost5Name, price: 100000, profit: 4000, isAuto: true);

        appDbContext.SaveChanges();



        void AddBoostIfNotExist(string boostName, long price, long profit, bool isAuto = false)
        {
            if (!existingBoosts.Any(eb => eb.Title == boostName))
            {
                var imageBytes = GetImageBytes(boostName);

                var boost = new Boost
                {
                    Image = imageBytes,
                    Price = price,
                    Profit = profit,
                    Title = boostName,
                    IsAuto = isAuto,
                };
                appDbContext.Boosts.Add(boost);
            }
        }
    }

    static byte[] GetImageBytes(string name)
    {
        var path = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
        using var fileStream = File.OpenRead(path);
        using var memoryStream = new MemoryStream();

        fileStream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }
}
