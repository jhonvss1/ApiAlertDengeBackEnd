using System.ComponentModel.DataAnnotations;

namespace ApiAlertaDengue.Models;

public class DengueAlert
{
    [Key]
    public int Id { get; set; }
    public int EpidemologicalWeek { get; set; }
    public int Year { get; set; }
    public double EstimatedCases  {get; set;}
    public int ReporteCases {get; set;}
    public AlertLevel AlertLevel {get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum AlertLevel
{
    Green = 1, //
    Yellow = 2, 
    Orange = 3,
    Red = 4,
}