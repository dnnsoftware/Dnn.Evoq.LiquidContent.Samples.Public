using DevDays.ViewModels;

using Xamarin.Forms;

namespace DevDays.Views
{
    public class ContentTypeDetailsPage : ContentPage
    {
        ContentTypeDetailViewModel viewModel;

        public ContentTypeDetailsPage(ContentTypeDetailViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            var layout = new StackLayout
            {
                Spacing = 20,
                Padding = 15
            };

            var nameLabel = new Label { Text = "Name", FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
            var nameEntry = new Label
            {
                Text = viewModel.ContentType.name,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry))
            };
            var descriptionLabel = new Label
            {
                Text = "Description",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            var descriptionEditor = new Label
            {
                Text = viewModel.ContentType.description,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Editor)),
                Margin = 0
            };
            var fieldListLabel = new Label
            {
                Text = "Fields",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };

            layout.Children.Add(nameLabel);
            layout.Children.Add(nameEntry);
            layout.Children.Add(descriptionLabel);
            layout.Children.Add(descriptionEditor);
            layout.Children.Add(fieldListLabel);

            var fieldList = new ListView
            {
                ItemsSource = viewModel.ContentType.fields,
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();

                    textCell.SetBinding(TextCell.TextProperty, "label");
                    textCell.SetBinding(TextCell.DetailProperty, "type");
                    textCell.TextColor = Color.FromHex("737373");
                    textCell.DetailColor = Color.Gray;

                    return textCell;
                }),
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            layout.Children.Add(fieldList);

            Title = viewModel.ContentType.name;
            Content = layout;
        }
    }
}