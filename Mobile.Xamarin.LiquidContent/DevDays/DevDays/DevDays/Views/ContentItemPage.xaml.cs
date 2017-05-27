using System;
using DevDays.Models.ContentItems;
using DevDays.ViewModels;
using Xamarin.Forms;

namespace DevDays.Views
{
    public partial class ContentItemPage : ContentPage
    {
        private readonly ContentItemViewModel _viewModel;

        public ContentItemPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ContentItemViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var document = args.SelectedItem as Document;
            if (document == null)
                return;

            await Navigation.PushAsync(new ContentItemDetailsPage(new ContentItemDetailViewModel(document)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContentTypeSelectorPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.ContentItems.Count == 0)
                _viewModel.LoadCommand.Execute(null);
        }
    }
}
