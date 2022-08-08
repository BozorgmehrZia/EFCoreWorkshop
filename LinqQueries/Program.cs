using LinqQueries;
using Microsoft.EntityFrameworkCore;
using Npgsql;

class Program
{
    public static void Main(string[] args)
    {
        using var context = new PeopleContext();

        
    }

    private static string CreateConnectionString()
    {
        var postgresConnectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5433,
            Username = "Username",
            Password = "Password"
        };
        return postgresConnectionStringBuilder.ConnectionString;
    }

    private static void FirstAndSingle(PeopleContext context)
    {
        var first = context.Cars.First(c => c.OwnerNationalId == "0024936219");
        
        var car1 = context.Cars.Single(c => c.MadeYear == 1399); //OK
        var car2 = context.Cars.Single(c => c.OwnerNationalId == "0024936219"); //Will throw exception
    }

    private static void TakeAndSkip(PeopleContext context)
    {
        var firstTwoCars = context.Cars.Take(2)
            .ToList();
        var secondTwoCars = context.Cars.Skip(2)
            .ToList();
    }

    private static void Join(PeopleContext context)
    {
        var people = context.People.Join(context.Cars,
            p => p.NationalId,
            c => c.OwnerNationalId,
            (person, car) => new
            {
                PersonName = person.Name,
                car.CarName,
                person.NationalId
            });
    }

    private static void GroupBy(PeopleContext context)
    {
        var groups = context.Cars
            .GroupBy(p => p.OwnerNationalId)
            .Select(g => new
            {
                OwnerNationalId = g.Key,
                Count = g.Count()
            })
            .ToList();
    }

    private static void RawSql(PeopleContext context)
    {
        var cars = context.Cars
            .FromSqlRaw(@"select * from ""Cars"" where ""MadeYear"" > 1398")
            .ToList();
    }

    private static void LoadRelatedData(PeopleContext context)
    {
        #region Eager

        var included = context.Houses
            .Include(p => p.Owner)
            .ToList();

        #endregion

        #region Explicit

        var house = context.Houses.Single(b => b.Id == 1);

        context.Entry(house)
            .Reference(h => h.Owner)
            .Load();

        #endregion
    }

    private static void ShowQueryWithLogger(PeopleContext context)
    {
        var databaseQuery = context.Cars
            .Where(c => c.CarName == "پراید")
            .Select(c => new
            {
                c.CarName
            })
            .ToList();
        var memoryQuery = context.Cars
            .ToList()
            .Where(c => c.CarName == "پراید")
            .Select(c => new
            {
                c.CarName
            });
    }

    private static void Transaction(PeopleContext context)
    {
        context.Database.BeginTransaction();
        //Work to do
        context.Database.CommitTransaction();
    }

    private static void AddRecords()
    {
        using var context = new PeopleContext();
        var people = new List<Person>
        {
            new()
            {
                Id = 1,
                Name = "علی احمدی",
                NationalId = "0031526123",
                Age = 40
            },
            new()
            {
                Id = 2,
                Name = "بزرگمهر ضیاء",
                NationalId = "0024936219",
                Age = 20
            },
        };
        var cars = new List<Car>
        {
            new()
            {
                Id = 1,
                CarName = "پراید",
                MadeYear = 1396,
                OwnerNationalId = "0024936219"
            },
            new()
            {
                Id = 2,
                CarName = "پژو",
                MadeYear = 1398,
                OwnerNationalId = "0024936219"
            },
            new()
            {
                Id = 3,
                CarName = "تیبا",
                MadeYear = 1400,
                OwnerNationalId = "0031526123"
            },
            new()
            {
                Id = 4,
                CarName = "سمند",
                MadeYear = 1399,
                OwnerNationalId = "0031526123"
            },
            
        };
        var houses = new List<House>
        {
            new()
            {
                Id = 1,
                Address = "خیابان انقلاب",
                Area = 50,
                Owner = people[0]
            },
            new()
            {
                Id = 2,
                Address = "گیشا",
                Area = 60,
                Owner = people[0]
            },
            new()
            {
                Id = 3,
                Address = "ستارخان",
                Area = 80,
                Owner = people[1]
            }
        };
        

        context.People.AddRange(people);
        context.Cars.AddRange(cars);
        context.Houses.AddRange(houses);
        context.SaveChanges();
    }
    
    
}