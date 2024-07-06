using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace Service.Interfaces
{
    public interface IBookAuthorService
    {
        List<BookAuthor> GetBookAuthors();
        BookAuthor GetBookAuthorById(int bookId, int authorId);
        void AddBookAuthor(BookAuthor bookAuthor);
        void UpdateBookAuthor(BookAuthor bookAuthor);
        void RemoveBookAuthor(int bookId, int authorId);
    }
}