using System;
using Volo.Abp.Collections;
using Volo.Abp.DynamicProxy;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 在服务注册后上下文
    /// </summary>
    public interface IOnServiceRegistredContext
    {
        /// <summary>
        /// 拦截器
        /// </summary>
        ITypeList<IAbpInterceptor> Interceptors { get; }
        /// <summary>
        /// 实现类型
        /// </summary>
        Type ImplementationType { get; }
    }
}