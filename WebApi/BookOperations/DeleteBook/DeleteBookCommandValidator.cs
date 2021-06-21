namespace WebApi.BookOperations.DeleteBook
{
    using System;
    using FluentValidation;

    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {

        public DeleteBookCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);
        }

    }
}