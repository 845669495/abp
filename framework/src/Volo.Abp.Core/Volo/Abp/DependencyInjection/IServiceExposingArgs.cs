using System;
using System.Collections.Generic;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 服务暴露时上下文
    /// </summary>
    public interface IOnServiceExposingContext
    {
        /// <summary>
        /// 实现类型
        /// </summary>
        Type ImplementationType { get; }
        /// <summary>
        /// 暴露类型集合
        /// </summary>
        List<Type> ExposedTypes { get; }
    }
}