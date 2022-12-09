using PayPal.Api;

namespace ShoesShop.UI.Models
{
    public class PaypalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static PaypalConfiguration()
        {
            ClientId = "AQ8OjxO__J60pc0xZ8ugvtarVTnihQlhG1eFJih5_g15qvOdf0aA2lmDTbH2oIU0s1qTKoPVZBzdLPCS";
            ClientSecret = "EMpuqpKngXlW_MGRx9mOe8y7tzzyVcnY2wVqlfOwpiQOXq0HV5ShbibaX7Upu7SmBr21sk3ZQefs4Azs";
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        //Create access token
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        // This will return APIContext object
        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}
