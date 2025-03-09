using AutoMapper;
using Clothing.CMS.Application.Customers.Dto;
using Clothing.CMS.Application.Services;
using Clothing.CMS.Entities;
using Clothing.CMS.EntityFrameworkCore.Pattern.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Clothing.CMS.Application.Customers
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IRepository<Customer> _repo;
		private readonly IMapper _mapper;

		public CustomerService(IRepository<Customer> repo,
            IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory) : base(httpContextAccessor, tempDataDictionaryFactory)
        {
            _repo = repo;
            _mapper = mapper;
        }

		public async Task<ICollection<CustomerDto>> GetAll()
		{
			try
			{
				var customers = await _repo.GetAll()
				  .OrderByDescending(x => x.Id)
				  .ToListAsync();

				var customerDto = _mapper.Map<ICollection<CustomerDto>>(customers);

				return customerDto;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
