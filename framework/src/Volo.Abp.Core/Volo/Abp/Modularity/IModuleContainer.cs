using System.Collections.Generic;
using JetBrains.Annotations;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// ģ������
    /// </summary>
    public interface IModuleContainer
    {
        [NotNull]
        IReadOnlyList<IAbpModuleDescriptor> Modules { get; }
    }
}