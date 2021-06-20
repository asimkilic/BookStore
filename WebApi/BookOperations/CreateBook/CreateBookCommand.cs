using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            if (_context.Books.Any(x => x.Title == Model.Title)==false)
            {
                Book book= new Book();
                book.Title=Model.Title;
                book.PublishDate=Model.PublishDate;
                book.PageCount=Model.PageCount;
                book.GenreId=Model.GenreId;
                _context.Books.Add(book);
                _context.SaveChanges();
                return;
            }
            throw new InvalidOperationException("Kitap zaten mevcut");
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }

}