/********************************************************************
	created:	2018/12/04
	created:	4:12:2018   16:36
	author:		feefiliang
	
	purpose:	网络数据流处理器初始化，在游戏启动时根据反射创建所有网络数据流处理对象，并注册网络消息
*********************************************************************/

using System;
using System.Reflection;
using System.Collections.Generic;

public class FViewModelAgentMgr 
{
    List<FViewModelAgentBase> _dataPipes = new List<FViewModelAgentBase>();
    public void Init()
    {
        Assembly ass = typeof(FViewModelAgentBase).Assembly;
        Type[] Types = ass.GetTypes();
        for (int ti = 0; Types != null && ti < Types.Length; ++ti)
        {
            var t = Types[ti];
            if (!t.IsAbstract && t.IsSubclassOf(typeof(FViewModelAgentBase)))
            {
                FViewModelAgentBase fDataPipe = Activator.CreateInstance(t) as FViewModelAgentBase;
                _dataPipes.Add(fDataPipe);
                //MethodInfo[] Methods = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

                //for (int m = 0; Methods != null && m < Methods.Length; ++m)
                //{
                //    var Method = Methods[m];
                //    var attrs = Method.GetCustomAttributes(typeof(MessageHandlerAttribute), true);
                //    if (attrs != null && attrs.Length > 0)
                //    {
                //        var attr = (MessageHandlerAttribute)attrs[0];
                //        var netCallback = Method.IsStatic ? (NetMsgDelegate)Delegate.CreateDelegate(typeof(NetMsgDelegate), Method)
                //            : (NetMsgDelegate)Delegate.CreateDelegate(typeof(NetMsgDelegate), fDataPipe, Method);
                //        NetworkModule.instance.RegisterMsgHandler(attr.ID, netCallback);
                //    }
                //}
            }

        }
        for (int i = 0; i < _dataPipes.Count; ++i)
        {
            _dataPipes[i].__PrivateInit();
        }
    }
}

/// <summary>
/// 主要的功能是用于反射和初始化,及保存数据流
/// </summary>
public abstract class FViewModelAgentBase
{
    protected FViewModel _data;

    public FViewModelAgentBase(FViewModel data)
    {
        _data = data;
    }
    public FViewModel __Privatedata
    {
        get { return _data; }
    }
    public void __PrivateInit()
    {
        _data.__PrivateInit();
        OnInit();
    }
    protected abstract void OnInit();
}

