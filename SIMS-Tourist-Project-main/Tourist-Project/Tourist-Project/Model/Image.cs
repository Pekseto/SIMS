using System;

namespace Tourist_Project.Model
{
    public class Image
    {
        private int id;
        public int Id
        {
            get => id;
            set => id = value;
        }
        private String url;
        public String Url
        {
            get => url;
            set => url = value;
        }


        public Image()
        {

        }
        public Image(int id, string url)
        {
            Id = id;
            this.Url = url;
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