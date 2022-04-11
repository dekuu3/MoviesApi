using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.ViewModels {
    public class MovieStatsViewModel {
        public string movieId { get; set; }

        public string title { get; set; }

        public int averageWatchDurationS { get; set; }

        public int watches { get; set; }

        public string releaseYear { get; set; }
    }
}
