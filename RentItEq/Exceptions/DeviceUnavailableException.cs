namespace RentItEq.Exceptions;

public class DeviceUnavailableException : Exception
{
    public DeviceUnavailableException(string deviceName, string status)
        : base($"Device '{deviceName}' is unavailable (status: {status}).") { }
}
