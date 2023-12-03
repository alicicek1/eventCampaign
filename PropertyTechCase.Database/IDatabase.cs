namespace PropertyTechCase.Database;

public interface IDatabase
{
    string AddToEventCampaign(EventCampaignEntity? entity);
    EventCampaignEntity? GetById(string id);
    void AddToEventCampaignOutbox(EventCampaignOutBox entity);
    IEnumerable<EventCampaignOutBox> GetCampaignEventOutboxData();
}