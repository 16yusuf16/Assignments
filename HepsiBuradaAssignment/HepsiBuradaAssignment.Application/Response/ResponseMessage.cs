namespace HepsiBuradaAssignment.Application.Response
{
    public class ResponseMessage
    {
        public struct Success
        {
            public static string CreateProduct(string code, decimal price, int stock)
                => $"Product created; code {code}, price {price}, stock {stock}";

            public static string GetProductByCode(string code, decimal price, int stock)
                => $"Product {code} info; price {price}, stock {stock}";

            public static string GetCampaignInfoByName(string name, string status, int targetSales, int totalSales, int turnover, decimal price)
                =>$"Campaign {name} info; Status {status}, Target Sales {targetSales}, Total Sales {totalSales}, Turnover {turnover}, Average Item Price {price}";

            public static string CreateCampaign(string name, string productCode, int duration, int limit, int targetSalesCount)
                => $" Campaign created; name {name}, product {productCode}, duration {duration}, limit {limit}, target sales count {targetSalesCount}";

            public static string CreateOrder(string productCode, int quantity)
                => $" Order created; product {productCode}, quantity {quantity}";


        }
        public struct Error
        {
            public static string ProductCouldntCreated = "Product could not be created";
            public static string CampaignCouldntCreated = "Campaign could not be created";
            public static string OrderouldntCreated = "Order could not be created";
            public static string NotFoundProduct = "Product not found";
            public static string NotFoundCampaign = "Campaign not Found";
            public static string InsufficientProduct = "Insufficient Product";
            public static string ProductStockInsufficientRequestedQuantity = "Product stock insufficient requested quantity";
            public static string CampaignLimitError(int limit,int quantity) => $"You cannot request more than the campaign limit.Campaign Limit {limit} ,YOur Request {quantity}";
        }
    }
}
