using System;

namespace BstConnectorViewer.Models
{
    /// <summary>
    ///  Client is an entity that contracts services from the Company.
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        /// Internal system key that uniquely identifies the Client.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Alphanumeric business key that identifies the Client.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name of the Client.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The full name.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Status of Client
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Client Display Name (Code - Name)
        /// </summary>
        public string DisplayName { get; set; }


        #region MetaData

        public string LastUpdatedFinalDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        #endregion

        public string BindableName => string.IsNullOrEmpty(Name) ? FullName : Name;
    }

}