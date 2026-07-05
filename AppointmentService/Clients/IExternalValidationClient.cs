namespace AppointmentService.Clients;

public interface IExternalValidationClient
{
    Task<bool> PatientExistsAsync(int patientId, CancellationToken cancellationToken = default);
    Task<bool> DoctorExistsAsync(int doctorId, CancellationToken cancellationToken = default);
}
