using HepsiBuradaAssignment.Application.Commands.CommandResult;
using HepsiBuradaAssignment.Application.Response;
using MediatR;
namespace HepsiBuradaAssignment.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreateOrderCommandResult>>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public int DifferenceTime { get; set; }

        public CreateOrderCommand()
        {

        }
        public CreateOrderCommand(string productCode,int quantity, int differenceTime)
        {
            ProductCode = productCode;
            Quantity = quantity;
            DifferenceTime = differenceTime;
        }
    }
}
