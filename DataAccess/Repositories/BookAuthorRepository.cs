using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Dao;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        public List<BookAuthor> GetBookAuthors() => BookAuthorDAO.Instance.GetBookAuthors();
        public BookAuthor GetBookAuthorById(int bookId, int authorId) => BookAuthorDAO.Instance.GetBookAuthorById(bookId, authorId);
        public void AddBookAuthor(BookAuthor bookAuthor) => BookAuthorDAO.Instance.AddBookAuthor(bookAuthor);
        public void UpdateBookAuthor(BookAuthor bookAuthor) => BookAuthorDAO.Instance.UpdateBookAuthor(bookAuthor);
        public void RemoveBookAuthor(int bookId, int authorId) => BookAuthorDAO.Instance.RemoveBookAuthor(bookId, authorId);
    }
}