using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            if (_context.Books.Any(x => x.Title == Model.Title)==false)
            {
                var book= _mapper.Map<Book>(Model); 
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