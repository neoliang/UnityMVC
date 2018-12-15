/********************************************************************
	created:	2018/11/07
	created:	7:11:2018   17:59
	author:		feefiliang
	
	purpose:	事件注册组合器，可以一次监听多个类型相同的事件
    比如 EventNameAndSexChanged = EventNameChanged + EventSexChanged
    EventNameAndSexChanged.AddEventHander可以同时监听EventNameChanged和EventSexChanged消息
    这个功能在实现红点时比较方便
*********************************************************************/
using System;

public class FEventRegisterComb : FEventRegister
{
    FEventRegister _left;
    FEventRegister _right;

    protected override void OnFirstListnerWillAdd()
    {
        base.OnFirstListnerWillAdd();
        _left.AddEventHandler(_BroadCastEvent);
        _right.AddEventHandler(_BroadCastEvent);
    }
    protected override void OnLastListenerRemoved()
    {
        _left.RemoveEventHandler(_BroadCastEvent);
        _right.RemoveEventHandler(_BroadCastEvent);
        base.OnLastListenerRemoved();
    }

    public FEventRegisterComb(FEventRegister left, FEventRegister right)
    {
        _left = left;
        _right = right;
    }
}