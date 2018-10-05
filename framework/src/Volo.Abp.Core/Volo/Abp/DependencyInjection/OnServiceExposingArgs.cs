using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 服务暴露时上下文
    /// </summary>
    public class OnServiceExposingContext : IOnServiceExposingContext
    {
        public Type ImplementationType { get; }

        public List<Type> ExposedTypes { get; }

        public OnServiceExposingContext([NotNull] Type implementationType, List<Type> exposedTypes)
        {
            ImplementationType = Check.NotNull(implementationType, nameof(implementationType));
            ExposedTypes = Check.NotNull(exposedTypes, nameof(exposedTypes));
        }
    }
}