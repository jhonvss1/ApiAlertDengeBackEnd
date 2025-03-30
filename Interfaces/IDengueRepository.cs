using ApiAlertaDengue.Models;

namespace ApiAlertaDengue.Interfaces;

public interface IDengueRepository
{
    Task<IEnumerable<DengueAlert?>> GetAllAsync();
    Task<DengueAlert?> GetByWeekAsync(int week, int year);
    Task AddAsync(DengueAlert dengueAlert);
    Task AddRangeAsync(IEnumerable<DengueAlert> dengueAlerts);
    Task<bool> SaveAllAsync();
}