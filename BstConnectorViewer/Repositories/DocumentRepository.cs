using System.Collections.Generic;
using BstConnectorViewer.Interfaces;
using BstConnectorViewer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BstConnectorViewer.Repositories
{
    public class DocumentRepository : IRepository
    {
        private readonly IDataRetriever retriever;

        public DocumentRepository(IDataRetriever retriever)
        {
            this.retriever = retriever;
        }

        public IEnumerable<DocumentInfo> GetAllDocInfos(string routeName)
        {
            var docInfos = new List<DocumentInfo>();
            string content = retriever.GetAllDocuments(routeName);

            if (!string.IsNullOrEmpty(content))
            {
                docInfos.AddRange(JsonConvert.DeserializeObject<IEnumerable<DocumentInfo>>(content));
            }

            return docInfos;
        }

        public DocumentDetails GetSpecificDoc(string routeName, string id)
        {
            var rawDoc = JObject.Parse(retriever.GetSpecificDocument(routeName, id));
            var docDetails = new DocumentDetails(routeName);
            return AddPropertiesToDocDetails(docDetails, rawDoc);
        }

        public IEnumerable<Transform> GetAllTransforms()
        {
            List<Transform> transforms = new List<Transform>();
            var transformOutputs = retriever.GetAllTransforms();
            foreach (var output in transformOutputs)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    transforms.AddRange(JsonConvert.DeserializeObject<IEnumerable<Transform>>(output));
                }
            }

            return transforms;
        }

        public IEnumerable<DocumentInfo> GetChildDocumentInfos(string documentType, string id, string childDocumentType)
        {
            var docInfos = new List<DocumentInfo>();
            string content = retriever.GetChildDocuments(documentType, id, childDocumentType);

            if (!string.IsNullOrEmpty(content))
            {
                docInfos.AddRange(JsonConvert.DeserializeObject<IEnumerable<DocumentInfo>>(content));
            }

            return docInfos;
        }

        public DocumentDetails GetSpecificChildDoc(string documentType, string rootDocId, string childDocumentType, string childDocId)
        {
            var content = JObject.Parse(retriever.GetSpecificChildDocument(documentType, rootDocId, childDocumentType, childDocId));
            var docDetails = new DocumentDetails(documentType);
            return AddPropertiesToDocDetails(docDetails, content);
        }

        private DocumentDetails AddPropertiesToDocDetails(DocumentDetails docDetails, JObject rawDoc)
        {
            List<PropertyInfoModel> propInfos = new List<PropertyInfoModel>();

            foreach (JProperty prop in rawDoc.Properties())
            {
                var value = prop.Value?.ToString();
                propInfos.Add(new PropertyInfoModel($"\"{prop.Name}\": ", string.IsNullOrEmpty(value) ? "null" : $"\"{value}\""));
            }

            docDetails.Properties = propInfos;
            return docDetails;
        }
    }
}