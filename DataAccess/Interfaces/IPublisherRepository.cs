using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetPublishers();
        Publisher GetPublisherById(int publisherId);
        void AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void RemovePublisher(int publisherId);
    }
}