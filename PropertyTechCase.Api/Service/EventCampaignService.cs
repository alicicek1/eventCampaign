using Microsoft.Extensions.Caching.Memory;
using PropertyTechCase.Api.Model.EventCampaign;
using PropertyTechCase.Api.Model.Results;
using PropertyTechCase.Database;
using EventCampaignEntity = PropertyTechCase.Api.Model.EventCampaign.EventCampaignEntity;

namespace PropertyTechCase.Api.Service;

public class EventCampaignService : IEventCampaignService
{
    private readonly IDatabase _database;
    private readonly IMemoryCache _memoryCache;


    public EventCampaignService(IDatabase database, IMemoryCache memoryCache)
    {
        _database = database;
        _memoryCache = memoryCache;
    }

    public DataResult<string> PostEventCampaign(EventCampaignEntity entity)
    {
        string id = _database.AddToEventCampaign(new PropertyTechCase.Database.EventCampaignEntity
        {
            CampaignCategoryId = entity.CampaignCategoryId,
            Name = entity.Name,
            Budget = entity.Budget,
            Spend = entity.Spend,
            Type = entity.Type,
            Status = entity.Status
        });

        Task.Run(() => _memoryCache.Set(id, entity));

        return new SuccessDataResult<string>(id, "success");
    }

    public DataResult<GetEventCampaignModel> GetById(string id)
    {
        EventCampaignEntity cacheVal = _memoryCache.Get<EventCampaignEntity>(id);
        if (cacheVal != null)
        {
            return new SuccessDataResult<GetEventCampaignModel>(new GetEventCampaignModel
            {
                Id = cacheVal.Id,
                CreatedAt = cacheVal.CreatedAt,
                UpdatedAt = cacheVal.UpdatedAt,
                CampaignCategoryId = cacheVal.CampaignCategoryId,
                Name = cacheVal.Name,
                Budget = cacheVal.Budget,
                Spend = cacheVal.Spend,
                Type = cacheVal.Type,
                Status = cacheVal.Status
            });
        }

        Database.EventCampaignEntity? eventCampaignEntityDataBase = _database.GetById(id);
        if (eventCampaignEntityDataBase == null) return new NotFoundResult<GetEventCampaignModel>("123");


        return new SuccessDataResult<GetEventCampaignModel>(new GetEventCampaignModel
        {
            Id = eventCampaignEntityDataBase.Id,
            CreatedAt = eventCampaignEntityDataBase.CreatedAt,
            UpdatedAt = eventCampaignEntityDataBase.UpdatedAt,
            CampaignCategoryId = eventCampaignEntityDataBase.CampaignCategoryId,
            Name = eventCampaignEntityDataBase.Name,
            Budget = eventCampaignEntityDataBase.Budget,
            Spend = eventCampaignEntityDataBase.Spend,
            Type = eventCampaignEntityDataBase.Type,
            Status = eventCampaignEntityDataBase.Status
        });
    }
}