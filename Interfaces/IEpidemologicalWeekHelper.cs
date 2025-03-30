namespace ApiAlertaDengue.Interfaces;

public interface IEpidemologicalWeekHelper
{
    (int ewStart, int ewEnd, int  eyStar, int eyEnd) GetLastSixMonths();
}