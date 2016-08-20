using RentalVideo.Base;
using RentalVideo.Data.Infrastructure;
using RentalVideo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace RentalVideo.Infrastructure.Core
{
    public class ApiBaseController : ApiController
    {
        protected readonly IEntityBaseRepository<Error> errorRepository;
        protected readonly IUnitOfWork unitOfWork;

        public ApiBaseController(IEntityBaseRepository<Error> errorRepository, IUnitOfWork unitOfWork)
        {
            this.errorRepository = errorRepository;
            this.unitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request,
            Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException dex)
            {
                LogError(dex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, dex.Message);
            }
            catch(Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        protected HttpResponseMessage CreateHttpResponse(Func<HttpResponseMessage> function)
        {
            return CreateHttpResponse(Request, function);
        }

        private void LogError(Exception ex)
        {
            try
            {
                var error = new Error
                {
                    Message = ex.Message,
                    DateCreated = SystemInformation.GetDate(),
                    StackTrace = ex.StackTrace
                };
                errorRepository.Add(error);
                unitOfWork.Commit();
            }
            catch { }
        }

    }
}
