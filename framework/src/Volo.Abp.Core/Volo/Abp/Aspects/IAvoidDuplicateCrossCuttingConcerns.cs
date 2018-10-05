using System.Collections.Generic;

namespace Volo.Abp.Aspects
{
    /// <summary>
    /// 避免重复的横切问题
    /// </summary>
    public interface IAvoidDuplicateCrossCuttingConcerns
    {
        /// <summary>
        /// 应用的横切问题
        /// </summary>
        List<string> AppliedCrossCuttingConcerns { get; }
    }
}