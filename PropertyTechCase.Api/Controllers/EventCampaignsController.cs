using Microsoft.AspNetCore.Mvc;
using PropertyTechCase.Api.Attributes;
using PropertyTechCase.Api.Mapper;
using PropertyTechCase.Api.Model.EventCampaign;
using PropertyTechCase.Api.Service;

namespace PropertyTechCase.Api.Controllers;

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
        return ApiResponse(_eventCampaignService.PostEventCampaign(Mapping.EventCampaignModelToEntity(model)));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        return ApiResponse(_eventCampaignService.GetById(id));
    }
}