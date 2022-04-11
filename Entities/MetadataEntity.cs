using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Entities {
    public class MetadataEntity {
        public string Id { get; set; }

        public string MovieID { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }

        public string ReleaseYear { get; set; }

    }
}
