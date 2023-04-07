using Tourist_Project.Serializer;

namespace Tourist_Project.Domain.Models
{

    public class Location : ISerializable

    {
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private string city;
        public string City
        {
            get => city;
            set => city = value;
        }
        private string country;
        public string Country
        {
            get => country;
            set => country = value;
        }

        public Location()
        {
        }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                City,
                Country
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            City = values[1];
            Country = values[2];
        }
        public override string ToString()
        {
            return City + ", " + Country;
        }
    }
}