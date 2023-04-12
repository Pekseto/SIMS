using System.Collections.Generic;
using Tourist_Project.Domain.Models;

namespace MyNamespace
{
    public interface INotificationRepository
    {
        public List<Notification> GetAll();
        public int NextId();
        public List<Notification> GetAllByType(string type);
        public Notification GetById(int id);
        public Notification Save(Notification notification);
        public Notification Update(Notification notification);
        public void Delete(int id);
    }
}