using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Models
{
    [Serializable]
    [Table("TB_Storage", Schema = "public")]
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StorageId { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataInsercao { get; set; } = DateTime.Now;

        public string Extensao { get; set; }

        public byte[] Arquivo { get; set; }

        public string ContentType { get; set; }

        public string Diretorio { get; set; }

        public string Tamanho { get; set; }
    }
}
