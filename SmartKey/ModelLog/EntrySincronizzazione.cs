﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartKey.ModelLog
{
    class EntrySincronizzazione : Entry
    {
        private string _sorgente;
        private string _destinatario;

        public EntrySincronizzazione(string operazione, string sorgente, string destinatario) : base (operazione)
        {
            _sorgente = sorgente;
            _destinatario = destinatario;

        }
        public string Sorgente
        {
            get
            {
                return _sorgente;
            }
        }
        public string Destinatario
        {
            get
            {
                return _destinatario;
            }
        }
    }
}
