using Microsoft.Extensions.Configuration;

namespace Volo.Abp.Configuration
{
    /// <summary>
    /// 默认配置访问器
    /// </summary>
    public class DefaultConfigurationAccessor : IConfigurationAccessor
    {
        public static DefaultConfigurationAccessor Empty { get; }

        public virtual IConfigurationRoot Configuration { get; }

        static DefaultConfigurationAccessor()
        {
            Empty = new DefaultConfigurationAccessor(
                new ConfigurationBuilder().Build()
            );
        }

        public DefaultConfigurationAccessor(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
    }
}