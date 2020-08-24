using BL;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "http://makeup4u.herokuapp.com,http://localhost:4200", headers: "*", methods: "*")]
    public class CompanyController : ApiController
    {
        [HttpGet]
        [Route("api/Company/GetAll")]
        public List<CompanyDto> GetAll()
        {
            return CompanyBL.GetAll();
        }
  
        [HttpPut]

        public void Put( CompanyDto company)
        {
            CompanyBL.Put(company);
        }

        [HttpPost]
        public void Post([FromBody] CompanyDto company)
        {
            CompanyBL.Post(company);
        }
       

        public void Delete(int id)
        {
            CompanyBL.Delete(id);
        }
    }
}
