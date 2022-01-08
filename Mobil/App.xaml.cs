using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobil.Data;
using System.IO;

namespace Mobil
{
    public partial class App : Application
    {
        static RentalListDatabase database;
        public static RentalListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new
                   RentalListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RentalList.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ListEntryPage());
        }
    }
}
