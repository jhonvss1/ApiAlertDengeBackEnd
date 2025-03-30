using ApiAlertaDengue.Helpers;
using ApiAlertaDengue.Interfaces;
using ApiAlertaDengue.Models;


namespace ApiAlertaDengue.Services;

public class DengueService : IDengueService
{
    private readonly IDengueRepository _repository;
    private readonly HttpClient _httpClient;
    private const string API_URL = "https://info.dengue.mat.br/api/alertcity";

    public DengueService(IDengueRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<DengueAlert?>> GetAlertsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<DengueAlert?> GetAlertByWeekAsync(int week, int year)
    {
        return await _repository.GetByWeekAsync(week, year);
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

            // Construindo a query string corretamente
            var queryString = string.Join("&", queryParams
                .Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            
            var url = $"{API_URL}?{queryString}";


            Console.WriteLine($"Generated URL: {url}");//para debug
            
            var response = await _httpClient.GetFromJsonAsync<List<ExternalApiResponse>>(url);
            
            
            if (response == null || response.Count == 0) return false;

            
            //transformando a resposta da api em um padrao de dados interno do sistema
            var alerts = response.Select(r => new DengueAlert
            {
                EpidemologicalWeek = r.SemanaEpidemologica,
                Year = r.SemanaEpidemologica/100,
                EstimatedCases = r.CasosEstimados,
                ReporteCases = r.CasosNotificados,
                AlertLevel = (AlertLevel)r.Nivel,
            });

            await _repository.AddRangeAsync(alerts);
            return await _repository.SaveAllAsync();
        }
        catch (Exception e)
        {
            return false;
        }
    }
}