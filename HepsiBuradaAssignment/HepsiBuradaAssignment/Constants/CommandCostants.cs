using System;
namespace HepsiBuradaAssignment.Api.Constants
{
    public static class CommandCostants
    {
        public static string CreateProduct = "create_product";
        public static string GetProductInfo = "get_product_info";
        public static string CreateOrder = "create_order";
        public static string CreateCampaign = "create_campaign";
        public static string GetCampaignInfo = "get_campaign_info";
        public static string IncreaseTime = "increase_time";
        public static string CommandNotFound(string command) => $"Command not Found =>>{command}";
        public static string CommandParameterNotFound(string command, string parameterName) => $"Command Parameter Not Found {command}, parameter{parameterName}";

    }
}
