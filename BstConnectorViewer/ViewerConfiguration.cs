using Microsoft.Extensions.Configuration;

namespace BstConnectorViewer
{
    public class ViewerConfiguration
    {
        public string BaseUrl { get; set; }

        public string CertificateSerialNumber { get; set; }

        public ViewerConfiguration(IConfiguration config)
        {
            var configBaseUrl = config.GetSection("BaseUrl").Value;
            if (!string.IsNullOrEmpty(configBaseUrl))
            {
                BaseUrl = configBaseUrl;
            }
            else
            {
                BaseUrl = "https://localhost/";
            }
            
            CertificateSerialNumber = config.GetSection("CertNumber").Value;
        }

        public ViewerConfiguration(string baseUrl, string certificateSerialNumber)
        {
            BaseUrl = baseUrl;
            CertificateSerialNumber = certificateSerialNumber.ToUpper();
        }
    }
}