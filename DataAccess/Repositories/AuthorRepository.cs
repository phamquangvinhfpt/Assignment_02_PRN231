using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Dao;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public List<Author> GetAuthors() => AuthorDAO.Instance.GetAuthors();
        public Author GetAuthorById(int authorId) => AuthorDAO.Instance.GetAuthorById(authorId);
        public void AddAuthor(Author author) => AuthorDAO.Instance.AddAuthor(author);
        public void UpdateAuthor(Author author) => AuthorDAO.Instance.UpdateAuthor(author);
        public void RemoveAuthor(int authorId) => AuthorDAO.Instance.RemoveAuthor(authorId);
    }
}