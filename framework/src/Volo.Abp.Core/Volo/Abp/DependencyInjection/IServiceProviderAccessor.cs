using System;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 服务提供者访问器
    /// </summary>
    public interface IServiceProviderAccessor
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}