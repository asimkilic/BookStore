using System;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {

        private readonly BookStoreDbContext _context;
        public int BookId{get;set;}
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public BookDetailViewModel Handle()
        {
            BookDetailViewModel bookDetailViewModel=new BookDetailViewModel();
           
            var book= _context.Books.FirstOrDefault(x => x.Id == BookId);
            if(book is null){
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            bookDetailViewModel.Title=book.Title;
            bookDetailViewModel.PageCount=book.PageCount;
            bookDetailViewModel.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
            bookDetailViewModel.Genre=((GenreEnum)book.GenreId).ToString();
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