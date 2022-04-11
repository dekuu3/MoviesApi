using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities {
    public class StatsEntity {
        public string MovieID { get; set; }

        public string WatchDurationMs { get; set; }

        public int Count { get;set; }
    }
}

