namespace PropertyTechCase.Event.Api.Model.EventCampaign;

public class PostEventCampaignModel
{
    public long CampaignCategoryId { get; set; }
    public string? Name { get; set; }
    public decimal Budget { get; set; }
    public decimal Spend { get; set; }
    public byte Type { get; set; }
    public byte Status { get; set; }
}