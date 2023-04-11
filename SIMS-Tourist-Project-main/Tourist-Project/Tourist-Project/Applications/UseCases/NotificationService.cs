using System.Collections.Generic;
using MyNamespace;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{

    public class NotificationService: IService<Notification>
    {

        private static readonly Injector injector = new();

        private readonly INotificationRepository notificationRepository =
            injector.CreateInstance<INotificationRepository>();
        public NotificationService()
        {
        }

        public Notification Create(Notification notification)
        {
            return notificationRepository.Save(notification);
        }

        public List<Notification> GetAll()
        {
            return notificationRepository.GetAll();
        }

        public List<Notification> GetAllByType(string type)
        {
            return notificationRepository.GetAllByType(type);
        }

        public Notification Get(int id)
        {
            return notificationRepository.GetById(id);
        }

        public Notification Update(Notification notification)
        {
            return notificationRepository.Update(notification);
        }

        public void Delete(int id)
        {
            notificationRepository.Delete(id);
        }
    }

}