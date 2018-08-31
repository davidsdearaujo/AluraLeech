using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraBot.Browser.Entidades
{
    public class CursoDTO
    {
        public string Nome { get; set; }
        public List<AulaDTO> Aulas { get; set; }
        public string DescricaoErro { get; set; }
        public string Url { get; set; }
    }
}
