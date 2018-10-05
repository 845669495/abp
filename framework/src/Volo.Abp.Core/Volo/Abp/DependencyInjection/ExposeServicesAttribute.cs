using System;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 暴露服务属性
    /// </summary>
    public class ExposeServicesAttribute : Attribute, IExposedServiceTypesProvider
    {
        public Type[] ExposedServiceTypes { get; }

        public ExposeServicesAttribute(params Type[] exposedServiceTypes)
        {
            ExposedServiceTypes = exposedServiceTypes ?? new Type[0];
        }

        public Type[] GetExposedServiceTypes(Type targetType)
        {
            return ExposedServiceTypes;
        }
    }
}