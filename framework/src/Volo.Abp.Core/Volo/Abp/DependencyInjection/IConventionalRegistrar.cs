using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 常规注册接口
    /// </summary>
    public interface IConventionalRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        void AddAssembly(IServiceCollection services, Assembly assembly);
        /// <summary>
        /// 注册类型集合
        /// </summary>
        /// <param name="services"></param>
        /// <param name="types"></param>
        void AddTypes(IServiceCollection services, params Type[] types);
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        void AddType(IServiceCollection services, Type type);
    }
}
