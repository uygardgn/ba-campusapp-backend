namespace BACampusApp.Business.Concretes
{
    public class PhoneNumberManager : IPhoneNumberService
    {
        private readonly IpApiService _ipApiService;

        public PhoneNumberManager(IpApiService ipApiService)
        {
            _ipApiService = ipApiService;
        }

        private async Task<string?> GetCountryCodeAsync()
        {
            return await _ipApiService.GetCountryCodeOfCurrentUserByIpAsync();
        }

        /// <summary>
        /// Bu method telefon numarasını uluslararası telefon numarası formatına çevirir.
        /// </summary>
        /// <param name="phoneNumber">Telefon numarası.</param>
        /// <param name="countryCode">2 haneli ülke kodu, örn:'TR'.</param>
        /// <returns>Formatlanmış telefon numarası döner.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string?> PhoneNumberFormaterAsync(string phoneNumber, string? countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))            
                countryCode = await GetCountryCodeAsync();            

            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                int internationalCallingCode = phoneNumberUtil.GetCountryCodeForRegion(countryCode);



                PhoneNumber numberProto = phoneNumberUtil.Parse(phoneNumber, countryCode);

                if (!phoneNumberUtil.IsValidNumber(numberProto))
                    throw new Exception();

                // Telefon numarasını uluslararası formata çevir
                string formattedPhoneNumber = phoneNumberUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);

                string strippedFormattedPhoneNumber = formattedPhoneNumber.Replace($"+{internationalCallingCode} ", "");

                return strippedFormattedPhoneNumber;


                //PhoneNumber numberProto = phoneNumberUtil.Parse(phoneNumber, countryCode);

                //if (!phoneNumberUtil.IsValidNumber(numberProto))
                //    throw new Exception();

                //return phoneNumberUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);
            }
            catch (Exception )
            {
                throw;
            }
        }

        public async Task<string?> GetCountryCodeAsync(string? countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
                countryCode = await GetCountryCodeAsync();

            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                int internationalCallingCode = phoneNumberUtil.GetCountryCodeForRegion(countryCode);

                return $"+{internationalCallingCode}";

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string?> GetCountryByCountryCodeAsync(string? countryCode)
        {
            //if (string.IsNullOrEmpty(countryCode))
            //    countryCode = await GetCountryCodeAsync();
            if(countryCode== null)
            {
                return null;
            }

            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                int countryCallingCode = int.Parse(countryCode); // Country code'u integer'a çevir

                // Country code'a göre ülke kodunu al
                string regionCode = phoneNumberUtil.GetRegionCodeForCountryCode(countryCallingCode);

                return regionCode;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
