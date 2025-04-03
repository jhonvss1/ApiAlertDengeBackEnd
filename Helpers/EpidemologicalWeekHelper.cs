using ApiAlertaDengue.Interfaces;

namespace ApiAlertaDengue.Helpers;

public class EpidemologicalWeekHelper : IEpidemologicalWeekHelper
{
    public (int ewStart, int ewEnd, int eyStar, int eyEnd) GetLastSixMonths()
    {
        DateTime today = DateTime.Today;
        DateTime lastSixMonths = today.AddMonths(-6);
        

        var (ewStart, eyStart) = GetEpidemologicalWeek(lastSixMonths);

        var (ewEnd, eyEnd) = GetEpidemologicalWeek(today);
        
        return  (ewStart, ewEnd, eyStart, eyEnd);
        
    }

    private (int week, int year) GetEpidemologicalWeek(DateTime date)
    {
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        int week = culture.Calendar.GetWeekOfYear(
            date,
            System.Globalization.CalendarWeekRule.FirstFourDayWeek, 
            DayOfWeek.Monday
            );

        int year = date.Month == 1 && week > 50 ? date.Year -1 : date.Year;
        return (week, year);
    }
}