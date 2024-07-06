using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao
{
    public class PublisherDAO
    {
        private static PublisherDAO instance = null;
        private static readonly object instanceLock = new object();

        private PublisherDAO() { }

        public static PublisherDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new PublisherDAO();
                    }
                }
                return instance;
            }
        }

        public List<Publisher> GetPublishers()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Publishers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting publishers", ex);
            }
        }

        public Publisher GetPublisherById(int publisherId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Publishers.Find(publisherId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting publisher with ID {publisherId}", ex);
            }
        }

        public void AddPublisher(Publisher publisher)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Publishers.Add(publisher);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding publisher", ex);
            }
        }

        public void UpdatePublisher(Publisher publisher)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Entry(publisher).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(publisher).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating publisher", ex);
            }
        }

        public void RemovePublisher(int publisherId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    var publisher = context.Publishers.Find(publisherId);
                    if (publisher != null)
                    {
                        context.Publishers.Remove(publisher);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing publisher with ID {publisherId}", ex);
            }
        }
    }
}
