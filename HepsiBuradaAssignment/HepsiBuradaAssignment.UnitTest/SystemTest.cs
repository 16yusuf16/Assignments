using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HepsiBuradaAssignment.Application.Commands;
using HepsiBuradaAssignment.Application.Commands.Handler;
using HepsiBuradaAssignment.Application.Queries;
using HepsiBuradaAssignment.Application.Response;
using HepsiBuradaAssignment.Domain.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace HepsiBuradaAssignment.UnitTest
{
    public class SystemTest
    {
        public Mock<IProductQueries> _productQueriesMock { get; set; }
        public Mock<ICampaignQueries> _campaignQueriesMock { get; set; }
        public Mock<IProductRepository> _productRepositoryMock { get; set; }
        public Mock<ICampaignRepository> _campaignRepositoryMock { get; set; }
        public Mock<IOrderRepository> _orderRepositoryMock { get; set; }
        public Mock<IMapper> _mapperMock { get; set; }
        public Mock<IMediator > _mediatorMock { get; set; }


       public SystemTest()
        {
            _productQueriesMock = new Mock<IProductQueries>();
            _campaignQueriesMock = new Mock<ICampaignQueries>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _campaignRepositoryMock = new Mock<ICampaignRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
        }

        [Fact]
        public void GetProductInfoByCode_Success()
        {
            #region fill data
            var productCode = "P12";
            #endregion

            #region Act
            var productQueries = new ProductQueries(_productRepositoryMock.Object, _mapperMock.Object);
            var response = productQueries.GetProductInfoByCode(productCode);
            #endregion


            #region Assert
            Assert.True(response.Succeeded);
            #endregion

        }

        [Fact]
        public void GetProductInfoByCode_NotFoundProduct_Error()
        {
            #region fill data
            var productCode = "Z1";
            #endregion

            #region Act
            var productQueries = new ProductQueries(_productRepositoryMock.Object, _mapperMock.Object);
            var response = productQueries.GetProductInfoByCode(productCode);
            #endregion


            #region Assert
            Assert.False(response.Succeeded);
            Assert.Equal(ResponseMessage.Error.NotFoundProduct, response.Message);
            #endregion

        }


        [Fact]
        public async Task CreateProduct_SuccessAsync()
        {
            #region fill data
            var code = "P1";
            var price = 50;
            var stock = 100;
            #endregion
            #region Act
            var command = new Application.Commands.CreateProductCommand
            {
                Code = code,
                Price = price,
                Stock = stock
            };
            var createCommandProduct = new CreateProductCommandHandler(_productRepositoryMock.Object);
            var response = await createCommandProduct.Handle(command,  CancellationToken.None);
            #endregion

            #region Assert
            Assert.True(response.Succeeded);
            #endregion
        }

        [Fact]
        public async Task CreateProduct_NotNullCode_ErrorAsync()
        {
            #region fill data
            var code = "";
            var price = 50;
            var stock = 100;
            #endregion
            #region Act
            var command = new CreateProductCommand
            {
                Code = code,
                Price = price,
                Stock = stock
            };
            var createCommandProduct = new CreateProductCommandHandler(_productRepositoryMock.Object);
            var response = await createCommandProduct.Handle(command, CancellationToken.None);
            #endregion

            #region Assert
            Assert.False(response.Succeeded);
            Assert.Equal(ResponseMessage.Error.ProductCouldntCreated, response.Message);
            #endregion
        }

        [Fact]
        public async Task CreateCampaign_SuccessAsync()
        {
            var name = "C2";
            var productCode ="P4";
            int limit = 20;
            int duration = 10;
            int targetSaleCount = 1000;

            var command = new CreateCampaignCommand
            {
                Name = name,
                ProductCode = productCode,
                TargetSaleCount = targetSaleCount,
                Limit = limit,
                Duration = duration,

            };

            var createCommandCampaign = new CreateCampaignCommandHandler(_campaignRepositoryMock.Object, _productQueriesMock.Object);
            var response = await createCommandCampaign.Handle(command, CancellationToken.None);


            #region Assert
            Assert.True(response.Succeeded);
            #endregion
        }

        [Fact]
        public async Task CreateCampaign_NotFoundProduct_ErrorAsync()
        {
            var name = "C2";
            var productCode = "z1";
            int limit = 20;
            int duration = 10;
            int targetSaleCount = 1000;

            var command = new CreateCampaignCommand
            {
                Name = name,
                ProductCode = productCode,
                TargetSaleCount = targetSaleCount,
                Limit = limit,
                Duration = duration,

            };

          var createCommandCampaign= new CreateCampaignCommandHandler(_campaignRepositoryMock.Object, _productQueriesMock.Object);
          
           var response = await createCommandCampaign.Handle(command, CancellationToken.None);

            #region Assert
            Assert.False(response.Succeeded);
            Assert.Equal(ResponseMessage.Error.NotFoundProduct, response.Message);
            #endregion
        }

        [Fact]
        public async Task CreateOrder_Success()
        {
            string productCode ="P11";
            int quantity = 3;
            int differenceTime = 3;

            var command = new CreateOrderCommand
            {
                ProductCode = productCode,
                Quantity = quantity,
                DifferenceTime = differenceTime,
            };

            var createCommandCampaign = new CreateOrderCommandHandler(_orderRepositoryMock.Object,_productRepositoryMock.Object,_campaignRepositoryMock.Object);
         
            var response = await createCommandCampaign.Handle(command, CancellationToken.None);


            #region Assert
            Assert.True(response.Succeeded);
            #endregion
        }

        [Fact]
        public async Task CreateOrder_NotFoundProduct_Error()
        {
            string productCode = "p8";
            int quantity = 3;
            int differenceTime = 3;

            var command = new CreateOrderCommand
            {
                ProductCode = productCode,
                Quantity = quantity,
                DifferenceTime = differenceTime,
            };

            var createCommandCampaign = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _productRepositoryMock.Object, _campaignRepositoryMock.Object);
            var response = await createCommandCampaign.Handle(command, CancellationToken.None);

            #region Assert
            Assert.False(response.Succeeded);
            Assert.Equal(ResponseMessage.Error.NotFoundProduct, response.Message);
            #endregion
        }
    }
}
