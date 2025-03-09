using AutoMapper;
using Clothing.CMS.Entities;

namespace Clothing.CMS.Application.Customers.Dto
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
