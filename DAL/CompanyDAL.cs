using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO;

namespace DAL
{
    public static partial class CompanyDAL
    {
        
        public static void Post(CompanyDto company)
        {
                using (var db = new DBContext())
                {
                    Company companyToUpdate = db.Companies.First(currentCompany => currentCompany.Id ==company.Id);
                    companyToUpdate.Name = company.Name;
                    db.SaveChanges();
                }
        }
        public static void Put(CompanyDto company)
        {

            using (var db = new DBContext())
            {
                db.Companies.Add(Converter.ConvertCompanyDtoToTbl(company));
                db.SaveChanges();
            }
        }

        public static CompanyDto GetCompanyById(int id)
        {
            using (var db = new DBContext())
            {
                Company company = db.Companies.FirstOrDefault(currentCompany => currentCompany.Id == id);
                CompanyDto convertedProduct =Converter.ConvertCompanyTblToDto(company);
                return convertedProduct;
            }
        }

        public static void Delete(int id)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Company companyToDelete = db.Companies.First(currentCompany => currentCompany.Id == id);
                    db.Companies.Remove(companyToDelete);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    //יש בעיה שלו מוחק חברה שיש מוצרים התלויים בה
                
                }
                
            }
        }

        public static List<CompanyDto> GetAll()
        {
            using (var db = new DBContext())
            {
                List<Company> companyList = db.Companies.ToList();
                List<CompanyDto> convertedCompany = new List<CompanyDto>();
                foreach (var currentCompany in companyList)
                {
                    convertedCompany.Add(Converter.ConvertCompanyTblToDto(currentCompany));
                }
                return convertedCompany;
            }

        }
    }
}
