using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class BookDAO
    {
        private static BookDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly EBookStoreContext _context;

        private BookDAO()
        {
            _context = new EBookStoreContext();
        }

        public static BookDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Books.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving books", ex);
            }
        }

        public Book GetBookById(int id)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    var book = _context.Books.Find(id);
                    if (book == null)
                    {
                        throw new KeyNotFoundException($"Book with ID {id} not found.");
                    }
                    return book;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving book with ID {id}", ex);
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding new book", ex);
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Entry(book).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(book).State = EntityState.Detached;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
                {
                    throw new KeyNotFoundException($"Book with ID {book.BookId} not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating book with ID {book.BookId}", ex);
            }
        }

        public void RemoveBook(Book book)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Books.Remove(book);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing book with ID {book.BookId}", ex);
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}