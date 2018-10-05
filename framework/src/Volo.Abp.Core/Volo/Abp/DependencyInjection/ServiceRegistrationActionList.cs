using System;
using System.Collections.Generic;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 服务注册后回调集合
    /// </summary>
    public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
    {
        
    }
}