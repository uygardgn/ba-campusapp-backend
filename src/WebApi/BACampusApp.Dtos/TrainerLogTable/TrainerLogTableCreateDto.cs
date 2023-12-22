using BACampusApp.Entities.Enums;
namespace BACampusApp.Dtos.TrainerLogTable
{
    public class TrainerLogTableCreateDto
    {
        public Guid Id { get; set; }
        public Guid TrainerId { get; set; }
        public TrainerActionType TrainerActionType { get; set; }
        public string? Description { get; set; }
    }
}
