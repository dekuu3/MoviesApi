using MoviesAPI.Entities;
using MoviesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services {
    public interface IMoviesService {
        List<MovieStatsViewModel> GetStats();

        List<MetadataEntity> GetMetadataById(int movieId);

        bool AddMovie(MetadataEntity metadata);
    }
}
