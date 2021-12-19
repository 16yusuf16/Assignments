using System;
using AutoMapper;
using HepsiBuradaAssignment.Application.Models;
using HepsiBuradaAssignment.Application.Response;
using HepsiBuradaAssignment.Domain.Interfaces;

namespace HepsiBuradaAssignment.Application.Queries
{
    public interface ICampaignQueries
    {
        Response<CampaignDtoModel> GetCampaignInfoByName(string name,DateTime systemDate);
        Response<CampaignDtoModel> GetProductCampaign(string productCode, DateTime systemDate);
    }
    public class CampaignQueries :ICampaignQueries
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductQueries _productQueries;
        private readonly IMapper _mapper;

        public CampaignQueries(ICampaignRepository campaignRepository, IMapper mapper, IProductQueries productQueries)
        {
            _campaignRepository = campaignRepository;
            _productQueries = productQueries;
            _mapper = mapper;
        }

        public Response<CampaignDtoModel> GetCampaignInfoByName(string name, DateTime systemDate)
        {
            var campaign = _campaignRepository.GetCampaignInfoByName(name);
            if (campaign is null)
                return Response<CampaignDtoModel>.Fail(ResponseMessage.Error.NotFoundCampaign);

            var result = _mapper.Map<CampaignDtoModel>(campaign);
            return Response<CampaignDtoModel>.Success(result,
                ResponseMessage.Success.GetCampaignInfoByName(campaign.Name, (campaign.StartDate < systemDate && campaign.EndDate > systemDate) ? "Active" : "Ended", campaign.TargetSaleCount,
                                    campaign.TotalSaleCount,(int)(campaign.TotalSaleCount*campaign.TargetSaleCount), campaign.Price));
        }

        public Response<CampaignDtoModel> GetProductCampaign(string productCode, DateTime systemDate)
        {
            var productResult = _productQueries.GetProductInfoByCode(productCode);

            if(!productResult.Succeeded)
                return Response<CampaignDtoModel>.Fail(ResponseMessage.Error.NotFoundCampaign);

            var campaign = _campaignRepository.GetCampaignByProductId(productResult.Data.Id,systemDate);
            if(campaign is null)
                return Response<CampaignDtoModel>.Fail(ResponseMessage.Error.NotFoundCampaign);

            var result = _mapper.Map<CampaignDtoModel>(campaign);

            return Response<CampaignDtoModel>.Success(result,
               ResponseMessage.Success.GetCampaignInfoByName
               (campaign.Name, (campaign.StartDate<systemDate && campaign.EndDate>systemDate) ? "Active" : "Ended", campaign.TargetSaleCount,
                                   campaign.TotalSaleCount, (int)(campaign.TotalSaleCount * campaign.TargetSaleCount), campaign.Price));
        }
    }
}
