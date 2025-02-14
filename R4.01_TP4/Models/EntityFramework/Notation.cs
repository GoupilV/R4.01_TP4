using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace R4._01_TP4.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [Key]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Required]
        [Range(0, 5)]
        public int Note { get; set; }

        [ForeignKey(nameof(FilmId))]
        [InverseProperty(nameof(Film.NotesFilm))]
        public virtual ICollection<Film> FilmNote { get; set; }

        [ForeignKey(nameof(UtilisateurId))]
        [InverseProperty(nameof(Utilisateur.NotesUtilisateur))]
        public virtual ICollection<Utilisateur> UtilisateurNotant { get; set; } = new List<Utilisateur>();
    }
}
