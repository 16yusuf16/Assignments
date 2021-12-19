using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HepsiBuradaAssignment.Api.Constants;
using HepsiBuradaAssignment.Application.Commands;
using HepsiBuradaAssignment.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HepsiBuradaAssignment.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssignmentController :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductQueries _productQueries;
        private readonly ICampaignQueries _campaignQueries;
        public AssignmentController(IMediator mediator, IProductQueries productQueries, ICampaignQueries campaignQueries)
        {
            _mediator = mediator;
            _productQueries = productQueries;
            _campaignQueries = campaignQueries;
        }


        [HttpPost]
        [Route("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file )
        {
            var newLine = Environment.NewLine;
            var message = string.Empty;
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(reader.ReadLine());
            }
            var date = DateTime.Now.Date;// günün  system date 00:00 saati
            int increasedSytemDateCount = 1;
            foreach (var item in result)
            {
                
                var commandAndParameterArr = item.Split(" ");
                switch (commandAndParameterArr[0])
                {
                    case "create_product":
                            if (commandAndParameterArr.Length != 4)
                            {
                                message += newLine;
                                message += CommandCostants.CommandParameterNotFound("create_product", "PRODUCTCODE PRICE STOCK");
                            break;
                            }
                            var command = new CreateProductCommand
                                {
                                     Code = commandAndParameterArr[1],
                                     Price= Convert.ToDecimal(commandAndParameterArr[2]),
                                     Stock = Convert.ToInt16(commandAndParameterArr[3])
                                };
                           var commandResult = await  _mediator.Send(command);
                            message += newLine;
                            message += commandResult.Message;
                            
                             break;
                    case "get_product_info":
                            if (commandAndParameterArr.Length != 2)
                            {
                                message += newLine;
                                message += CommandCostants.CommandParameterNotFound("get_product_info", "PRODUCTCODE");
                                break;
                            }
                            var productInfo = _productQueries.GetProductInfoByCode(commandAndParameterArr[1]);
                            message += newLine;
                            message += productInfo.Message;
                           
                            break;
                    case "create_order":
                            if (commandAndParameterArr.Length != 3)
                            {
                                message += newLine;
                                message += CommandCostants.CommandParameterNotFound("create_order", "PRODUCTCODE QUANTITY");
                                break;
                            }
                            var orderCommand = new CreateOrderCommand
                            {
                                ProductCode = commandAndParameterArr[1],
                                Quantity = Convert.ToInt32(commandAndParameterArr[2]),
                                DifferenceTime =increasedSytemDateCount,
                            };
                            var orderCommandResult = await _mediator.Send(orderCommand);
                            message += newLine;
                            message += orderCommandResult.Message;
                           
                            break;
                    case "create_campaign":
                            if (commandAndParameterArr.Length != 6)
                            {
                                message += newLine;
                                message += CommandCostants.CommandParameterNotFound("create_campaign", "NAME PRODUCTCODE DURATION PMLIMIT TARGETSALESCOUNT");
                                break;
                            }
                            var campaignCommand = new CreateCampaignCommand
                            {
                                Name = commandAndParameterArr[1],
                                ProductCode = commandAndParameterArr[2],
                                Duration = Convert.ToInt32(commandAndParameterArr[3]),
                                Limit = Convert.ToInt32(commandAndParameterArr[4]),
                                TargetSaleCount = Convert.ToInt32(commandAndParameterArr[5]),
                            };
                            var campaignCommandResult = await _mediator.Send(campaignCommand);
                            message += newLine;
                            message += campaignCommandResult.Message;
                            break;
                    case "get_campaign_info":
                        if (commandAndParameterArr.Length != 2)
                        {
                            message += newLine;
                            message += CommandCostants.CommandParameterNotFound("get_campaign_info", "NAME");
                            break;
                        }
                        var campaignInfo = _campaignQueries.GetCampaignInfoByName(commandAndParameterArr[1],DateTime.Now.Date.AddHours(increasedSytemDateCount));
                        message += newLine;
                        message += campaignInfo.Message;
                        break;
                    case "increase_time":
                        if (commandAndParameterArr.Length != 2)
                        {
                            message += newLine;
                            message += CommandCostants.CommandParameterNotFound("increase_time", "HOUR");
                            break;
                        }
                        date = date.AddHours(Convert.ToInt16(commandAndParameterArr[1]));
                        increasedSytemDateCount++;
                        message += newLine;
                        message += "Time is :" + date.Hour;
                        break;
                      default:
                        break;
                }
               
            }
            return Ok(message);
        }
    }
}
