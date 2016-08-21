using AutoMapper;
using RentalVideo.Data.Infrastructure;
using RentalVideo.Entities;
using RentalVideo.Infrastructure.Core;
using RentalVideo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalVideo.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/genres")]
    public class GenresController : ApiBaseController
    {
        private readonly IEntityBaseRepository<Genre> genreRepo;

        public GenresController(IEntityBaseRepository<Genre> genreRepo, IUnitOfWork unitOfWork,
            IEntityBaseRepository<Error> errorRepo) : base(errorRepo, unitOfWork)
        {
            this.genreRepo = genreRepo;
        }

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            return base.CreateHttpResponse(() =>
            {
                HttpResponseMessage response = null;
                var genres = genreRepo.GetAll().ToList();
                IEnumerable<GenreViewModel> viewGenres = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(genres);
                response = Request.CreateResponse(HttpStatusCode.OK, viewGenres);
                return response;
            });
        }
    }
}
