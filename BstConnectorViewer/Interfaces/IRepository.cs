
using System.Collections.Generic;
using BstConnectorViewer.Models;


namespace BstConnectorViewer.Interfaces
{
    public interface IRepository
    {
        IEnumerable<DocumentInfo> GetAllDocInfos(string routeName);

        DocumentDetails GetSpecificDoc(string routeName, string id);

        IEnumerable<Transform> GetAllTransforms();

        IEnumerable<DocumentInfo> GetChildDocumentInfos(string documentType, string id, string childDocumentType);

        DocumentDetails GetSpecificChildDoc(string documentType, string rootDocId, string childDocumentType, string childDocId);
    }
}
