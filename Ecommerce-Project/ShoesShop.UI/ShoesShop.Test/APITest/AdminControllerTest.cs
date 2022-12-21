using Microsoft.EntityFrameworkCore;
using Moq;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.API;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoesShop.API.Mapper;
using ShoesShop.DTO;
using ShoesShop.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.DTO.Admin;

namespace ShoesShop.Test.APITest
{
    public class AdminControllerTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Admin> _admins;
        private readonly IMapper _mapper;
        public AdminControllerTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TempTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AdminMapper())).CreateMapper();
            _admins = AdminTestData.GetAdmins();
            _context.Database.EnsureDeleted();
            _context.Admins.AddRange(_admins);
            _context.SaveChanges();
        }


    }
}
