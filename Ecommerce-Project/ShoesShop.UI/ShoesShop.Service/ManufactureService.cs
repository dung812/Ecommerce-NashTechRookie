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
        public bool CreateManufacture(ManufactureViewModel manufactureViewModel);
        public bool UpdateManufacture(int manufactureId, ManufactureViewModel manufactureViewModel);
        public bool DeleteManufacture(int manufactureId);
    }
    public class ManufactureService : IManufactureService
    {
        private readonly ApplicationDbContext _context;
        public ManufactureService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ManufactureViewModel> GetAllManufacture()
        {
            List<ManufactureViewModel> manufactures = new List<ManufactureViewModel>();
            manufactures = _context.Manufactures.Where(m => m.Status).Select(m => new ManufactureViewModel
            {
                ManufactureId = m.ManufactureId,
                Name = m.Name,
                Logo = m.Logo,
            }).ToList();
            return manufactures;
        }
        public ManufactureViewModel GetManufactureById(int manufactureId)
        {
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel();
            var manufacture = _context.Manufactures.FirstOrDefault(m => m.ManufactureId == manufactureId && m.Status);
            if (manufacture != null)
            {
                manufactureViewModel.ManufactureId = manufacture.ManufactureId;
                manufactureViewModel.Name = manufacture.Name;
                manufactureViewModel.Logo = manufacture.Logo;
            }
            return manufactureViewModel;
        }


        public bool CreateManufacture(ManufactureViewModel manufactureViewModel)
        {
            try
            {
                Manufacture manufacture = new Manufacture()
                {
                    Name = manufactureViewModel.Name,
                    Logo = manufactureViewModel.Logo,
                    Status = true
                };
                _context.Manufactures.Add(manufacture);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateManufacture(int manufactureId, ManufactureViewModel manufactureViewModel)
        {
            try
            {
                bool result;
                var manufacture = _context.Manufactures.Find(manufactureId);
                if (manufacture != null)
                {
                    manufacture.Name = manufactureViewModel.Name;
                    manufacture.Logo = manufactureViewModel.Logo;

                    _context.Manufactures.Update(manufacture);
                    _context.SaveChanges();

                    result = true;
                }
                else
                    result = false;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteManufacture(int manufactureId)
        {
            try
            {
                bool result;
                var manufacture = _context.Manufactures.Find(manufactureId);
                if (manufacture != null)
                {
                    manufacture.Status = false;

                    _context.Manufactures.Update(manufacture);
                    _context.SaveChanges();

                    result = true;
                }
                else
                    result = false;
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
