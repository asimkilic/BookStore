using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.Getbooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {

            //  return _context.Books.OrderBy(x => x.PublishDate).ToList();
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            return Ok(query.Handle());
        }
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                query.Handle();
                return Ok(query.Handle());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getbook")]
        public Book GetBook([FromQuery] int id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            try
            {
                CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
                createBookCommand.Model = newBook;

                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(createBookCommand);
                
                // if (!result.IsValid)
                // {
                //     foreach (var item in result.Errors)
                //     {
                //         System.Console.WriteLine("Özellik " + item.PropertyName + " - Error message: " + item.ErrorMessage);
                //     }
                //     return BadRequest(result);
                // }
                // else
                // {
                //     createBookCommand.Handle();
                //     return Ok();
                // }


                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
                updateBookCommand.BookId = id;
                updateBookCommand.Model = updatedBook;
                updateBookCommand.Handle();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
                deleteBookCommand.BookId = id;
                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(deleteBookCommand);
                
                deleteBookCommand.Handle();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
    }


}