using PropertyTechCase.Api.Model.EventCampaign;
using PropertyTechCase.Api.Model.Results;

namespace PropertyTechCase.Api.Service;

public interface IEventCampaignService
{
    DataResult<string> PostEventCampaign(EventCampaignEntity entity);
    DataResult<GetEventCampaignModel> GetById(string id);
}