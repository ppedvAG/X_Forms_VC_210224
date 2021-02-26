using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X_Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pg_MCSubscriber : ContentPage
    {
        public Pg_MCSubscriber()
        {
            InitializeComponent();

            //Abonieren der Nachricht unter Angabe von <Sender, Inhaltstyp>(Empfänger, Titel, Callback-Methode)
            MessagingCenter.Subscribe<MainPage, string>(this, "Gesendeter Text", SetzteText);
        }

        //Methode, welche bei Empfang einer Nachricht ausgeführt wird
        private void SetzteText (MainPage sender, string text)
        {
            Lbl_Main.Text = text;
        }
    }
}