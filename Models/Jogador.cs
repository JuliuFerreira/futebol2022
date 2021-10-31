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
    [Table("TB_Jogadores", Schema = "public")]
    public class Jogador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [DisplayName("Nome do jogador")]
        public string NomeJogador { get; set; }

        public string Apelido { get; set; }

        [ForeignKey("Storage")]
        public int StorageId { get; set; }

        public virtual Storage Storage { get; set; }

        [DisplayFormat(DataFormatString ="{0:g}", NullDisplayText = "---")]
        public DateTime? DataNascimento { get; set; }

    }
}
