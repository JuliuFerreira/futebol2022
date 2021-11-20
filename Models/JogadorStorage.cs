using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Models
{
    [Serializable]
    [Table("TB_JogadorStorage", Schema = "public")]
    public class JogadorStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JogadorStorageId { get; set; }
        [ForeignKey("Jogador")]
        public int JogadorId { get; set; }
        [ForeignKey("Storage")]
        public int StorageId { get; set; }
        public virtual Jogador Jogador { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
