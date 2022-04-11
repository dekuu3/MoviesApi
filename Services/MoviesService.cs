using MoviesAPI.Entities;
using MoviesAPI.ViewModels;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services {
    public class MoviesService : IMoviesService {
        #region GetStats
        public List<MovieStatsViewModel> GetStats() {
            var statsDataLocation = "./Database/stats.csv";
            var metadataDataLocation = "./Database/metadata.csv";
            var StatsList = new List<StatsEntity>();
            var MetadataList = new List<MetadataEntity>();

            // Get data from stats.csv
            var statsData = File.ReadAllLines(statsDataLocation);
            for (var i = 1; i < statsData.Length; i++) {
                var line = statsData[i].Split(",");
                var index = StatsList.FindIndex(x => x.MovieID == line[0]);
                    
                // if movieid doesnt exist in list, create it and add it to list
                if (index < 0) {
                    var stat = new StatsEntity() {
                        MovieID = line[0],
                        WatchDurationMs = line[1],
                        Count = 1
                    };

                    StatsList.Add(stat);

                } else {
                    //else override current stat object
                    var stat = StatsList[index];

                    stat.MovieID = line[0];
                    stat.WatchDurationMs = line[1];
                    stat.Count++;

                    StatsList[index] = stat;
                }
            }

            // Get data from metadata.csv
            var metadataData = File.ReadAllLines(metadataDataLocation);
            for (var i = 1; i < metadataData.Length; i++) {
                var line = metadataData[i].Split(",");
                var index = MetadataList.FindIndex(x => x.MovieID == line[1]);

                // if movieid doesnt exist in list, create it and add it to list
                if (index < 0) {
                    var metadata = new MetadataEntity() {
                        MovieID = line[1],
                        Title = line[2],
                        ReleaseYear = line[5]
                    };

                    MetadataList.Add(metadata);
                }
            }

            // Combine the data for output
            var OutputData = new List<MovieStatsViewModel>();
            foreach (var metadata in MetadataList) {
                var stat = StatsList.Find(x => x.MovieID == metadata.MovieID);
                var averageDuration = Int32.Parse(stat.WatchDurationMs) / stat.Count;

                var movieStat = new MovieStatsViewModel() {
                    movieId = metadata.MovieID,
                    title = metadata.Title,
                    averageWatchDurationS = averageDuration * 60,
                    watches = stat.Count,
                    releaseYear = metadata.ReleaseYear
                };

                OutputData.Add(movieStat);
            }

            // Sort list
            var sortedList = OutputData.OrderByDescending(o => o.watches).ToList();

            return sortedList;
        }
        #endregion

        #region GetMetadataById
        public List<MetadataEntity> GetMetadataById(int movieId) {
            var metadataDataLocation = "./Database/metadata.csv";
            var MetadataList = new List<MetadataEntity>();
            var outputList = new List<MetadataEntity>();

            // Get movies with movieId from metadata.csv
            var metadataData = File.ReadAllLines(metadataDataLocation);
            for (var i = 1; i < metadataData.Length; i++) {
                var line = metadataData[i].Split(",");

                if (line[1] == movieId.ToString()) {
                    var metadata = new MetadataEntity() {
                        Id = line[0],
                        MovieID = line[1],
                        Title = line[2],
                        Language = line[3],
                        Duration = line[4],
                        ReleaseYear = line[5]
                    };

                    MetadataList.Add(metadata);
                }
            }

            // if movieid doesnt exist in list, return null
            if (!MetadataList.Any()) {
                return null;
            }

            // only add new movieid's or new languages to our output list
            foreach (var movie in MetadataList) {
                var index = outputList.FindIndex(x => x.MovieID == movie.MovieID);
                var langIndex = outputList.FindLastIndex(x => x.Language == movie.Language);

                if (index < 0 || langIndex < 0) {
                    outputList.Add(movie);
                } 

            }
            var sortedList = outputList.OrderBy(o => o.Language).ToList();

            return sortedList;
        }
        #endregion

        public bool AddMovie(MetadataEntity metadata) {
            var metadataDataLocation = "./Database/metadata.csv";

            try {
                string csvData = $"{metadata.Id},{metadata.MovieID},{metadata.Title},{metadata.Language},{metadata.Duration},{metadata.ReleaseYear}";
                File.AppendAllText(metadataDataLocation, csvData.ToString());
            } catch {
                return false;
            }

            return true;
        }
    }
}
