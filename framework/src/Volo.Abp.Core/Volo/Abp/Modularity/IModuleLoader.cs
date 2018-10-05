using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity.PlugIns;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public interface IModuleLoader
    {
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startupModuleType"></param>
        /// <param name="plugInSources"></param>
        /// <returns></returns>
        [NotNull]
        IAbpModuleDescriptor[] LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType,
            [NotNull] PlugInSourceList plugInSources
        );
    }
}
