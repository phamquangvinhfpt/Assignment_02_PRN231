using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Dao;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        public List<Publisher> GetPublishers() => PublisherDAO.Instance.GetPublishers();
        public Publisher GetPublisherById(int publisherId) => PublisherDAO.Instance.GetPublisherById(publisherId);
        public void AddPublisher(Publisher publisher) => PublisherDAO.Instance.AddPublisher(publisher);
        public void UpdatePublisher(Publisher publisher) => PublisherDAO.Instance.UpdatePublisher(publisher);
        public void RemovePublisher(int publisherId) => PublisherDAO.Instance.RemovePublisher(publisherId);
    }
}