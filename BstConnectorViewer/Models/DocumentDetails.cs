using System.Collections.Generic;
#nullable disable
namespace BstConnectorViewer.Models
{
    public class DocumentDetails
    {
        public string DocumentType { get; }
        public IEnumerable<PropertyInfoModel> Properties { get; set; }

        public DocumentDetails(string documentType)
        {
            DocumentType = documentType;
            Properties = new List<PropertyInfoModel>();
        }
    }
}