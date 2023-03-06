using System;

namespace Tourist_Project.Model
{
    public class Image
    {
        int Id { get; set; }
        String url { get; set; }
        int entityId { get; set; }

        public Image(int id, string url, int entityId)
        {
            Id = id;
            this.url = url;
            this.entityId = entityId;
        }
    }
}