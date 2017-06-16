using System;
using System.Threading.Tasks;

using DevDays.Models.ContentItems;
using DevDays.Services;

using Xamarin.Forms;

namespace DevDays.ViewModels
{
    public class ContentItemDetailViewModel : BaseViewModel
    {
        public Document ContentItem { get; set; }
        public IContentTypeDataStore<Models.ContentTypes.Document> DataStore => DependencyService.Get<IContentTypeDataStore<Models.ContentTypes.Document>>();
        public IContentItemDataStore<Document> ItemDataStore => DependencyService.Get<IContentItemDataStore<Document>>();

        public ContentItemDetailViewModel(Document document = null)
        {

            try
            {
                if (document == null) return;

                document.contentType = ExecuteLoadDocumentsCommand(document.contentTypeId).Result;

                Title = document.name;
                ContentItem = document;

            }
            catch (Exception e)
            {
                Title = "none";
            }
        }

        private async Task<Models.ContentTypes.Document> ExecuteLoadDocumentsCommand(string contentTypeId)
        {
            return await DataStore.GetContentTypeAsync(contentTypeId);
        }
    }
}