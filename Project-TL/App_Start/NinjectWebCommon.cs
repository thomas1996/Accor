[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Project_TL.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Project_TL.App_Start.NinjectWebCommon), "Stop")]

namespace Project_TL.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Models.Domain;
    using Models.DAL;
    using Models.DAL.Mapping;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHotelRepository>().To<HotelRepository>().InRequestScope();
            kernel.Bind<IApplicationRepository>().To<ApplicationRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IOwnerRepository>().To<OwnerRepository>().InRequestScope();
            kernel.Bind<IContactPersonRepository>().To<ContactPersonRepository>().InRequestScope();
            kernel.Bind<IBranchRepository>().To<BranchRepository>().InRequestScope();
            kernel.Bind<IFirmRepository>().To<FirmRepository>().InRequestScope();
            kernel.Bind<ILoginService>().To<CostumLoginService>().InRequestScope();
            kernel.Bind<IStatusRepository>().To<StatusRepository>().InRequestScope();

            
        }        
    }
}
