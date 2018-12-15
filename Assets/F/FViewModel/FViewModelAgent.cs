/********************************************************************
	created:	2018/12/04
	created:	4:12:2018   16:38
	author:		feefiliang
	
	purpose:	ViewModel处理基类,主要用来生成ViewModel
        一般来讲，一个系统的所需要的ViewModel数据是由服务器发送过来的网络消息再加上一些本地的excel等配置数据组合而成
    ViewModelAgent提供了自动监听网络消息，存储数据的功能。
    
    以下是一个兑换系统的用法示例：
    public class ExchangeViewModelAgent : FViewModelAgent<Model.ExchangeViewModel>
    {
        //当收到勋章变化时，更新兑换数据相应的内容
        [MessageHandler(CSProtocolMacros.SCID_NATIONALBATTLE_MEDAL_CHG_NTF)]
        public void OnNBMedalChangeNTF(CSPkg msg)
        {
            PipeData.Updatexx();
        }
    }

    通过 ExchangeViewModelAgent.ViewModel可以访问到兑换数据

*********************************************************************/
using UnityEngine;

public abstract class FViewModelAgent<T> : FViewModelAgentBase where T : FViewModel, new()
{
    private static FViewModelAgent<T> _instance;
    public static FViewModelAgent<T> instance
    {
        get
        {
            return _instance;
        }
    }

    public FViewModelAgent()
        : base(new T())
    {
        Debug.Log(_instance == null);
        _instance = this;
    }

    /// <summary>
    /// 获取数据流
    /// </summary>
    /// <returns></returns>
    static public T ViewModel
    {
        get
        {
            return instance._data as T;
        }
    }

    /// <summary>
    /// 可以在这里初始化或者注册event
    /// </summary>
    /// <returns></returns>
    protected override void OnInit()
    {
        //do nothing
    }
}

