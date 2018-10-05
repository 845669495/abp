using System;
using JetBrains.Annotations;
using Volo.Abp.Collections;
using Volo.Abp.DynamicProxy;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 在服务注册后上下文
    /// </summary>
    public class OnServiceRegistredContext : IOnServiceRegistredContext
    {
        public virtual ITypeList<IAbpInterceptor> Interceptors { get; }

        public virtual Type ServiceType { get; }

        public virtual Type ImplementationType { get; }

        public OnServiceRegistredContext(Type serviceType, [NotNull] Type implementationType)
        {
            ServiceType = Check.NotNull(serviceType, nameof(serviceType));
            ImplementationType = Check.NotNull(implementationType, nameof(implementationType));

            Interceptors = new TypeList<IAbpInterceptor>();
        }
    }
}