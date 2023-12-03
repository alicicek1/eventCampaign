using Microsoft.AspNetCore.Mvc;
using PropertyTechCase.Event.Api.Attributes;
using PropertyTechCase.Event.Api.Mapper;
using PropertyTechCase.Event.Api.Model.EventCampaign;
using PropertyTechCase.Event.Api.Model.Results;
using PropertyTechCase.Event.Api.Service;

namespace PropertyTechCase.Event.Api.Controllers;

public class EventCampaignsController : BaseController
{
    private IEventCampaignService _eventCampaignService;

    public EventCampaignsController(IEventCampaignService eventCampaignService)
    {
        _eventCampaignService = eventCampaignService;
    }

    [HttpPost]
    [TokenRequired]
    public IActionResult Post(PostEventCampaignModel model)
    {
        return ApiResponse(_eventCampaignService.PublishEvent(Mapping.EventCampaignModelToEntity(model)));
    }
}