using System;
using Volo.Abp.Collections;
using Volo.Abp.DynamicProxy;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// �ڷ���ע���������
    /// </summary>
    public interface IOnServiceRegistredContext
    {
        /// <summary>
        /// ������
        /// </summary>
        ITypeList<IAbpInterceptor> Interceptors { get; }
        /// <summary>
        /// ʵ������
        /// </summary>
        Type ImplementationType { get; }
    }
}