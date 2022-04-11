using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase {
        private readonly IMoviesService _moviesService;

        public MoviesController(IMoviesService moviesService) {
            _moviesService = moviesService;
        }

        [HttpGet("stats")]
        public IActionResult GetStats() {
            try {
                var response = _moviesService.GetStats();
                
                if (response == null) {
                    return BadRequest(new { message = "No stats found!" });
                }
                return Ok(response);
            }
            catch (Exception err) {
                return BadRequest(new { message = err});
            }
        }
    }
}
