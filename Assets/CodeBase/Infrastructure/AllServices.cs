using CodeBase.Service;
namespace CodeBase.Infrastructure
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.ServiceImplementation = implementation;

        public TService Single<TService>() where TService : IService => Implementation<TService>.ServiceImplementation;

        private static class Implementation<TService>
        {
            public static TService ServiceImplementation;
        }
    }
}