using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void RemoveAuthor(int authorId);
    }
}