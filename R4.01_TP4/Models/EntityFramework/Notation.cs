using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace R4._01_TP4.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public partial class Notation
    {
        [Key]
        [ForeignKey("utl_id")]
        [Column("utl_id")]
        public int Utilisateur { get; set; }

        [Key]
        [ForeignKey("flm_id")]
        [Column("flm_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        [Required]
        [Range(0, 5)]
        public int Note { get; set; }

        [InverseProperty(nameof(Film.Idfilm))]
        public virtual ICollection<Film> FilmNote { get; set; } = new List<Film>();

        [InverseProperty(nameof(Utilisateur.UtilisateurId))]
        public virtual ICollection<Utilisateur> UtilisateurNotant { get; set; } = new List<Utilisateur>();
    }
}
