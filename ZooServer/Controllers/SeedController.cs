using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using zooModel;
using CsvHelper.Configuration;
using CsvHelper;
using ZooServer.DTOS;

namespace ZooServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ProjectSourceContext context, IHostEnvironment environment,
        UserManager<ZooUser> userManager) : ControllerBase
    {
        string _pathNameZoo = Path.Combine(environment.ContentRootPath, "DTOS/Zoos.csv");

        [HttpPost("Users")]
        public async Task ImportUsersAsync()
        {
            ZooUser user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            IdentityResult x = await userManager.CreateAsync(user, "Password0!");
            int y = await context.SaveChangesAsync();

        }

        [HttpPost("Zoos")]
        public async Task<ActionResult> ImportZoosAsync()
        {
            Dictionary<string, Zoo> zoosByName = context.Zoos
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathNameZoo);
            using CsvReader csv = new(reader, config);

            List<ZooDTO> records = csv.GetRecords<ZooDTO>().ToList();
            foreach (ZooDTO record in records)
            {
                if (zoosByName.ContainsKey(record.Name))
                {
                    continue;
                }


                Zoo zoo = new()
                {
                    Name = record.Name,
                    Address = record.Address,
                    State = record.State
                };
                await context.Zoos.AddAsync(zoo);
                zoosByName.Add(record.Name, zoo);
            }
            await context.SaveChangesAsync();

            return new JsonResult(zoosByName.Count);
        }

        [HttpPost("Habitats")]
        public async Task<ActionResult> ImportHabitatsAsync()
        {
            Dictionary<string, Zoo> zoos = await context.Zoos.ToDictionaryAsync(z => z.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int habitatCount = 0;
            using (StreamReader reader = new(_pathNameZoo))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<ZooDTO>? records = csv.GetRecords<ZooDTO>();
                foreach (ZooDTO record in records)
                {
                    if (!zoos.TryGetValue(record.Name, out Zoo? value))
                    {
                        Console.WriteLine($"Not found country for {record.Name}");
                        return NotFound(record);
                    }

                    if (string.IsNullOrEmpty(record.Population))
                    {
                        Console.WriteLine($"Skipping {record.HabitatName}");
                        continue;
                    }
                    Habitat habitat = new()
                    {
                        Name = record.HabitatName,
                        Population = record.Population,
                        ZooId = value.ZooId
                    };
                    context.Habitats.Add(habitat);
                    habitatCount++;
                }
                await context.SaveChangesAsync();
            }
            return new JsonResult(habitatCount);
        }
    }
}
