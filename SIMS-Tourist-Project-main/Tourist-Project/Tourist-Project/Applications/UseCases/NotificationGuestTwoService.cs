using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist_Project.Domain.Models;
using Tourist_Project.Domain.RepositoryInterfaces;

namespace Tourist_Project.Applications.UseCases
{
    public class NotificationGuestTwoService
    {
        private static readonly Injector injector = new();
        
        private readonly INotificationGuestTwoRepository notificationRepository = injector.CreateInstance<INotificationGuestTwoRepository>();

        public NotificationGuestTwoService()
        {
            
        }

        public List<NotificationGuestTwo> GetAll()
        {
            return notificationRepository.GetAll();
        }
        public NotificationGuestTwo Save(NotificationGuestTwo notification)
        {
            return notificationRepository.Save(notification);
        }
        public NotificationGuestTwo Update(NotificationGuestTwo notification)
        {
            return notificationRepository.Update(notification);
        }
        public void Delete(int id)
        {
            notificationRepository.Delete(id);
        }

        public List<NotificationGuestTwo> GetAllForUser(int userId)
        {
            return notificationRepository.GetAllForUser(userId);
        }
    }
}
