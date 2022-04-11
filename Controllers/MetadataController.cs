using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MetadataController : ControllerBase {
        private readonly IMoviesService _moviesService;

        public MetadataController(IMoviesService moviesService) {
            _moviesService = moviesService;
        }

        [HttpPost]
        public IActionResult AddMetadata(MetadataEntity metadata) {
            try {
                var response = _moviesService.AddMovie(metadata);

                if (!response) {
                    return BadRequest(new { message = "No stats found!" });
                }
                return Ok(response);
            }
            catch (Exception err) {
                return BadRequest(new { message = err });
            }
        }

        [HttpGet("{movieId}")]
        public ActionResult<List<MetadataEntity>> GetMetadataById(int movieId) {
            try {
                var response = _moviesService.GetMetadataById(movieId);

                if (response == null) {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception err) {
                return BadRequest(new { message = err });
            }
        }
    }
}
