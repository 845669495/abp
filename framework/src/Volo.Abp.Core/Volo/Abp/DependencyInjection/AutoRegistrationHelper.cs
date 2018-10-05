using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 自动注册帮助类
    /// </summary>
    public static class AutoRegistrationHelper
    {
        /// <summary>
        /// 获取暴露的服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetExposedServices(IServiceCollection services, Type type)
        {
            var typeInfo = type.GetTypeInfo();

            var customExposedServices = typeInfo
                .GetCustomAttributes()
                .OfType<IExposedServiceTypesProvider>()
                .SelectMany(p => p.GetExposedServiceTypes(type))
                .ToList();

            if (customExposedServices.Any())  //自定义暴露服务类型，标记ExposeServicesAttribute属性
            {
                return customExposedServices;
            }

            return GetDefaultExposedServices(services, type);
        }

        /// <summary>
        /// 获取默认暴露的服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetDefaultExposedServices(IServiceCollection services, Type type)
        {
            var serviceTypes = new List<Type>();

            serviceTypes.Add(type);  //默认暴露自身类型

            foreach (var interfaceType in type.GetTypeInfo().GetInterfaces())
            {
                //默认暴露命名结尾一致的接口
                var interfaceName = interfaceType.Name;

                if (interfaceName.StartsWith("I"))
                {
                    interfaceName = interfaceName.Right(interfaceName.Length - 1);
                }

                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfaceType);
                }
            }

            var exposeActions = services.GetExposingActionList();
            if (exposeActions.Any())
            {
                var args = new OnServiceExposingContext(type, serviceTypes);
                foreach (var action in services.GetExposingActionList())
                {
                    //触发服务暴露事件
                    action(args);
                }
            }

            return serviceTypes;
        }
    }
}
