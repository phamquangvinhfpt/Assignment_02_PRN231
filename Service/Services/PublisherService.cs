using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository publisherRepository = new PublisherRepository();

        public List<Publisher> GetPublishers()
        {
            try
            {
                return publisherRepository.GetPublishers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PublisherService.GetPublishers", ex);
            }
        }

        public Publisher GetPublisherById(int publisherId)
        {
            try
            {
                return publisherRepository.GetPublisherById(publisherId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in PublisherService.GetPublisherById for ID {publisherId}", ex);
            }
        }

        public void AddPublisher(Publisher publisher)
        {
            try
            {
                publisherRepository.AddPublisher(publisher);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PublisherService.AddPublisher", ex);
            }
        }

        public void UpdatePublisher(Publisher publisher)
        {
            try
            {
                publisherRepository.UpdatePublisher(publisher);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in PublisherService.UpdatePublisher", ex);
            }
        }

        public void RemovePublisher(int publisherId)
        {
            try
            {
                publisherRepository.RemovePublisher(publisherId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in PublisherService.RemovePublisher for ID {publisherId}", ex);
            }
        }
    }
}