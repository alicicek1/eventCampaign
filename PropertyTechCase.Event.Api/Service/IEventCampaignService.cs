using PropertyTechCase.Event.Api.Model.EventCampaign;
using PropertyTechCase.Event.Api.Model.Results;

namespace PropertyTechCase.Event.Api.Service;

public interface IEventCampaignService
{
    DataResult<string?> PublishEvent(EventCampaignEntity entity);
}