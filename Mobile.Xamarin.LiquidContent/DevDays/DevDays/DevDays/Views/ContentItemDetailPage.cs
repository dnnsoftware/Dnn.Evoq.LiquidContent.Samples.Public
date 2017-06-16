using DevDays.ViewModels;

using Xamarin.Forms;

namespace DevDays.Views
{
    public class ContentItemDetailsPage : ContentPage
    {
        ContentItemDetailViewModel viewModel;

        public ContentItemDetailsPage(ContentItemDetailViewModel viewModel)
        {
            BindingContext = this.viewModel = viewModel;

            var layout = new StackLayout
            {
                Spacing = 20,
                Padding = 15
            };

            foreach (var field in viewModel.ContentItem.contentType.fields)
            {
                var fieldLabel = new Label { Text = field.label, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
                layout.Children.Add(fieldLabel);                  

                switch (field.type)
                {
                    case "singleLineText":
                        var fieldEntry = new Entry
                        {
                            Text = viewModel.ContentItem.details.GetValue(field.name).ToString(),
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry))
                        };

                        layout.Children.Add(fieldEntry);
                        break;
                    case "multiLineText":
                        var fieldEditor = new Editor
                        {
                            Text = viewModel.ContentItem.details.GetValue(field.name).ToString(),
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Editor)),
                            HeightRequest = 150
                        };

                        layout.Children.Add(fieldEditor);
                        break;
                }
            }
        
            Title = viewModel.ContentItem.name;
            Content = layout;
        }
    }
}