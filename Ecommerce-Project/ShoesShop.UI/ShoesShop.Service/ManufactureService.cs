using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using System.Xml.Linq;

namespace ShoesShop.Service
{
    public interface IManufactureService
    {
        public List<ManufactureViewModel> GetAllManufacture();
        public ManufactureViewModel GetManufactureById(int manufactureId);
    }
    public class ManufactureService : IManufactureService
    {
        public List<ManufactureViewModel> GetAllManufacture()
        {
            List<ManufactureViewModel> manufactures = new List<ManufactureViewModel>();
            using (var context = new ApplicationDbContext())
            {
                manufactures = context.Manufactures.Where(m => m.Status).Select(m => new ManufactureViewModel
                {
                    ManufactureId = m.ManufactureId,
                    Name = m.Name,
                    Logo = m.Logo, 
                }).ToList();
            }
            return manufactures;
        }
        public ManufactureViewModel GetManufactureById(int manufactureId)
        {
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel();
            using (var context = new ApplicationDbContext())
            {
                var manufacture = context.Manufactures.FirstOrDefault(m => m.ManufactureId == manufactureId && m.Status);
                if (manufacture != null)
                {
                    manufactureViewModel.ManufactureId = manufacture.ManufactureId;
                    manufactureViewModel.Name = manufacture.Name;
                    manufactureViewModel.Logo = manufacture.Logo;
                }
            }
            return manufactureViewModel;
        }
    }
}
