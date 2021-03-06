﻿using SmartKey.Controller;
using SmartKey.Controller.Controller.Interfaces;
using SmartKey.DataPersistence;
using SmartKey.Log.ModelLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartKey.Blacklist
{
    public class BlackListController : IGestoreBlacklist
    {
        //Eventi
        public event EventHandler<ActionCompletedEvent> ToLog;
        public event EventHandler<PersistEvent> Persist;

        private Blacklist _blacklist;
        private HomeBlacklist _blacklistView;

        public BlackListController()
        {
            _blacklist = Blacklist.Instance;
        }
        
        public BlackListController(HomeBlacklist _view)
        {
            _blacklist = Blacklist.Instance;
            _blacklistView = _view;
            _blacklistView.ButtonRimuovi.Click += RimuoviUtenteHandler;
        }
        private void RimuoviUtenteHandler(object sender, EventArgs args)
        {
            foreach (DataGridViewRow row in _blacklistView.DataGridViewBlacklist.SelectedRows)
            {
                string utente = row.Cells[0].Value.ToString();
                Console.WriteLine(utente);
                RimuoviUtente(utente);               
                _blacklistView.DataGridViewBlacklist.Rows.Remove(row);
                
            }
        }
        public void AggiungiUtente(string utente)
        {
            if (_blacklist.AggiungiUtenteCattivo(utente))
            {
                //Col fatto che questa viene chiamata dal thread della sincronizzazione va
                //Risolto con l'escamotage 
                _blacklistView.AddRow(utente);
                //_blacklistView.DataGridViewBlacklist.Rows.Add(utente);
                //Creazione del parametro da passare quando scateno l'evento
                ActionCompletedEvent args = new ActionCompletedEvent
                {
                    ToEntry = EntryFactory.CreateEntry(this, "aggiunto", utente)
                };
                PersistEvent toPersist = new PersistEvent
                {
                    Action = "aggiungi",
                    ToPersist = utente
                };
                //scateno gli handler registrati all'evento
                ToLog?.Invoke(this, args);
                Persist?.Invoke(this, toPersist);
            }
            else
            {
                //Creazione del parametro da passare quando scateno l'evento
                ActionCompletedEvent args = new ActionCompletedEvent
                {
                    ToEntry = EntryFactory.CreateEntry(this, "nonaggiunto", utente)
                };
                //scateno gli handler registrati all'evento
                ToLog?.Invoke(this, args);
            }
        }

        public bool IsBlackListed(string utente)
        {
            return _blacklist.IsInBlacklist(utente);
        }

        public void RimuoviUtente(string utente)
        {
            if (_blacklist.EliminaUtenteCattivo(utente))
            {
                //Creazione del parametro da passare quando scateno l'evento
                ActionCompletedEvent args = new ActionCompletedEvent
                {
                    ToEntry = EntryFactory.CreateEntry(this, "rimosso", utente)
                };
                //scateno gli handler registrati all'evento
                PersistEvent toPersist = new PersistEvent
                {
                    Action = "rimuovi",
                    ToPersist = utente
                };
                //scateno gli handler registrati all'evento
                ToLog?.Invoke(this, args);
                Persist?.Invoke(this, toPersist);
            }
            else
            {
                //Creazione del parametro da passare quando scateno l'evento
                ActionCompletedEvent args = new ActionCompletedEvent
                {
                    ToEntry = EntryFactory.CreateEntry(this, "nonrimosso", utente)
                };
                //scateno gli handler registrati all'evento
                ToLog?.Invoke(this, args);
            }
        }

        public void SetBlackList(ISet<string> blacklist)
        {
            _blacklist.Utenti = blacklist;
            foreach(string utente in _blacklist.Utenti)
            {
                _blacklistView.DataGridViewBlacklist.Rows.Add(utente);
            }

        }
    }
}
