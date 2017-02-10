using CustomersApp.Models;
using FluentValidation;

namespace CustomersApp.Configurations
{
    public class CustomerValidator: AbstractValidator<Customer>
    {
        public CustomerValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(2, 100)
                .WithName("Имя клиента")
                .WithMessage("Имя клиента должно быть не менее 2 символов и не более 100"); 
        
            RuleFor(x => x.Payment)
                .NotEmpty()
                .InclusiveBetween(0, int.MaxValue)
                .WithName("Сумма платежей")
                .WithMessage("Сумма платежей должна быть целым неотрицательным числом");

        }
    }
}