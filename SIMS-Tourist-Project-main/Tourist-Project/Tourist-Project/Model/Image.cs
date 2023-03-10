using System;

namespace Tourist_Project.Model
{
    public class Image
    {
        public int Id { get; set; }
        string url { get; set; }

        public Image() { }
        public Image(string url)
        {
            this.url = url;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), url};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            url = values[1];
        }
    }
}