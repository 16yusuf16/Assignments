using System;
using System.Linq;
using HepsiBuradaAssignment.Domain.Entities;
using HepsiBuradaAssignment.Domain.Interfaces;
using HepsiBuradaAssignment.Infrastructure.Data.Context;

namespace HepsiBuradaAssignment.Infrastructure.Repositories
{
    public class CampaignRepository : RepositoryBase<Campaign>, ICampaignRepository
    {
        private AssignmentContext _context;
        public CampaignRepository(AssignmentContext context) : base(context)
        {
            _context = context;
        }

        public Campaign GetCampaignInfoByName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : _context.Campaigns.FirstOrDefault(x =>x.IsActive && x.Name.Equals(name));
        }

        public Campaign GetCampaignByProductId(int id,DateTime systemDate)
        {
            return id > 0 ? _context.Campaigns.FirstOrDefault(x => x.IsActive && x.StartDate< systemDate && x.EndDate>systemDate
            && x.ProductId == id)  : null;
        }

        public Campaign GetCampaignByProductCode(string productCode,DateTime systemDate)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return null;
            var product = _context.Products.FirstOrDefault(x => x.Code == productCode);

            if (product is null)
                return null;
            var campaign = _context.Campaigns.FirstOrDefault(x => x.IsActive && x.ProductId == product.Id && x.StartDate<systemDate&& x.EndDate>systemDate);

            return campaign;

        }

    }
}
