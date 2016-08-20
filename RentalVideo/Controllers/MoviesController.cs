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
    [Route("api/movies")]
    public class MoviesController : ApiBaseController
    {
        private readonly IEntityBaseRepository<Movie> movieRepo;

        public MoviesController(IEntityBaseRepository<Movie> movieRepo, IUnitOfWork unitOfWork,
            IEntityBaseRepository<Error> errorsRepository): base(errorsRepository, unitOfWork)
        {
            this.movieRepo = movieRepo;
        }

        [AllowAnonymous]
        [Route("lastest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return base.CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var movies = movieRepo.GetAll().OrderByDescending(m => m.ReleaseDate).Take(6).ToList();
                IEnumerable<MovieViewModel> viewMovie = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);
                response = request.CreateResponse(HttpStatusCode.OK, viewMovie);
                return response;
            });
        }
    }
}
