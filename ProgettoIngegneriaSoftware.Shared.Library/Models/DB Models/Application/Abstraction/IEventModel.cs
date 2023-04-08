namespace ProgettoIngegneriaSoftware.Shared.Library.Models.DB_Models.Application.Abstraction
{
    public interface IEventModel : IEditableEventModel, IReadableEventModel
    {

        #region CONSTS

        public const int EVENT_NAME_MAX_LENGTH = 64;
        public const int EVENT_NAME_MIN_LENGTH = 1;
        public const int EVENT_DESCRIPTION_MAX_LENGTH = 255;
        public const int EVENT_DESCRIPTION_MIN_LENGTH = 1;
        public const int EVENT_SEATS_MIN_VALUE = 0;
        public const int EVENT_BOOKED_SEATS_MIN_VALUE = 0;

        #endregion CONSTS

        public new Guid Id { get; set; }
        public new string Name { get; set; }
        public new string Description { get; set; }
        public new DateTime Date { get; set; }
        public new int Seats { get; set; }
        public new int BookedSeats { get; }

    }
}
