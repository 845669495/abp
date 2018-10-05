using System;
using System.Collections.Generic;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// 服务暴露时回调集合
    /// </summary>
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}