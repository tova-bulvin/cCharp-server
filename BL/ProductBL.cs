using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities.DTO;

namespace BL
{
    public class ProductBL
    {
        public static List<ProductDto> GetAll()
        {
            return ProductDAL.GetAll();
        }

        public static List<ProductDto> GetProductsByName(string name)
        {
            return ProductDAL.GetProductsByName(name);
        }
 

        public static void Post(ProductDto product)
        {
            ProductDAL.Post(product);
        }

        public static void Put(ProductDto product)
        {
            ProductDAL.Put(product);
        }

        public static void Delete(int id)
        {
            ProductDAL.Delete(id);
        }

        public static ProductDto GetProductsById(int id)
        {
            return ProductDAL.GetProductsById(id);
        }
    }
}