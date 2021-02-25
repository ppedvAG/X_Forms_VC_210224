﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using X_Forms.BspMVVM.Model;

namespace X_Forms.BspMVVM.Service
{
    public class DbService
    {
        static object locker = new object();

        //Connection-Objekt
        SQLiteConnection database;

        //Konstruktor
        public DbService()
        {
            //Mittels des lock-Stichworts kann der Datenbankzugriff mittels eines spezifischen Objekts limitiert werden
            lock (locker)
            {
                string ordner = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string dbName = "MVVM.db3";

                string path = Path.Combine(ordner, dbName);

                database = new SQLiteConnection(path);

                //Erstellen einer neuen Person-Tabelle (wenn noch nicht vorhanden)
                database.CreateTable<Person>();
            }
        }

        //Methoden für die verschiedenen Datenbankzugriffsarten
        public Person GetPerson(Guid id)
        {
            lock (locker)
            {
                //Erfragen eines einzelnen Person-Objekts anhand der Id
                return database.Get<Person>(id);
            }
        }

        public List<Person> GetPeople()
        {
            lock (locker)
            {
                //Erfragen aller Person-Objekte der Datenbank
                return database.Table<Person>().ToList();
            }
        }

        public int AddPerson(Person p)
        {
            lock (locker)
            {
                //Hinzufügen einer Person zur Datenbank
                return database.Insert(p);
            }
        }

        public int UpdatePerson(Person p)
        {
            lock (locker)
            {
                //Aktualisieren einer Person in der Datenbank
                return database.Update(p);
            }
        }

        public int DeletePerson(Person p)
        {
            lock (locker)
            {
                //Löschen einer Person in der Datenbank
                return database.Delete(p);
            }
        }

    }
}
