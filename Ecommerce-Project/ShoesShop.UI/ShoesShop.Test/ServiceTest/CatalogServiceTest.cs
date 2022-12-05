using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.ServiceTest
{
    public class CatalogServiceTest 
    {
        //private readonly DbContextOptions<ApplicationDbContext> _options;
        //private readonly ApplicationDbContext _context;
        //private readonly List<Catalog> _catalogs;
        //public CatalogServiceTest()
        //{
        //    //_options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AssignmentTestDB").Options;
        //    //_context = new ApplicationDbContext(_options);
        //    //_catalogs = CatalogTestData.GetCatalogs();
        //    //_context.Database.EnsureDeleted();
        //    //_context.Catalogs.AddRange(_catalogs);
        //    //_context.SaveChanges();
        //}

        //[Fact]
        //public void GetAssignmentById_ValidId_ShouldReturnAssignmentDTO()
        //{
        //    //Arrange
        //    var countCatalog = CatalogTestData.GetCatalogs().Count; // parameter



        //    //AssignmentRepository assignmentRepo = new AssignmentRepository(_mapper, _context);

        //    //// Act
        //    //var result = assignmentRepo.GetAssignmentById(assignmentId).Result;

        //    //// Assert
        //    //Assert.NotNull(result);
        //    //Assert.IsType<AssignmentDetailDTO>(result);
        //    //Assert.Equal(assignmentFake.Id, result.Id);
        //}

        //public void Dispose()
        //{
        //    _context.Database.EnsureDeleted();
        //    _context.Dispose();
        //}

    }
}
