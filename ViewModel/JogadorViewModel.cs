using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.ViewModel
{
    [Serializable]
    public class JogadorViewModel
    {
        [DisplayName("Nome do jogador")]
        [Required(ErrorMessage = "Informe o nome do jogador")]
        public string NomeJogador { get; set; }

        [DisplayName("Apelido")]
        public string Apelido { get; set; }
       
        [DisplayName("Foto do jogador")]
        public IFormFile FotoJogador { get; set; }

        [DisplayName("Data nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:g}")]
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }
    }
}
