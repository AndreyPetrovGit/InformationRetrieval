using System;
using System.Collections.Generic;

namespace Dictionary
{
    public partial class MoviesActors
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual Actors Actor { get; set; }
        public virtual Movies Movie { get; set; }
    }
}
