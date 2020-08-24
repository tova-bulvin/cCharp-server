using BL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://makeup4u.herokuapp.com,http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("api/Product/GetAll")]
        public List<ProductDto> GetAll()
        {
            return ProductBL.GetAll();
        }

        [HttpGet]
        [Route("api/Product/GetProductsByName")]
        public List<ProductDto> GetProductsByName(string name)
        {
            return ProductBL.GetProductsByName(name);
        }
        [HttpPost]
        public void Post([FromBody] ProductDto product)
        {
            Console.WriteLine(product.Description);
            ProductBL.Post(product);
        }
        [HttpPut]
        public void Put([FromBody] ProductDto product)
        {
            Console.WriteLine(product.Description);
            ProductBL.Put(product);
        }

        public void Delete(int id)
        {
            ProductBL.Delete(id);
        }
    }
}