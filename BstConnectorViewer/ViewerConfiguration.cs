using Microsoft.Extensions.Configuration;

namespace BstConnectorViewer
{
    public class ViewerConfiguration
    {
        public string BaseUrl { get; set; }

        public string CertificateSerialNumber { get; set; }

        public ViewerConfiguration(IConfiguration config)
        {
            BaseUrl = config.GetSection("BaseUrl").Value;
            CertificateSerialNumber = config.GetSection("CertNumber").Value;
        }

        public ViewerConfiguration(string baseUrl, string certificateSerialNumber)
        {
            BaseUrl = baseUrl;
            CertificateSerialNumber = certificateSerialNumber.ToUpper();
        }
    }
}