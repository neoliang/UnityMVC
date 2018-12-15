
/********************************************************************
	created:	2018/11/07
	created:	7:11:2018   11:14
	author:		feefiliang  
	
	purpose:	带有类型的Event
    相比于原来的Event有几个优点：
    1.定义的Event是带类型的,在定义Event时就定了Event的listener类型和触发者的类型，不会出现因为误操作而导致监听者类型和触发类型不一致，也不会漏传参数
    2.相比于字符串类型的Event，不但不会有两个相同的Event出现,而且可以减少字符串占用的空间
    3.只要事件定义了，监听的Delegate就存在，Broadcast时不用去判断Delegate是否存在
    4.使用Register的Subsribe返回IRelease可以实现自动移除监听消息函数，不用手工RemoveEventHander,减少出错的概率
    5.从多参数的Register可以转换到无参数的Register，因为有时候我们只关心事件是否发生，而不关心事件的参数
    6.在第5条的基础上，可以将不同类型Register转换为无参Register组合在一起，实现一次监听多个Event的消息
    7.相比于原来的Event每次触发调用不用再去查一次hash表
*********************************************************************/
using System;


public class FEvent : FEventRegister
{
    public void BroadCastEvent()
    {
        _BroadCastEvent();
    }
}

public class FEvent<T0> : FEventRegister<T0>
{
    public void BroadCastEvent(T0 arg0)
    {
        _BroadCastEvent(arg0);
    }
}



public class FEvent<T0, T1> : FEventRegister<T0, T1>
{
    public void BroadCastEvent(T0 arg0,T1 arg1)
    {
        _BroadCastEvent(arg0, arg1);
    }
}




public class FEvent<T0, T1, T2> : FEventRegister<T0, T1, T2>
{
    public void BroadCastEvent(T0 arg0, T1 arg1, T2 arg2)
    {
        _BroadCastEvent(arg0, arg1, arg2);
    }
}


public class FEvent<T0, T1, T2, T3> : FEventRegister<T0, T1, T2, T3>
{  
    public void BroadCastEvent(T0 arg0, T1 arg1, T2 arg2,T3 arg3)
    {
        _BroadCastEvent(arg0, arg1, arg2, arg3);
    }
}
