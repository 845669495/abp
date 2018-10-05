using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity.PlugIns;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public class ModuleLoader : IModuleLoader
    {
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        /// <returns></returns>
        public IAbpModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(plugInSources, nameof(plugInSources));

            var modules = GetDescriptors(services, startupModuleType, plugInSources);

            modules = SortByDependency(modules, startupModuleType);
            ConfigureServices(modules, services);

            return modules.ToArray();
        }

        private List<IAbpModuleDescriptor> GetDescriptors(
            IServiceCollection services, 
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            var modules = new List<AbpModuleDescriptor>();

            FillModules(modules, services, startupModuleType, plugInSources);
            SetDependencies(modules);

            return modules.Cast<IAbpModuleDescriptor>().ToList();
        }

        /// <summary>
        /// 填充模块
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="services"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        protected virtual void FillModules(
            List<AbpModuleDescriptor> modules,
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources)
        {
            //All modules starting from the startup module
            foreach (var moduleType in AbpModuleHelper.FindAllModuleTypes(startupModuleType))
            {
                modules.Add(CreateModuleDescriptor(services, moduleType));
            }

            //Plugin modules
            foreach (var moduleType in plugInSources.GetAllModules())
            {
                if (modules.Any(m => m.Type == moduleType))
                {
                    continue;
                }

                modules.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
            }
        }

        protected virtual void SetDependencies(List<AbpModuleDescriptor> modules)
        {
            foreach (var module in modules)
            {
                SetDependencies(modules, module);
            }
        }

        /// <summary>
        /// 根据依赖排序
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        protected virtual List<IAbpModuleDescriptor> SortByDependency(List<IAbpModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        /// <summary>
        /// 创建模块描述
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleType"></param>
        /// <param name="isLoadedAsPlugIn"></param>
        /// <returns></returns>
        protected virtual AbpModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
        {
            return new AbpModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
        }

        /// <summary>
        /// 创建并注册模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        protected virtual IAbpModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IAbpModule)Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="services"></param>
        protected virtual void ConfigureServices(List<IAbpModuleDescriptor> modules, IServiceCollection services)
        {
            var context = new ServiceConfigurationContext(services);
            services.AddSingleton(context);

            foreach (var module in modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = context;
                }
            }

            //PreConfigureServices
            foreach (var module in modules.Where(m => m.Instance is IPreConfigureServices))
            {
                ((IPreConfigureServices)module.Instance).PreConfigureServices(context);
            }

            //ConfigureServices
            foreach (var module in modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    if (!abpModule.SkipAutoServiceRegistration)
                    {
                        services.AddAssembly(module.Type.Assembly);
                    }
                }

                module.Instance.ConfigureServices(context);
            }

            //PostConfigureServices
            foreach (var module in modules.Where(m => m.Instance is IPostConfigureServices))
            {
                ((IPostConfigureServices)module.Instance).PostConfigureServices(context);
            }

            foreach (var module in modules)
            {
                if (module.Instance is AbpModule abpModule)
                {
                    abpModule.ServiceConfigurationContext = null;
                }
            }
        }

        /// <summary>
        /// 设置依赖关系
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="module"></param>
        protected virtual void SetDependencies(List<AbpModuleDescriptor> modules, AbpModuleDescriptor module)
        {
            foreach (var dependedModuleType in AbpModuleHelper.FindDependedModuleTypes(module.Type))
            {
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule == null)
                {
                    throw new AbpException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }
    }
}