using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace X_Forms.BspMVVM.Model
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }

        public string Vorname { get; set; }
        public string Nachname { get; set; }

        [Ignore]
        public static ObservableCollection<Person> PersonenListe { get; set; } = new ObservableCollection<Person>();
    }
}
