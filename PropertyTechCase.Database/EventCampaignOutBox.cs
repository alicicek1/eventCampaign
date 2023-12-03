namespace PropertyTechCase.Database;

public class EventCampaignOutBox
{
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public string @Type { get; set; }
    public string Payload { get; set; }
    public Guid IdempotentToken { get; set; }
}