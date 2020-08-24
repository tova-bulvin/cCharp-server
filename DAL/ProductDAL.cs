using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO;

namespace DAL
{
    public static partial class ProductDAL
    {
        public static List<ProductDto> GetAll()
        {
            using (var db = new DBContext())
            {
                List<Product> productList = db.Products.ToList();
                List<ProductDto> convertedProduct = new List<ProductDto>();
                foreach (var currentProduct in productList)
                {
                    convertedProduct.Add(Converter.ConvertProductTblToDto(currentProduct));
                }
                return convertedProduct;
            }
        }

        public static List<ProductDto> GetProductsByName(string name)
        {
            using (var db = new DBContext())
            {
                List<Product> productList = db.Products.Where(currentProduct => currentProduct.Company.Name == name).ToList();
                List<ProductDto> convertedProduct = new List<ProductDto>();
                foreach (var currentProduct in productList)
                {
                    convertedProduct.Add(Converter.ConvertProductTblToDto(currentProduct));
                }
                return convertedProduct;
            }
        }

        public static ProductDto GetProductsById(int id)
        {
            using (var db = new DBContext())
            {
                Product product = db.Products.FirstOrDefault(currentProduct => currentProduct.Id == id);
                ProductDto productDto = Converter.ConvertProductTblToDto(product);
                return productDto;
            }
        }

        public static List<List<int>> GetRGBProductsByName(string name)
        {
            using (var db = new DBContext())
            {
                List<Product> productList = db.Products.Where(currentProduct => currentProduct.Company.Name == name).ToList();
                List<List<int>> RGBProductList = new List<List<int>>();
                ProductDto product;
                List<int> RGBList = new List<int>();
                foreach (var currentProduct in productList)
                {
                    product = Converter.ConvertProductTblToDto(currentProduct);
                    RGBList= new List<int>();
                    RGBList.Add(product.Id);
                    RGBList.Add(product.R);
                    RGBList.Add(product.G);
                    RGBList.Add(product.B);
                    RGBProductList.Add(RGBList);
                }
                return RGBProductList;
            }
        }
        public static void Post(ProductDto product)
        {
            using (var db = new DBContext())
            {
                try
                {
                    Product productToUpdate = db.Products.First(currentProduct => currentProduct.Id == product.Id);
                    productToUpdate.Id = product.Id;
                    productToUpdate.CodeInCompany = product.CodeInCompany;
                    productToUpdate.R = product.R;
                    productToUpdate.G = product.G;
                    productToUpdate.B = product.B;
                    productToUpdate.Price = product.Price;
                    productToUpdate.Description = product.Description;
                    productToUpdate.CompanyId = product.Company.Id;
                    productToUpdate.Company = null;
                    //productToUpdate.Company = Converter.ConvertCompanyDtoToTbl(product.Company);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    Console.WriteLine("");
                }
               

            }
        }

        public static void Put(ProductDto productDto)
        {
            using (var db = new DBContext())
            {
                Product product = Converter.ConvertProductDtoToTbl(productDto);
                product.Company = null;
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public static void Delete(int id)
        {
            using (var db = new DBContext())
            {
                Product productToDelete = db.Products.First(currentProduct => currentProduct.Id == id);
                db.Products.Remove(productToDelete);
                db.SaveChanges();
            }
        }
    }
}
