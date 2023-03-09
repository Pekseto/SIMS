using System;

namespace Tourist_Project.Model
{
    public class Image
    {
        public int Id { get; set; }
        string url { get; set; }
        int entityId { get; set; }

        public Image() { }
        public Image(int id, string url, int entityId)
        {
            Id = id;
            this.url = url;
            this.entityId = entityId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), url, entityId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            url = values[1];
            entityId = Convert.ToInt32(values[2]);
        }
    }
}