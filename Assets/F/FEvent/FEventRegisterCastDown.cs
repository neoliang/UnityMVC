using System;
/********************************************************************
	created:	2018/11/07
	created:	7:11:2018   16:55
	author:		feefiliang
	
	purpose:    将Event向下类型转换，比如将 FEventRegister<T0> upper 转换为 FEventRegister
*********************************************************************/

public class FEventRegisterCastDown<T0> : FEventRegister
{
    FEventRegister<T0> _upper;
    private void BindAction(T0 arg0)
    {
        _BroadCastEvent();
    }
    protected override void OnLastListenerRemoved()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _upper.AddEventHandler(BindAction);
    }
    public FEventRegisterCastDown(FEventRegister<T0> upper)
    {
        _upper = upper;
    }
}

public class FEventRegisterCastTo<T0> : FEventRegister<T0>
{
    FEventRegister _inner;
    Func<T0> _castbridge;
    private void BindAction()
    {
        if (_castbridge != null)
        {
            _BroadCastEvent(_castbridge());
        }
    }
    protected override void OnLastListenerRemoved()
    {
        _inner.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _inner.AddEventHandler(BindAction);
    }

    public FEventRegisterCastTo(FEventRegister inner, Func<T0> castFun)
    {
        _inner = inner;
        _castbridge = castFun;
    }
}

public class FEventRegisterCastFromTo<T0, U0> : FEventRegister<U0>
{
    FEventRegister<T0> _inner;
    Func<T0, U0> _castbridge;
    private void BindAction(T0 arg0)
    {
        if (_castbridge != null)
        {
            _BroadCastEvent(_castbridge(arg0));
        }
    }
    protected override void OnLastListenerRemoved()
    {
        _inner.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _inner.AddEventHandler(BindAction);
    }
    public FEventRegisterCastFromTo(FEventRegister<T0> inner, Func<T0, U0> castFun)
    {
        _inner = inner;
        _castbridge = castFun;
    }
}

public class FEventRegisterCastDown<T0,T1> : FEventRegister
{
    FEventRegister<T0,T1> _upper;
    private void BindAction(T0 arg0,T1 arg1)
    {
        _BroadCastEvent();
    }
    protected override void OnLastListenerRemoved()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _upper.AddEventHandler(BindAction);
    }
    public FEventRegisterCastDown(FEventRegister<T0,T1> upper)
    {
        _upper = upper;
    }
}

//from FEventRegister<T0, T1> to FEventRegister<U>
public class FEventRegisterCastDownRemoveT_0_1<T0, T1,U0> : FEventRegister<U0>
{
    FEventRegister<T0, T1> _upper;
    Action<T0, T1, Action<U0>> _tranAction;
    private void BindAction(T0 arg0, T1 arg1)
    {
        if (_tranAction != null)
        {
            _tranAction(arg0, arg1, _BroadCastEvent);      
        }
    }
    public FEventRegisterCastDownRemoveT_0_1(FEventRegister<T0, T1> upper, Action<T0, T1, Action<U0>> tranAction)
    {
        _upper = upper;
        _tranAction = tranAction;
    }
    protected override void OnLastListenerRemoved()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _upper.AddEventHandler(BindAction);
    }
}

public class FEventRegisterCastDown<T0,T1,T2> : FEventRegister
{
    FEventRegister<T0, T1,T2> _upper;
    private void BindAction(T0 arg0, T1 arg1,T2 arg2)
    {
        _BroadCastEvent();
    }
    protected override void OnLastListenerRemoved()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _upper.AddEventHandler(BindAction);
    }
    public FEventRegisterCastDown(FEventRegister<T0, T1,T2> upper)
    {
        _upper = upper;
    }
}

public class FEventRegisterCastDown<T0, T1, T2,T3> : FEventRegister
{
    FEventRegister<T0,T1,T2,T3> _upper;
    private void BindAction(T0 arg0, T1 arg1, T2 arg2,T3 arg3)
    {
        _BroadCastEvent();
    }
    protected override void OnLastListenerRemoved()
    {
        _upper.RemoveEventHandler(BindAction);
        base.OnLastListenerRemoved();
    }
    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _upper.AddEventHandler(BindAction);
    }
    public FEventRegisterCastDown(FEventRegister<T0, T1, T2,T3> upper)
    {
        _upper = upper;
    }
}