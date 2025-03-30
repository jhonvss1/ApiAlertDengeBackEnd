using ApiAlertaDengue.Interfaces;

namespace ApiAlertaDengue.Helpers;

public class EpidemologicalWeekHelper : IEpidemologicalWeekHelper
{
    public (int ewStart, int ewEnd, int eyStar, int eyEnd) GetLastSixMonths()
    {
        DateTime today = DateTime.Today;
        DateTime lastSixMonths = today.AddMonths(-6);
        
        //semana e ano de seis meses atrás
        var (ewStart, eyStart) = GetWeekEpidemologicalWeek(lastSixMonths);
        //semana e ano de hoje
        var (ewEnd, eyEnd) = GetWeekEpidemologicalWeek(today);
        
        return  (ewStart, ewEnd, eyStart, eyEnd);
        
    }

    private (int week, int year) GetWeekEpidemologicalWeek(DateTime date)
    {
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        int week = culture.Calendar.GetWeekOfYear(
            date,
            // define que a primeira semana do ano tenha pelo menos quatro dias
            System.Globalization.CalendarWeekRule.FirstFourDayWeek, 
            DayOfWeek.Monday // define que a semna sempre comeca na seguna feira
            );
        //validando o ano em que a semana epidemiologica está
        int year = date.Month == 1 && week > 50 ? date.Year -1 : date.Year;
        return (week, year);
    }
}