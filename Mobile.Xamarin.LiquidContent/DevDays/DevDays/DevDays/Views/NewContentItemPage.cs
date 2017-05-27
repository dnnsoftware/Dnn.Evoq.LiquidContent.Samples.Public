using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using DevDays.Models.ContentTypes;
using DevDays.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Document = DevDays.Models.ContentItems.Document;

namespace DevDays.Views
{
    public class NewContentItemPage : ContentPage
    {
        ContentItemDetailViewModel _itemModel = new ContentItemDetailViewModel();

        ContentTypeDetailViewModel typeViewModel;

        private StackLayout _layout;

        public NewContentItemPage(ContentTypeDetailViewModel viewModel)
        {
            typeViewModel = viewModel;
            BindingContext = _itemModel;

            _layout = new StackLayout
            {
                Spacing = 20,
                Padding = 15
            };

            // mimic grabbing a column name at runtime and adding it as a property

            foreach (var field in typeViewModel.ContentType.fields)
            {
                var fieldLabel = new Label { Text = field.label, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)) };
                _layout.Children.Add(fieldLabel);

                switch (field.type)
                {
                    case "singleLineText":
                        var fieldEntry = new Entry
                        {
                            StyleId = field.name,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry))
                        };

                        _layout.Children.Add(fieldEntry);
                        break;

                    case "multiLineText":
                        var fieldEditor = new Editor
                        {
                            StyleId = field.name,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Editor)),
                            HeightRequest = 150
                        };

                        _layout.Children.Add(fieldEditor);
                        break;

                    default:
                        var fieldDefault = new Entry
                        {
                            StyleId = field.name,
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry))
                        };

                        _layout.Children.Add(fieldDefault);
                        break;
                }
            }

            Title = $"New {typeViewModel.ContentType.name}";
            Content = _layout;

            var draft = new ToolbarItem
            {
                Text = "Draft",
                Command = new Command<bool>(SaveContentItem),
                CommandParameter = false
            };

            ToolbarItems.Add(draft);

            var publish = new ToolbarItem
            {
                Text = "Publish",
                Command = new Command<bool>(SaveContentItem),
                CommandParameter = true
            };

            ToolbarItems.Add(publish);
        }

        private void SaveContentItem(bool publish)
        {
            var _details = new JObject();

            foreach (var children in _layout.Children)
            {
                if (children.GetType() != typeof(Entry) && children.GetType() != typeof(Editor)) continue;

                var property = children.GetType() == typeof(Entry) ? Entry.TextProperty : Editor.TextProperty;

                if (typeViewModel.ContentType.fields.Count(w => w.name == children.StyleId) == 1)
                {
                    _details.Add(children.StyleId, (string)children.GetValue(property));
                }
            }
            
            _itemModel.ContentItem = new Document
            {
                name = Title,
                slug = $"{Title.ToLowerInvariant().Replace(" ","")}-1",
                contentTypeName = typeViewModel.ContentType.name,
                contentTypeId = typeViewModel.ContentType.id,
                details = _details,
                alreadyPublished = publish,
                stateId = publish ? 1: 0
            };

            _itemModel.ItemDataStore.AddContentItemAsync(_itemModel.ContentItem);

            Navigation.PushAsync(new ContentItemPage());
        }
    }
}