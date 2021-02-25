using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using X_Forms.BspMVVM.Model;
using X_Forms.BspMVVM.Service;
using Xamarin.Forms;

namespace X_Forms.BspMVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Page ContextPage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DbService Datenbank { get; set; }

        public string NeuerVorname { get; set; }
        public string NeuerNachname { get; set; }

        public ObservableCollection<Person> PersonenListe 
        { 
            get { return Model.Person.PersonenListe; }
            set { Model.Person.PersonenListe = value; } 
        }

        public Command AddCmd { get; set; }
        public Command DeleteCmd { get; set; }

        public MainViewModel()
        {
            Datenbank = new DbService();
            PersonenListe = new ObservableCollection<Person>(Datenbank.GetPeople());

            AddCmd = new Command(AddPerson);
            DeleteCmd = new Command(DeletePerson);
        }

        private void AddPerson()
        {
            Person person = new Person()
            {
                Vorname = NeuerVorname,
                Nachname = NeuerNachname
            };

            PersonenListe.Add(person);
            Datenbank.AddPerson(person);

            NeuerVorname = String.Empty;
            NeuerNachname = String.Empty;

            InformView(nameof(NeuerVorname));
            InformView(nameof(NeuerNachname));
        }

        private async void DeletePerson(object person)
        {
            bool result = await ContextPage.DisplayAlert("Löschen", "Soll diese Person wirklich gelöscht werden?", "Ja", "Nein");

            if (result)
            {
                Datenbank.DeletePerson(person as Person);
                PersonenListe.Remove(person as Person);
            }
        }

        private void InformView(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
