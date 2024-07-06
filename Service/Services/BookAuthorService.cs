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
    public class BookAuthorService : IBookAuthorService
    {
        private IBookAuthorRepository bookAuthorRepository = new BookAuthorRepository();

        public List<BookAuthor> GetBookAuthors()
        {
            try
            {
                return bookAuthorRepository.GetBookAuthors();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BookAuthorService.GetBookAuthors", ex);
            }
        }

        public BookAuthor GetBookAuthorById(int bookId, int authorId)
        {
            try
            {
                return bookAuthorRepository.GetBookAuthorById(bookId, authorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BookAuthorService.GetBookAuthorById for Book ID {bookId} and Author ID {authorId}", ex);
            }
        }

        public void AddBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                bookAuthorRepository.AddBookAuthor(bookAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BookAuthorService.AddBookAuthor", ex);
            }
        }

        public void UpdateBookAuthor(BookAuthor bookAuthor)
        {
            try
            {
                bookAuthorRepository.UpdateBookAuthor(bookAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BookAuthorService.UpdateBookAuthor", ex);
            }
        }

        public void RemoveBookAuthor(int bookId, int authorId)
        {
            try
            {
                bookAuthorRepository.RemoveBookAuthor(bookId, authorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in BookAuthorService.RemoveBookAuthor for Book ID {bookId} and Author ID {authorId}", ex);
            }
        }
    }
}