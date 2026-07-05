namespace AppointmentService.Common;

public class ResourceNotFoundException(string message) : Exception(message);
