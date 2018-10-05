using System;
using JetBrains.Annotations;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// 依赖类型提供者
    /// </summary>
    public interface IDependedTypesProvider
    {
        [NotNull]
        Type[] GetDependedTypes();
    }
}