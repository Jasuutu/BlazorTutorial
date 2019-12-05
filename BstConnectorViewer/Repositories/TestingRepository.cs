using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BstConnectorViewer.Interfaces;
using BstConnectorViewer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BstConnectorViewer.Repositories
{
    public class TestingRepository
    {
        private IDataRetriever retriever;

        public TestingRepository(IDataRetriever retriever)
        {
            this.retriever = retriever;
        }

        public DocumentDetails CheckPingRoute()
        {
            var rawDoc = JObject.Parse(retriever.CheckPingResponse());
            var docDetails = new DocumentDetails("ping");
            return AddPropertiesToDocDetails(docDetails, rawDoc);
        }

        public PressureRecord PressureTest(int startPosition)
        {
            var record = new PressureRecord { StartTime = 0, StartPosition = startPosition };
            string result = string.Empty;
            var timer = Stopwatch.StartNew();
            try
            {
                result = retriever.GetAllDocuments("projects");
            }
            catch (WebException e)
            {
                record.IsValid = false;
            }
            timer.Stop();

            record.EndTime = timer.ElapsedMilliseconds;
            record.IsValid = !string.IsNullOrEmpty(result);



            return record;
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