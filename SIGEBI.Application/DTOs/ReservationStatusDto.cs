
namespace SIGEBI.Application.DTOs
{
    public class ReservationStatusDto : Base.DtoBase
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
