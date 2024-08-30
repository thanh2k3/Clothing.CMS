using Clothing.CMS.Application.Common;
using Clothing.CMS.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Clothing.CMS.Application.Services
{
    public class BaseService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public BaseService(IHttpContextAccessor httpContextAccessor,
			ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        protected void FillAuthInfo(BaseCruidEntity data)
        {
            if (data != null)
            {
                var userId = _httpContextAccessor.HttpContext.User.GetUserProperty(CustomClaimTypes.NameIdentifier);
                var timeNow = DateTime.Now;
                if (data.Id < 1)
                {
                    data.CreatedBy = userId;
                    data.CreatedTime = timeNow;
                    data.ModifiedBy = userId;
                    data.ModifiedTime = timeNow;
                }
                else
                {
                    if (string.IsNullOrEmpty(data.CreatedBy))
                    {
                        data.CreatedBy = userId;
                        data.CreatedTime = timeNow;
                    }
                    data.ModifiedBy = userId;
                    data.ModifiedTime = timeNow;
                }
            }
        }

        protected void NotifyMsg(string msg)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var TempData = _tempDataDictionaryFactory.GetTempData(httpContext);

			TempData["Message"] = msg;
        }
    }
}
