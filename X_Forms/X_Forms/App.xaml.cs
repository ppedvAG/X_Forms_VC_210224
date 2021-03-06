﻿using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_Forms
{
    public partial class App : Application
    {
        //Die App-Klasse beinhaltet eine grundlegen Initialisierung der App sowie die MainPage-Property, welche defniert,
        //welche Page gerade in der App angezeigt wird. Diese Property wird auch als Einstiegspunkt verwendet.
        public App()
        {
            InitializeComponent();

            //Zuweisung der MainPage-Property zu einer Page
            //MainPage = new MainPage();

            //Zuweisung der MainPage - Property zu einer NavigationPage(ermöglicht Stack - Navigation) mit Angabe der Startpage.
            //MainPage = new NavigationPage(new MainPage());

            //ZUweisung der MasterDetailPage als Hauptnavigation zu der MainPage-Property
            MainPage = new NavigationBsp.MasterDetail.MDP();
        }

        public DateTime TimeStamp { get; set; }

        //Methoden, welche zu bestimmten globalen Events ausgeführt werden (Start, Unterbrechen der App [Sleep], Wiederaktivierung der App [Resume])
        protected override void OnStart()
        {
            if(Preferences.ContainsKey("timestamp"))
                MainPage.DisplayAlert("Time", $"Gespeicherte Zeit: {Preferences.Get("timestamp", DateTime.Now)}", "ok");
            else
                MainPage.DisplayAlert("Time", $"Startzeit: {DateTime.Now}", "ok");
        }

        protected override void OnSleep()
        {
            TimeStamp = DateTime.Now;

            Preferences.Set("timestamp", DateTime.Now);
        }

        protected override void OnResume()
        {
            DateTime time = Preferences.Get("timestamp", DateTime.Now);

            MainPage.DisplayAlert("Time", $"Geschlafene Zeit: {DateTime.Now.Subtract(time).TotalSeconds}", "ok");
        }
    }
}
