using System;
using System.Collections.Generic;

namespace Volo.Abp.DependencyInjection
{
    /// <summary>
    /// ����¶ʱ�ص�����
    /// </summary>
    public class ServiceExposingActionList : List<Action<IOnServiceExposingContext>>
    {

    }
}