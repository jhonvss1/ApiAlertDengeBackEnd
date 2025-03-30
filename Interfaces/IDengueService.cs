using ApiAlertaDengue.Models;

namespace ApiAlertaDengue.Interfaces;

public interface IDengueService
{
    Task<IEnumerable<DengueAlert?>> GetAlertsAsync();
    Task<DengueAlert?> GetAlertByWeekAsync( int year, int week);
    Task<bool> FetchAndStoreAlertsAsync();
}