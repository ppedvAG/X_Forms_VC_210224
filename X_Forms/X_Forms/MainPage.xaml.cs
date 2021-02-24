using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace X_Forms
{
    public partial class MainPage : ContentPage
    {
        //Property zum Zwischenspeichern der Personenliste (ObservableCollection ist eine generische Liste, welche die GUI
        //automatisch über Veränderungen informiert
        public ObservableCollection<string> Namensliste { get; set; }

        //Konstruktor
        public MainPage()
        {
            //Initialisierung der UI (Xaml-Datei). Sollte immer erste Aktion des Konstruktors sein
            InitializeComponent();

            //Zuweisung von EventHandlern zu den Completed-Events, damit ein besserer Bedienfluss gegeben ist
            Ent_Vorname.Completed += (s, e) => Ent_Nachname.Focus();
            //Erstellen eines neuen Listenelements (aus UI-Properties)
            Ent_Nachname.Completed += (s, e) => Namensliste.Add(Ent_Vorname.Text + " " + Ent_Nachname.Text);

            //Initialisierung der Namesliste
            Namensliste = new ObservableCollection<string>()
            {
                "Anna Nass",
                "Rainer Zufall"
            };

            //Durch Setzen des BindingContextes nehmen Kurzbindungen aus dem XAML-Code automatisch Bezug auf die Properties
            //des im BindingContext gesetzten Objekts
            this.BindingContext = this;
        }

        //EventHandler
        private void Btn_KlickMich_Clicked(object sender, EventArgs e)
        {
            //Neuzuweisung einer UI-Property über die x:Name-Property des Steuerelements
            Lbl_Main.Text = "Danke, dass du auf den Button geklickt hast.";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Anzeige einer 'MessageBox' und Abfrage der User-Antwort
            bool erg = await DisplayAlert("Begrüßung", "Sei gegrüßt", "Danke", "Interessiert mich nicht");

            if (erg)
                await DisplayAlert("Gerne", "Gerne", "ok");
        }
    }
}
