﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnulaciónDte.Models
{
    public class Ambiente_Destino
    {
        [Key]
        public string CODIGO { get; set; }
        public string VALORES { get; set; }
    }
}
