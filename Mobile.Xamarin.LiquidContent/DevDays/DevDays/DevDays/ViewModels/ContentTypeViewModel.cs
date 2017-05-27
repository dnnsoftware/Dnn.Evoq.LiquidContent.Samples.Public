using System;
using System.Diagnostics;
using System.Threading.Tasks;

using DevDays.Helpers;
using DevDays.Models.ContentTypes;
using DevDays.Services;
using DevDays.Views;

using Xamarin.Forms;

namespace DevDays.ViewModels
{
    public class ContentTypeViewModel : BaseViewModel
    {
        public IContentTypeDataStore<Document> DataStore => DependencyService.Get<IContentTypeDataStore<Document>>();
        public ObservableRangeCollection<Document> ContentTypes { get; set; }
        public Command LoadCommand { get; set; }

        public ContentTypeViewModel()
        {
            Title = "DevDays Content Types";
            ContentTypes = new ObservableRangeCollection<Document>();
            LoadCommand = new Command(async () => await ExecuteLoadDocumentsCommand());

            MessagingCenter.Subscribe<NewContentTypePage, Document>(this, "AddContentType", async (obj, contentType) =>
            {
                var _contentType = contentType;
                if (_contentType == null) return;

                ContentTypes.Add(_contentType);
                await DataStore.AddContentTypeAsync(_contentType);
            });
        }

        private async Task ExecuteLoadDocumentsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ContentTypes.Clear();
                var contentTypes = await DataStore.GetContentTypesAsync(true);
                ContentTypes.ReplaceRange(contentTypes);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load content types.",
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