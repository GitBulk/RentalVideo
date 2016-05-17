using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalVideo.Entities
{
    public class Genre : IEntityBase
    {
        public Genre()
        {
            this.Movies = new List<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
