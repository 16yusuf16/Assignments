using HepsiBuradaAssignment.Application.Commands.CommandResult;
using HepsiBuradaAssignment.Application.Response;
using MediatR;

namespace HepsiBuradaAssignment.Application.Commands
{
    public class CreateProductCommand:IRequest<Response<CreateProductCommandResult>>
    {
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CreateProductCommand()
        {

        }

        public CreateProductCommand(string code,decimal price, int stock)
        {
            Code = code;
            Price = price;
            Stock = stock;
        }
    }
}
