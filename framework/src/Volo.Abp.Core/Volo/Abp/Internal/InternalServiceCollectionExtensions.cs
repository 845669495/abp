using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Configuration;
using Volo.Abp.Modularity;
using Volo.Abp.Reflection;

namespace Volo.Abp.Internal
{
    /// <summary>
    /// 内部服务收集扩展
    /// </summary>
    internal static class InternalServiceCollectionExtensions
    {
        /// <summary>
        /// 添加核心服务
        /// </summary>
        /// <param name="services"></param>
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddLocalization();
        }

        /// <summary>
        /// 添加核心abp服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="abpApplication"></param>
        internal static void AddCoreAbpServices(this IServiceCollection services, IAbpApplication abpApplication)
        {
            //注册一些服务
            var moduleLoader = new ModuleLoader();
            var assemblyFinder = new AssemblyFinder(abpApplication);
            var typeFinder = new TypeFinder(assemblyFinder);

            services.TryAddSingleton<IConfigurationAccessor>(DefaultConfigurationAccessor.Empty);
            services.TryAddSingleton<IModuleLoader>(moduleLoader);
            services.TryAddSingleton<IAssemblyFinder>(assemblyFinder);
            services.TryAddSingleton<ITypeFinder>(typeFinder);

            services.AddAssemblyOf<IAbpApplication>();  //注册程序集

            services.Configure<ModuleLifecycleOptions>(options =>  //配置应用生命周期
            {
                options.Contributers.Add<OnPreApplicationInitializationModuleLifecycleContributer>();
                options.Contributers.Add<OnApplicationInitializationModuleLifecycleContributer>();
                options.Contributers.Add<OnPostApplicationInitializationModuleLifecycleContributer>();
                options.Contributers.Add<OnApplicationShutdownModuleLifecycleContributer>();
            });
        }
    }
}