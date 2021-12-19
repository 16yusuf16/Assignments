using HepsiBuradaAssignment.Application.Commands.CommandResult;
using HepsiBuradaAssignment.Application.Response;
using MediatR;

namespace HepsiBuradaAssignment.Application.Commands
{
    public class CreateCampaignCommand : IRequest<Response<CreateCampaignCommandResult>>
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Limit { get; set; }
        public int Duration { get; set; }
        public int TargetSaleCount { get; set; }
     

        public CreateCampaignCommand()
        {

        }

        public CreateCampaignCommand(string name, string productCode, int limit,
            int duration, int targetSaleCount)
        {
            Name = name;
            ProductCode = productCode;
            Limit = limit;
            Duration = duration;
            TargetSaleCount = targetSaleCount;
        }

    }
}
