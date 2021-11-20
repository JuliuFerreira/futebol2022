using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Models
{
    [Serializable]
    [Table("TB_Partidas", Schema = "public")]
    public class Partidas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("data do jogo")]
        public DateTime DataJogo { get; set; }

        [DisplayName("Adversário")]
        public string AdversarioJogo { get; set; }

        [DisplayName("Local")]
        public string LocalJogo { get; set; }

        [DisplayName("Hora")]
        public DateTime HoraJogo { get; set; }

        [DisplayName("Placar")]
        public string PlacarJogo { get; set; }
    }
}
