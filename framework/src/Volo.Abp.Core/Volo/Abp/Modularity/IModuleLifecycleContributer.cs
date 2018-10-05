using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// 模块生命周期贡献者
    /// </summary>
    public interface IModuleLifecycleContributer : ISingletonDependency
    {
        void Initialize([NotNull] ApplicationInitializationContext context, [NotNull] IAbpModule module);

        void Shutdown([NotNull] ApplicationShutdownContext context, [NotNull] IAbpModule module);
    }
}
