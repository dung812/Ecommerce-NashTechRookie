using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ShoesShop.Service;
using ShoesShop.DTO;

namespace ShoesShop.Test.ServiceTest
{
    public class StatisticServiceTest : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Catalog> _catalogs;
        public StatisticServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDB").Options;
            _context = new ApplicationDbContext(_options);
            _catalogs = CatalogTestData.GetCatalogs();
            _context.Database.EnsureDeleted();
            _context.Catalogs.AddRange(_catalogs);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
