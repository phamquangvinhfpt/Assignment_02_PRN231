using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao
{
    public class BookAuthorDAO
    {
        private static BookAuthorDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly EBookStoreContext context;

        private BookAuthorDAO()
        {
            context = new EBookStoreContext();
        }

        public static BookAuthorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new BookAuthorDAO();
                    }
                }
                return instance;
            }
        }

        public List<BookAuthor> GetBookAuthors()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.BookAuthors.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting book authors", ex);
            }
        }

        public BookAuthor GetBookAuthorById(int bookId, int authorId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.BookAuthors.Find(bookId, authorId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting book author with Book ID {bookId} and Author ID {authorId}", ex);
            }
        }

        public void AddBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.BookAuthors.Add(bookAuthor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding book author", ex);
            }
        }

        public void UpdateBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Entry(bookAuthor).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(bookAuthor).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating book author", ex);
            }
        }

        public void RemoveBookAuthor(int bookId, int authorId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    var bookAuthor = context.BookAuthors.Find(bookId, authorId);
                    if (bookAuthor == null)
                    {
                        throw new Exception($"Book author with Book ID {bookId} and Author ID {authorId} not found");
                    }
                    context.BookAuthors.Remove(bookAuthor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing book author with Book ID {bookId} and Author ID {authorId}", ex);
            }
        }
    }
}