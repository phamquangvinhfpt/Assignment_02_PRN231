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
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository authorRepository = new AuthorRepository();

        public List<Author> GetAuthors()
        {
            try
            {
                return authorRepository.GetAuthors();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthorService.GetAuthors", ex);
            }
        }

        public Author GetAuthorById(int authorId)
        {
            try
            {
                return authorRepository.GetAuthorById(authorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AuthorService.GetAuthorById for ID {authorId}", ex);
            }
        }

        public void AddAuthor(Author author)
        {
            try
            {
                authorRepository.AddAuthor(author);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthorService.AddAuthor", ex);
            }
        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                authorRepository.UpdateAuthor(author);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AuthorService.UpdateAuthor", ex);
            }
        }

        public void RemoveAuthor(int authorId)
        {
            try
            {
                authorRepository.RemoveAuthor(authorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AuthorService.RemoveAuthor for ID {authorId}", ex);
            }
        }
    }

}