using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Models
{
    [Serializable]
    public class Jogador
    {
        public int Id {get; set;}
        public string NomeJogador { get; set; }
        public string Apelido { get; set; }
        public int? NumeroDeJogos { get; set; }
        public int? NumeroDeGols { get; set; }
        public byte[] ImagemJogador { get; set; }

        public DateTime? DataNascimento { get; set; }

    }
}
