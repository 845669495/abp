using System;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 暴露的服务类型提供者
    /// </summary>
    public interface IExposedServiceTypesProvider
    {
        Type[] GetExposedServiceTypes(Type targetType);
    }
}
