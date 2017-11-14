using Microsoft.Practices.Unity;
using PersonalReferenceProject.Cryptography;
using PersonalReferenceProject.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Unity.WebApi;

namespace PersonalReferenceProject
{
    //public sealed class UnityConfig
    //{
    //    private readonly static UnityConfig _instance = new UnityConfig();
    //    static UnityConfig() { }
    //    private UnityConfig() { }
    //    public static UnityConfig Instance {get { return _instance; } }

    //    public void RegisterComponents(HttpConfiguration config)
    //    {
    //        UnityContainer container = new UnityContainer();

    //        // register all your components with the container here
    //        // it is NOT necessary to register your controllers

    //        // e.g. container.RegisterType<ITestService, TestService>();
    //        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
    //        config.DependencyResolver = new UnityResolver(container);
    //    }

    //    public class UnityResolver : System.Web.Http.Dependencies.IDependencyResolver
    //    {
    //        protected IUnityContainer container;

    //        public UnityResolver(IUnityContainer container)
    //        {
    //            if (container == null)
    //                throw new ArgumentException("missing container");
    //            this.container = container;
    //        }
    //        public object GetService(Type serviceType)
    //        {
    //            try { return container.Resolve(serviceType); }
    //            catch (ResolutionFailedException) { return null; }
    //        }
    //        public IEnumerable<object> GetServices(Type serviceType)
    //        {
    //            try { return container.ResolveAll(serviceType); }
    //            catch(ResolutionFailedException) { return new List<object>(); }
    //        }
    //        public IDependencyScope BeginScope()
    //        {
    //            IUnityContainer child = container.CreateChildContainer();
    //            return new UnityResolver(child);
    //        }
    //        public void Dispose()
    //        {
    //            container.Dispose();
    //        }
    //    }
    //}
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //container.RegisterType<IReviewService, ReviewService>();
            
            container.RegisterType<IUserNameService, UserNameService>();
            container.RegisterType<ICryptographyService, CryptographyService>();
            container.RegisterType<IReferenceService, ReferenceService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }

}