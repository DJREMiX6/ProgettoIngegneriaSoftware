using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application.Abstraction;

namespace ProgettoIngegneriaSoftware.Services
{
    public class EventModelService : IEventModelService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<EventModelService> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public EventModelService(ILogger<EventModelService> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        #endregion CTORS

        #region CREATE

        public async Task<IReadableEventModel?> CreateAsync(IEditableEventModel editableEventModel, Guid adminId)
        {
            var eventToCreate = new EventModel(editableEventModel);

            await _applicationDbContext.Events.AddAsync(eventToCreate);
            await _applicationDbContext.SaveChangesAsync();

            return eventToCreate;
        }

        #endregion CREATE

        #region READ

        public async Task<ICollection<IReadableEventModel>> GetAsync()
        {
            var eventsList = await _applicationDbContext.Events.ToListAsync();
            return eventsList.ToArray();
        }

        public async Task<IReadableEventModel?> GetAsync(Guid id)
        {
            return await _applicationDbContext.Events.FindAsync(id);
        }

        #endregion READ

        #region UPDATE



        #endregion UPDATE

        #region DELETE



        #endregion DELETE

    }
}
