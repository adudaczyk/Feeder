namespace FeederSokML.Services
{
    public interface IStatusService
    {
        short? GetStatus(int procesId);
        void ChangeStatus(int procesId, short newStatus);
    }
}