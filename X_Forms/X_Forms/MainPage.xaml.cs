using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            //Setzten der Ressourcensprache -> Bestimmt, welche resx-Bibliothek für die Lokalisierung verwendet wird
            Properties.Resources.Culture = new System.Globalization.CultureInfo("en");

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

            //Zugriff auf Xamarin.Essentials.Battery zur Anzeige des Batteriestandes (benötigt BatteryState-Permission)
            Lbl_Battery.Text = Battery.State.ToString() + " | Level: " + Battery.ChargeLevel * 100 + "%";
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

        private void Btn_Push(object sender, EventArgs e)
        {
            //Aufruf einer neuen Seite innerhalb der aktuellen NavigationPage 
            Navigation.PushAsync(new NavigationBsp.TabbedPageBsp());
        }

        private void Btn_PushModal(object sender, EventArgs e)
        {
            //Aufruf einer neuen Seite innerhalb der aktuellen NavigationPage, welche aber keinen 'Zurück'-Button anzeigt
            Navigation.PushModalAsync(new Layouts.RelativeLay());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Namensliste.Clear();
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            //Anzeige einer 'MessageBox' und Abfrage der User-Antwort
            bool result = await DisplayAlert("Löschung", "Soll diese Person wirklich gelöscht werden?", "Ja", "Nein");

            if (result)
            {
                //Löschen eines Listeneintrags
                string person = (sender as MenuItem).CommandParameter.ToString();

                Namensliste.Remove(person);
            }
        }

        private void Btn_Carousel(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationBsp.CarouselPageBsp());
        }

        //Bsp für Verwendung des MessagingCenters
        private void Btn_MC_Clicked(object sender, EventArgs e)
        {
            //Instanzieren des Empängerobjekts
            Page page = new Pg_MCSubscriber();

            //Senden der Nachricht mit Angabe des Senders, des Titels und des Inhalts
            MessagingCenter.Send(this, "Gesendeter Text", Pkr_Monkeys.SelectedItem.ToString());

            //Öffnen der Bsp-Seite
            Navigation.PushAsync(page);
        }

        private async void Btn_YouTube_Clicked(object sender, EventArgs e)
        {
            //Öffnen der Youtube-App über die Xamarin-Essentials mit Übergabe des Package-Namens
            if (await Launcher.CanOpenAsync("vnd.youtube://"))
                await Launcher.OpenAsync("vnd.youtube://rLKnqR9Oqh8");
        }
    }
}
