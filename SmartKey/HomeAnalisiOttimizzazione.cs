﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartKey
{
    public partial class HomeAnalisiOttimizzazione : SmartKey.BaseForm
    {

        public HomeAnalisiOttimizzazione()
        {
            InitializeComponent();
        }

        public Button ButtonToLogAnalisi
        {
            get
            {
                return buttonLogCompressione;
            }
        }

        public Button ButtonComprimi
        {
            get
            {
                return buttonComprimi;
            }
        }
        
        public Button ButtonPulisciLista
        {
            get
            {
                return buttonPulisciLista;
            }
        }
        
    }
}
