using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DevDays.Helpers;
using DevDays.Models.ContentItems;
using DevDays.Services;
using DevDays.Views;

using Xamarin.Forms;

namespace DevDays.ViewModels
{
    public class ContentItemViewModel : BaseViewModel
    {
        public IContentItemDataStore<Document> DataStore => DependencyService.Get<IContentItemDataStore<Document>>();
        public ObservableRangeCollection<Document> ContentItems { get; set; }
        public Command LoadCommand { get; set; }

        public ContentItemViewModel()
        {
            Title = "DevDays Content Items";
            ContentItems = new ObservableRangeCollection<Document>();
            LoadCommand = new Command(async () => await ExecuteLoadDocumentsCommand());

            MessagingCenter.Subscribe<NewContentTypePage, Document>(this, "AddContentItem", async (obj, document) =>
            {
                var _document = document as Document;
                ContentItems.Add(_document);
                await DataStore.AddContentItemAsync(_document);
            });
        }

        private async Task ExecuteLoadDocumentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ContentItems.Clear();
                var contentItems = await DataStore.GetContentItemsAsync(true);
                ContentItems.ReplaceRange(contentItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load content items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}