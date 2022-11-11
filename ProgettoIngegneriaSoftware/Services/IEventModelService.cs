using ProgettoIngegneriaSoftware.Models.DB_Models.Application;
using ProgettoIngegneriaSoftware.Models.DB_Models.Application.Abstraction;

namespace ProgettoIngegneriaSoftware.Services
{
    public interface IEventModelService
    {

        #region CREATE

        public Task<IReadableEventModel?> CreateAsync(IEditableEventModel editableEventModel, Guid adminId);

        #endregion CREATE

        #region READ

        public Task<ICollection<IReadableEventModel>> GetAsync();

        public Task<IReadableEventModel?> GetAsync(Guid id);

        #endregion READ

        #region UPDATE



        #endregion UPDATE

        #region DELETE



        #endregion DELETE

    }
}
