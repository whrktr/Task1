using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace TestTasks.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Data> Data { get; set; }

        public void AddFromDictList(List<Dictionary<string, string>> entities)
        {
            Data[] converted = entities.Select(x =>
                    new Data
                    {
                        Code = int.Parse(x.First().Key),
                        Value = x.First().Value
                    })
                    .OrderBy(x => x.Code)
                    .ToArray();

            Data.RemoveRange(Data);

            Data.AddRange(converted);

            SaveChanges();
        }

        public IEnumerable<Data> Get(int? code = null, string? value = null, int? id = null)
        {
            if(code == null && value == null && id == null) return Data.ToList();

            var data = Data.AsQueryable();

            if(code != null)
            {
                data = data.Where(x => x.Code == code);
            }
            if(value != null) 
            {
                data = data.Where(x => EF.Functions.Like(x.Value, $"%{value}%"));
            }

            if (id != null)
            {
                data = data.Where(x => x.Id == id);
            }

            return data.ToList();
        }
    }
}
