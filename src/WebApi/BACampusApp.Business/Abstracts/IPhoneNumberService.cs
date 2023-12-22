namespace BACampusApp.Business.Abstracts
{
    public interface IPhoneNumberService
    {
        Task<string?> PhoneNumberFormaterAsync(string phoneNumber, string? countryCode);
        Task<string?> GetCountryCodeAsync(string? countryCode);
        Task<string?> GetCountryByCountryCodeAsync(string? countryCode);
    }
}
