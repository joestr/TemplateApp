using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TemplateApp.Core.Entities;
using TemplateApp.Data.Contexts;

namespace TemplateApp.Core.Providers.Data
{
    public class AuthorDataProvider
    {
        public DatabaseContext DatabaseContext { get; set; }
        public ReadOnlyDatabaseContext ReadOnlyDatabaseContext { get; set; }

        public AuthorDataProvider(DatabaseContext databaseContext, ReadOnlyDatabaseContext readOnlyDatabaseContext)
        {
            DatabaseContext = databaseContext;
            ReadOnlyDatabaseContext = readOnlyDatabaseContext;
        }

        private IQueryable<TemplateApp.Data.Entities.Author> QueryAuthors()
        {
            return ReadOnlyDatabaseContext.Authors;
        }

        public List<Author> GetAuthors()
        {
            var dbAuthors = QueryAuthors();

            var result = dbAuthors.Select(dbAuthor => new Author(dbAuthor)).ToList();

            return result;
        }

        public List<Author> FindAuthors(string searchTerm)
        {
            var dbAuthors = QueryAuthors()
                .Where(dbAuthor =>
                    dbAuthor.FirstName.Contains(searchTerm) || dbAuthor.LastName.Contains(searchTerm));

            var result = dbAuthors.Select(dbAuthor => new Author(dbAuthor)).ToList();

            return result;
        }
    }
}
