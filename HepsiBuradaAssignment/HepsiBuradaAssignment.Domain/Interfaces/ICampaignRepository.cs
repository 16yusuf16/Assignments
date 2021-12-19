using System;
using HepsiBuradaAssignment.Domain.Entities;

namespace HepsiBuradaAssignment.Domain.Interfaces
{
    public interface ICampaignRepository :IRepositoryBase<Campaign>
    {
        Campaign GetCampaignInfoByName(string name);
        Campaign GetCampaignByProductId(int id, DateTime systemDate);
        Campaign GetCampaignByProductCode(string productCode, DateTime systemDate);
    }
}
