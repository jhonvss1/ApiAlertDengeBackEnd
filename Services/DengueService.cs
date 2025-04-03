using ApiAlertaDengue.Helpers;
using ApiAlertaDengue.Interfaces;
using ApiAlertaDengue.Models;


namespace ApiAlertaDengue.Services;

public class DengueService(IDengueRepository repository, HttpClient httpClient) : IDengueService
{
    private const string ApiUrl = "https://info.dengue.mat.br/api/alertcity";

    public async Task<IEnumerable<DengueAlert?>> GetAlertsAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<DengueAlert?> GetAlertByWeekAsync(int week, int year)
    {
        try
        {
            return await repository.GetByWeekAsync(week, year);
        }
        catch (Exception e)
        {
            Console.WriteLine("é necessário adicionar uma semana que" +
                              " esteja dentro do intervaolo dos últimos seis meses da data atual",e.Message);
            throw;
        }
    }

    public async Task<bool> FetchAndStoreAlertsAsync()
    {
        try
        {
            var (startWeek, endWeek, startYear, endYear) = new EpidemologicalWeekHelper()
                .GetLastSixMonths();

            var queryParams = new Dictionary<string, string>()
            {
                { "geocode", "3106200" },
                { "disease", "dengue" },
                { "format", "json" },
                { "ew_start", startWeek.ToString() },
                { "ew_end", endWeek.ToString() },
                { "ey_start", startYear.ToString() },
                { "ey_end", endYear.ToString() }
            };
            
            var queryString = string.Join("&", queryParams
                .Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            
            var url = $"{ApiUrl}?{queryString}";
            
            var response = await httpClient.GetFromJsonAsync<List<ExternalApiResponse>>(url);
            
            if (response == null || response.Count == 0) return false;
            
            var alerts = response.Select(r => new DengueAlert
            {
                EpidemologicalWeek = r.SemanaEpidemologica,
                Year = r.SemanaEpidemologica/100,
                EstimatedCases = r.CasosEstimados,
                ReporteCases = r.CasosNotificados,
                AlertLevel = (AlertLevel)r.Nivel,
            });

            await repository.AddRangeAsync(alerts);
            return await repository.SaveAllAsync();
        }
        catch (Exception e)
        {
            return false;
        }
    }
}