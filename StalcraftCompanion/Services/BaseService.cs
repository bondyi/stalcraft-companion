using RestSharp;

namespace StalcraftCompanion.Services
{
    public abstract class BaseService
    {
        protected RestClient Client { get; }

        public BaseService()
        {
            Client = new RestClient();
        }
    }
}
