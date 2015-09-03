using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_Asky.Tables
{
    public class Jerarquia
    {
        public int jerarquiaid { get; set; }
        public int talento_humano { get; set; }
        public int jefe_talentohumano { get; set; }
        public int departamentoid { get; set; }
    }
}