using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetBooks() => BookDAO.Instance.GetBooks();
        public Book GetBookById(int id) => BookDAO.Instance.GetBookById(id);
        public void AddBook(Book book) => BookDAO.Instance.AddBook(book);
        public void UpdateBook(Book book) => BookDAO.Instance.UpdateBook(book);
        public void RemoveBook(Book book) => BookDAO.Instance.RemoveBook(book);
    }
}