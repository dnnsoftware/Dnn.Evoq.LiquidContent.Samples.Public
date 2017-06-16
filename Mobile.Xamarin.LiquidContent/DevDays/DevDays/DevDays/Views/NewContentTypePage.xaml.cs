using System;
using System.Collections.Generic;
using DevDays.Models;
using DevDays.Models.ContentTypes;

using Xamarin.Forms;

namespace DevDays.Views
{
    public partial class NewContentTypePage : ContentPage
    {
        public ContentTypes ContentType { get; set; }

        public NewContentTypePage()
        {
            InitializeComponent();

            ContentType = new ContentTypes
            {
                documents = new List<Document>() { new Document() { name = "New Content Type", description = "Description" } }
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddContentType", ContentType);
            await Navigation.PopToRootAsync();
        }
    }
}