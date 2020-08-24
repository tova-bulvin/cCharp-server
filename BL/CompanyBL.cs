using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO;
using DAL;


namespace BL
{
    public class CompanyBL
    {
        public static void Post(CompanyDto company)
        {
            CompanyDAL.Post(company);
        }

        public static void Put(CompanyDto company)
        {
            CompanyDAL.Put(company);
        }

        public static List<CompanyDto> GetAll()
        {
            return CompanyDAL.GetAll();
        }


        public static void Delete(int id)
        {
            CompanyDAL.Delete(id);
        }

    }
}
