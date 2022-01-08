using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobil.Models;

namespace Mobil
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
            public ListPage()
            {
                InitializeComponent();
            }
            async void OnSaveButtonClicked(object sender, EventArgs e)
            {
                var slist = (RentalList)BindingContext;
                slist.Date = DateTime.UtcNow;
                await App.Database.SaveRentalListAsync(slist);
                await Navigation.PopAsync();
            }
            async void OnDeleteButtonClicked(object sender, EventArgs e)
            {
                var slist = (RentalList)BindingContext;
                await App.Database.DeleteRentalListAsync(slist);
                await Navigation.PopAsync();
            }

            async void OnChooseButtonClicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new MoviePage((RentalList)this.BindingContext)
                {
                    BindingContext = new Movie()
                });

            }

            protected override async void OnAppearing()
            {
                base.OnAppearing();
                var rentall = (RentalList)BindingContext;

                listView.ItemsSource = await App.Database.GetListMoviesAsync(rentall.ID);
            }
    }
}