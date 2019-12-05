namespace BstConnectorViewer.Models
{
    public class PropertyInfoModel
    {
        public string Name { get; }
        public string? Value { get; set; }

        public PropertyInfoModel(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}