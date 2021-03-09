using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWebAPIAspCore.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public  int AnoLancamento { get; set; }
        public string Resumo { get; set; }
    }

    public class FilmeOutputModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AnoLancamento { get; set; }
        public string Resumo { get; set; }
        public DateTime UltimoAcesso { get; set; }
    }

    public class FilmeInputModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AnoLancamento { get; set; }
        public string Resumo { get; set; }
    }
}