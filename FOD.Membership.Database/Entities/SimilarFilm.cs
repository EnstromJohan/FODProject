using System.ComponentModel.DataAnnotations.Schema;

namespace FOD.Membership.Database.Entities
{
    public class SimilarFilm
    {
        public int FilmId { get; set; }
        public int SimilarFilmId { get; set; }
        public virtual Film? Film { get; set; }
        [ForeignKey("SimilarFilmId")]

        public virtual Film? Similar { get; set; }

    }
}
