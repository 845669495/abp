using System;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// �����ṩ�߷�����
    /// </summary>
    public interface IServiceProviderAccessor
    {
        /// <summary>
        /// �����ṩ��
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}