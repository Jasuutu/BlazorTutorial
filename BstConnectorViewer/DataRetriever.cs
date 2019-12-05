using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using BstConnectorViewer.Interfaces;

namespace BstConnectorViewer
{
    public class DataRetriever : IDataRetriever
    {
        private ViewerConfiguration config;
        public DataRetriever(ViewerConfiguration config)
        {
            this.config = config;
        }

        public string GetAllDocuments(string documentTypeName)
        {
            var newRequest = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/{documentTypeName}");
            
            newRequest.Method = "GET";
            return GetWebResponse(newRequest);
        }

        public string GetSpecificDocument(string documentType, string id)
        {
            var newRequest = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/{documentType}/{id}");
            
            return GetWebResponse(newRequest);
        }

        public IEnumerable<string> GetAllTransforms()
        {
            var clientRequest = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/transforms/client/properties/id");
            var clientResponse = GetWebResponse(clientRequest);

            var currencyRequest = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/transforms/currency/properties/code");
            var currencyResponse = GetWebResponse(currencyRequest);

            return new List<string> {clientResponse, currencyResponse};
        }

        public string GetChildDocuments(string documentType, string rootDocumentId, string childDocumentType)
        {
            var request = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/{documentType}/{rootDocumentId}/{childDocumentType}");
            
            return GetWebResponse(request);
        }

        public string GetSpecificChildDocument(string documentType, string rootDocumentId, string childDocumentType, string childDocumentId)
        {
            var request = (HttpWebRequest) WebRequest.Create(
                $"{config.BaseUrl}v1/{documentType}/{rootDocumentId}/{childDocumentType}/{childDocumentId}");
            return GetWebResponse(request);
        }

        public string CheckPingResponse()
        {
            var request = (HttpWebRequest) WebRequest.Create($"{config.BaseUrl}v1/ping");
            return GetWebResponse(request);
        }

        private string GetWebResponse(HttpWebRequest newRequest)
        {
            var certificate = GetCertificate();
            if (certificate != null)
            {
                newRequest.ClientCertificates.Add(certificate);
            }
            string content;
            using (var response = (HttpWebResponse)newRequest.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream ?? throw new InvalidOperationException()))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }

        private X509Certificate GetCertificate()
        {
            if (string.IsNullOrEmpty(config.CertificateSerialNumber))
            {
                return null;
            }
            var localMachineStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            localMachineStore.Open(OpenFlags.ReadOnly);
            var certColl = localMachineStore.Certificates.Find(X509FindType.FindBySerialNumber,config.CertificateSerialNumber,
                true);
            localMachineStore.Close();

            if (certColl.Count == 0)
            {
                var personalStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                personalStore.Open(OpenFlags.ReadOnly);
                certColl = personalStore.Certificates.Find(X509FindType.FindBySerialNumber, config.CertificateSerialNumber, true);
                personalStore.Close();
            }

            X509Certificate cert;
            if (certColl.Count == 1 && certColl[0] != null)
                cert = certColl[0];
            else
            {
                throw new InvalidOperationException("Bad cert!");
            }
            return cert;
        }
    }
}
