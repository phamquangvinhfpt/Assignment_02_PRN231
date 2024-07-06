using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao
{
    public class AuthorDAO
    {
        private static AuthorDAO instance = null;
        private static readonly object instanceLock = new object();

        private AuthorDAO() { }

        public static AuthorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new AuthorDAO();
                    }
                }
                return instance;
            }
        }

        public List<Author> GetAuthors()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Authors.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting authors", ex);
            }
        }

        public Author GetAuthorById(int authorId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Authors.Find(authorId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting author with ID {authorId}", ex);
            }
        }

        public void AddAuthor(Author author)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding author", ex);
            }
        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Entry(author).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(author).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating author", ex);
            }
        }

        public void RemoveAuthor(int authorId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    var author = context.Authors.Find(authorId);
                    if (author != null)
                    {
                        context.Authors.Remove(author);
                        context.SaveChanges();
                    } else {
                        throw new Exception($"Author with ID {authorId} not found");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing author with ID {authorId}", ex);
            }
        }
    }
}
