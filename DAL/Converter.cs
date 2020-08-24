
using Entities;
using Entities.DTO;

namespace DAL
{
    public class Converter
    {
        //company
        public static Company ConvertCompanyDtoToTbl(CompanyDto companyDto)
        {
            Company company = new Company
            {
                Id = companyDto.Id,
                Name = companyDto.Name
            };
            return company;
        }
        public static User ConvertUserDtoToTbl(UserDto userDto)
        {
            User user = new User
            {
                UserId= userDto.UserId,
                UserName = userDto.UserName
            };
            return user;
        }
        public static CompanyDto ConvertCompanyTblToDto(Company company)
        {
            CompanyDto companyDto = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name
            };
            return companyDto;
        }
        //product
        public static Product ConvertProductDtoToTbl(ProductDto productDto)
        {
            Product product = new Product
            {
                Id = productDto.Id,
                CodeInCompany = productDto.CodeInCompany,
                R = productDto.R,
                G = productDto.G,
                B = productDto.B,
                Description = productDto.Description,
                Price = productDto.Price,
                CompanyId = productDto.Company.Id,
                Company = ConvertCompanyDtoToTbl(productDto.Company)
            };
            return product;
        }
        public static ProductDto ConvertProductTblToDto(Product product)
        {
            ProductDto productDto = new ProductDto
            {
                Id = product.Id,
                CodeInCompany = product.CodeInCompany,
                R = product.R,
                G = product.G,
                B = product.B,
                Company = CompanyDAL.GetCompanyById(product.CompanyId),
                Description = product.Description,
                Price = product.Price
            };
            return productDto;
        }
    }
}
