using System;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Volo.Abp.DependencyInjection
{
    //TODO: Make DefaultConventionalRegistrar extensible, so we can only define GetLifeTimeOrNull to contribute to the convention. This can be more performant!

    /// <summary>
    /// 默认常规注册
    /// </summary>
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        public override void AddType(IServiceCollection services, Type type)
        {
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);  //获取生命周期

            if (lifeTime == null)  //生命周期为空，不做注册
            {
                return;
            }

            foreach (var serviceType in AutoRegistrationHelper.GetExposedServices(services, type))
            {
                var serviceDescriptor = ServiceDescriptor.Describe(serviceType, type, lifeTime.Value);

                if (dependencyAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        /// <summary>
        /// 是否禁用常规注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        /// <summary>
        /// 获取依赖属性，没有则为空
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual DependencyAttribute GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type, [CanBeNull] DependencyAttribute dependencyAttribute)
        {
            //首先根据依赖属性确定生命周期，没有则根据实现的依赖注入接口确定生命周期
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarcy(type);
        }

        /// <summary>
        /// 根据实现的依赖注入接口确定生命周期
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarcy(Type type)
        {
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }
    }
}