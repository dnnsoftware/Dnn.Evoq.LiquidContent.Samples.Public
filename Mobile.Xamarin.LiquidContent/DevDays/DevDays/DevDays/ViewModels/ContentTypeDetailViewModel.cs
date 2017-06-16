using DevDays.Models.ContentTypes;

namespace DevDays.ViewModels
{
    public class ContentTypeDetailViewModel : BaseViewModel
    {
        public Document ContentType { get; set; }

        public ContentTypeDetailViewModel(Document contentType = null)
        {
            Title = contentType?.name;
            ContentType = contentType;
        }
    }
}