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
    public partial class MoviePage : ContentPage
    {
        RentalList rl;
        public MoviePage(RentalList rlist)
        {
            InitializeComponent();
            rl = rlist;
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var movie = (Movie)BindingContext;
            await App.Database.SaveMovieAsync(movie);
            listView.ItemsSource = await App.Database.GetMoviesAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var movie = (Movie)BindingContext;
            await App.Database.DeleteMovieAsync(movie);
            listView.ItemsSource = await App.Database.GetMoviesAsync();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetMoviesAsync();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Movie p;
            if (e.SelectedItem != null)
            {
                p = e.SelectedItem as Movie;
                var lp = new ListMovie()
                {
                    RentalListID = rl.ID,
                    MovieID = p.ID
                };
                await App.Database.SaveListMovieAsync(lp);
                p.ListMovies = new List<ListMovie> { lp };

                await Navigation.PopAsync();
            }
        }
    }
}