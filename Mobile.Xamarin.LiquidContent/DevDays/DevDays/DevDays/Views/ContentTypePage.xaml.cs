using System;
using DevDays.Models.ContentTypes;
using DevDays.ViewModels;

using Xamarin.Forms;

namespace DevDays.Views
{
    public partial class ContentTypePage : ContentPage
    {
        private readonly ContentTypeViewModel _viewModel;

        public ContentTypePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ContentTypeViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var document = args.SelectedItem as Document;
            if (document == null)
                return;

            await Navigation.PushAsync(new ContentTypeDetailsPage(new ContentTypeDetailViewModel(document)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewContentTypePage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.ContentTypes.Count == 0)
                _viewModel.LoadCommand.Execute(null);
        }
    }
}
