using System.Collections.Generic;
using JetBrains.Annotations;

namespace Volo.Abp.Modularity
{
    /// <summary>
    /// Ä£¿éÈÝÆ÷
    /// </summary>
    public interface IModuleContainer
    {
        [NotNull]
        IReadOnlyList<IAbpModuleDescriptor> Modules { get; }
    }
}