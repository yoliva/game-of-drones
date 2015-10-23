using System.Web.Http;
using GameOfDrones.Data.Repositories;
using GameOfDrones.Domain.Repositories;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace GameOfDrones.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IGameOfDronesRepository, GameOfDronesRepository >();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}