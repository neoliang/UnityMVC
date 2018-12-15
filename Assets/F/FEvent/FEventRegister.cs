/********************************************************************
	created:	2018/11/07
	created:	7:11:2018   17:56
	author:		feefiliang
	
	purpose:	//事件注册器，提供事件注册和订阅
    //Event本身就是一个全局的注册器，除此之外，还可以调用BroadCastEvent来触发事件,
    //C#不支持变长模板参数，暂时支持0~4个参数的注册器
*********************************************************************/
using System;

//IDisposable可以和using一起使用，一般是用于释放非托管资源的，为了防止误，单独定义一个释放接口，
//关于IDisposable的讨论请参考 https://www.zhihu.com/question/51592470
public interface IRelease
{
    void Release();
}

//所有Event的基类，实现了一些通用的方法
public abstract class FEventRegisterBase 
{
    protected  Delegate _delegate;


    /// <summary>
    /// 转为新的无参事件注册器，所有事件注册器都可以转为无参数的，因为有时候我们只关心事件是否发生，而不关心事件的参数
    /// </summary>
    /// <returns></returns>
    public abstract FEventRegister CastDown();

    /// <summary>
    /// 最后一个监听者被移除时触发，此时Delegate非空变为空
    /// </summary>
    /// <returns></returns>
    protected virtual void OnLastListenerRemoved() { }

    /// <summary>
    /// 第一个监听者被添加前触发,此时Delegate从空变为非空
    /// </summary>
    /// <returns></returns>
    protected virtual void OnFirstListnerWillAdd() { }

    
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    protected void _AddEventHandler(Delegate d)
    {
        if(_delegate == null)
        {
            OnFirstListnerWillAdd();
        }
        _delegate = Delegate.Combine(_delegate, d);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    protected void _RemoveEventHandler(Delegate d)
    {
        _delegate = Delegate.RemoveAll(_delegate, d);
        if(_delegate == null)
        {
            OnLastListenerRemoved();
        }
    }

    /// <summary>
    /// 订阅监听,订阅的监听不用remove,需要执行返回接口的Release。该功能可以方便统一管理，实现自动移除监听
    /// </summary>
    /// <param name="cb"></param>
    /// <returns></returns>
    protected IRelease _Subscribe(Delegate cb)
    {
        _AddEventHandler(cb);
        return new HandlerRemover(this, cb);
    }
    class HandlerRemover : IRelease
    {
        FEventRegisterBase _soruce;
        Delegate _value;
        public HandlerRemover(FEventRegisterBase soruce, Delegate value)
        {
            _soruce = soruce;
            _value = value;
        }
        void IRelease.Release()
        {
            _soruce._RemoveEventHandler(_value);
        }
    }

    /// <summary>
    /// 多个有参或者无参EventRegister合并成一个无参数的FEventRegister，方便一次监听多个消息
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    static public FEventRegister operator +(FEventRegisterBase left, FEventRegisterBase right)
    {
        return left.CastDown() + right.CastDown();
    }
}

public abstract class FEventRegister : FEventRegisterBase
{
    public void AddEventHandler(Action cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action cb)
    {
        _RemoveEventHandler(cb);
    }

    //定阅事件
    public IRelease Subscribe(Action cb)
    {
        return _Subscribe(cb);
    }

    public override FEventRegister CastDown()
    {
        return this;
    }

    /// <summary>
    /// 注册器不应该拥有广播事件的功能，把这个方法提取到这里是为减少重复代码，方便多个子类复用
    /// </summary>
    /// <returns></returns>
    protected void _BroadCastEvent()
    {
        if (_delegate != null)
        {
            (_delegate as Action)();
        }
    }


    /// <summary>
    /// 将多个无参数的EventRegister合并到一起，从而实现一次监听多个事件是合理的
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    static public FEventRegister operator +(FEventRegister left, FEventRegister right)
    {
        return new FEventRegisterComb(left, right);
    }

    /// <summary>
    /// 将无参数的注册器转换为有参数的注册器，在实现红点的功能的时候比较有用
    /// 比如一个红点关心的是多个事件是否发生，在发生之后一般会调用一个函数HasRedDot去判断是否要显示红点
    /// 例如大厅的一个按钮在有新任务或者有奖励可领取时要显示红点，对应的事件分别为 TaskChanged 和 RewardChanged ,我们只需要这样做就可以：
    /// var LobbyReddotEvent = (TaskChanged + RewardChanged).CastTo(()=> HasNewTask() || HasNewRewards)
    /// BindReddot(reddotGameObject,LobbyReddotEvent)
    /// </summary>
    /// <param name="castCall">Event发生时参数</param>
    /// <returns></returns>
    public FEventRegister<T0> CastTo<T0>(Func<T0> castCall)
    {
        return new FEventRegisterCastTo<T0>(this, castCall);
    }
}

//一个参数
public abstract class FEventRegister<T0> : FEventRegisterBase
{

    public void AddEventHandler(Action<T0> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0> cb)
    {
        _RemoveEventHandler(cb);
    }
    //定阅事件
    public IRelease Subscribe(Action<T0> cb)
    {
        return _Subscribe(cb);
    }
    protected void _BroadCastEvent(T0 arg0)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0>)(arg0);
        }
    }

    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0>(this);
    }

    /// <summary>
    /// 将T0参数的注册器转换到U0参数的注册器，可以实现一个数据变动触发多个事件
    /// 比如玩家拥有的金币数量，当数量变化时会触发有一个UInt参数(表示金币的数量)CoinChanged事件,我们希望玩家的金币可以够买商品时显示红点，那么我们可以这样做
    /// var CanBuyAny = CoinChanged.CastTo( n => IsCoinEnoughToBuyAny(n))
    /// BindReddot(reddotGameObject,CanBuyAny)
    /// </summary>
    /// <param name="castCall">函数：将事件A参数转换为事件B参数</param>
    /// <returns></returns>
    public FEventRegister<U0> CastTo<U0>(Func<T0,U0> castCall)
    {
        return new FEventRegisterCastFromTo<T0,U0>(this, castCall);
    }
}


//两个参数
public abstract class FEventRegister<T0, T1> : FEventRegisterBase
{

    public void AddEventHandler(Action<T0, T1> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1> cb)
    {
        _RemoveEventHandler(cb);
    }

    //定阅事件
    public IRelease Subscribe(Action<T0, T1> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1>)(arg0, arg1);
        }
    }

    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0,T1>(this);
    }
}

//三个参数
public abstract class FEventRegister<T0, T1, T2> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0, T1, T2> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1, T2> cb)
    {
        _RemoveEventHandler(cb);
    }

    //定阅事件
    public IRelease Subscribe(Action<T0, T1, T2> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1, T2 arg2)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1, T2>)(arg0, arg1, arg2);
        }
    }

    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0,T1,T2>(this);
    }
}

//四个参数
public abstract class FEventRegister<T0, T1, T2, T3> : FEventRegisterBase
{
    public void AddEventHandler(Action<T0, T1, T2, T3> cb)
    {
        _AddEventHandler(cb);
    }
    public void RemoveEventHandler(Action<T0, T1, T2, T3> cb)
    {
        _RemoveEventHandler(cb);
    }

    //定阅事件
    public IRelease Subscribe(Action<T0, T1, T2, T3> cb)
    {
        return _Subscribe(cb);
    }

    protected void _BroadCastEvent(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        if (_delegate != null)
        {
            (_delegate as Action<T0, T1, T2, T3>)(arg0, arg1, arg2, arg3);
        }
    }
    public override FEventRegister CastDown()
    {
        return new FEventRegisterCastDown<T0, T1, T2,T3>(this);
    }
}