using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.Getbooks
{
    public class GetBooksQuery
    {

        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            //return _context.Books.OrderBy(x => x.PublishDate).ToList();
            var bookList=_context.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> booksViewModels =_mapper.Map<List<BooksViewModel>>(bookList);
            // new List<BooksViewModel>();
            // foreach (var book in _context.Books.OrderBy(x => x.PublishDate).ToList())
            // {
            //     booksViewModels.Add(new BooksViewModel(){
            //         Title=book.Title,
            //         Genre=((GenreEnum)book.GenreId).ToString(),
            //         PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy"),
            //         PageCount=book.PageCount
            //     });
            // }
            return booksViewModels;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Genre { get; set; }

        }
    }
}