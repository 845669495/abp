using Microsoft.Extensions.Configuration;

namespace Volo.Abp.Configuration
{
    /// <summary>
    /// 配置访问器
    /// </summary>
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// 配置
        /// </summary>
        IConfigurationRoot Configuration { get; }
    }
}
