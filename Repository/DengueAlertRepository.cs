using ApiAlertaDengue.Data;
using ApiAlertaDengue.Interfaces;
using ApiAlertaDengue.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAlertaDengue.Repository;

public class DengueAlertRepository : IDengueRepository 
{
    private readonly AppDbContext _context;

    public DengueAlertRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DengueAlert?>> GetAllAsync()
    {
        return await _context.DengueAlerts.ToListAsync();
    }

    public async Task<DengueAlert?> GetByWeekAsync(int week, int year)
    {   
        return await _context.DengueAlerts
            .FirstOrDefaultAsync(da => da.EpidemologicalWeek == week && da.Year == year);
    }

    public async Task AddAsync(DengueAlert dengueAlert)
    {
        await _context.DengueAlerts.AddAsync(dengueAlert);
    }

    public async Task AddRangeAsync(IEnumerable<DengueAlert> dengueAlerts)
    {
        await _context.DengueAlerts.AddRangeAsync(dengueAlerts);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}