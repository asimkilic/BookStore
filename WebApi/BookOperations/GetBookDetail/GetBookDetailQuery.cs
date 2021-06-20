using System;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _context;
        private IMapper _mapper;
        public int BookId{get;set;}
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
             var book= _context.Books.FirstOrDefault(x => x.Id == BookId);
        
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookDetailViewModel bookDetailViewModel=_mapper.Map<BookDetailViewModel>(book);
            return bookDetailViewModel;
        }
    }
    public class BookDetailViewModel{
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }

}