using System.Collections;

namespace PropertyTechCase.Database;

public class Database : IDatabase
{
    private Dictionary<string, EventCampaignEntity?> EventCampaignEntitiesTable { get; } = new();
    private readonly IList<EventCampaignOutBox> _eventCampaignOutBoxTable = new List<EventCampaignOutBox>();


    public string AddToEventCampaign(EventCampaignEntity? entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.CreatedAt = DateTime.Now;
        EventCampaignEntitiesTable.Add(entity.Id, entity);
        return entity.Id;
    }

    public EventCampaignEntity? GetById(string id)
    {
        EventCampaignEntity? res;
        EventCampaignEntitiesTable.TryGetValue(id, out res);

        return res;
    }

    public void AddToEventCampaignOutbox(EventCampaignOutBox entity)
    {
        _eventCampaignOutBoxTable.Add(entity);
    }

    public IEnumerable<EventCampaignOutBox> GetCampaignEventOutboxData()
    {
        return _eventCampaignOutBoxTable;
    }
}