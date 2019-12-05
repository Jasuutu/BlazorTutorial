using System.Collections.Generic;

namespace BstConnectorViewer.Interfaces
{
    public interface IDataRetriever
    {
        string GetAllDocuments(string documentTypeName);

        string GetSpecificDocument(string documentType, string id);

        IEnumerable<string> GetAllTransforms();

        string GetChildDocuments(string documentType, string rootDocumentId, string childDocumentType);
        
        string GetSpecificChildDocument(string documentType, string rootDocumentId, string childDocumentType, string childDocumentId);

        string CheckPingResponse();
    }
}