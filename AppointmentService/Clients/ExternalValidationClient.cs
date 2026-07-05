namespace AppointmentService.Clients;

public class ExternalValidationClient(HttpClient httpClient, IConfiguration configuration, ILogger<ExternalValidationClient> logger) : IExternalValidationClient
{
    public async Task<bool> PatientExistsAsync(int patientId, CancellationToken cancellationToken = default)
    {
        var patientBaseUrl = configuration["ExternalServices:PatientServiceBaseUrl"];
        if (string.IsNullOrWhiteSpace(patientBaseUrl))
        {
            logger.LogWarning("Patient service base URL is not configured.");
            return false;
        }

        using var response = await httpClient.GetAsync($"{patientBaseUrl}/api/v1/patients/{patientId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DoctorExistsAsync(int doctorId, CancellationToken cancellationToken = default)
    {
        var doctorBaseUrl = configuration["ExternalServices:DoctorServiceBaseUrl"];
        if (string.IsNullOrWhiteSpace(doctorBaseUrl))
        {
            logger.LogWarning("Doctor service base URL is not configured.");
            return false;
        }

        using var response = await httpClient.GetAsync($"{doctorBaseUrl}/api/v1/doctors/{doctorId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
