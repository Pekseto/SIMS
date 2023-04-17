using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Image() { }
        public Image(string url)
        {
            Url = url;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
        }
    }
}