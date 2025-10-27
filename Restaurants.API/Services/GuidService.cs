namespace Restaurants.API.Services
{
    public interface IGuidService
    {
        Guid GetGuid1();
        Guid GetGuid2();
    }

    public sealed class GuidService : IGuidService
    {
        private readonly Guid _guid;

        public GuidService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid GetGuid1()
        {
            return _guid;
        }

        public Guid GetGuid2()
        {
            return _guid;
        }
    }
}