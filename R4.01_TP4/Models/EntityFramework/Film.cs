using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

namespace R4._01_TP4.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    public partial class Film
    {
        [Key]
        [Column("flm_id")]
        public int Idfilm { get; set; }

        [Column("flm_titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;

        [Column("flm_resume")]
        public string? Resume { get; set; }

        [Column("flm_datesortie")]
        public DateTime DateSortie { get; set; }

        [Column("flm_duree")]
        [Precision(3,0)]
        public decimal Duree {  get; set; }

        [Column("flm_genre")]
        [StringLength(30)]
        public string Genre { get; set; }

        [InverseProperty(nameof(Notation.FilmId))]
        public virtual ICollection<Notation> NotesFilm { get; set; } = new List<Notation>();

    }
}
