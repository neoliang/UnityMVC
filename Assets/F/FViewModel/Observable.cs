/********************************************************************
	created:	2018/12/05
	created:	5:12:2018   20:49
	author:		feefiliang
	
	purpose:	Observable的数据在变化后会发消息通知观察者，可以配合CUIView绑定UI显示与数据
    由于C#不能重载赋值构造函数，这里使用了Value属性
*********************************************************************/

using System;


    public abstract class IObservable<T> where T : struct
    {
        /// <summary>
        /// 定阅数据变化回调
        /// </summary>
        /// <param name="cb"></param>
        /// <returns>返回可以被Release的对象，当Release时，移除定阅</returns>
        public abstract IRelease Subscribe(Action<T> cb);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public abstract T GetValue();

        /// <summary>
        /// 转换到另外一种可以观察类型的数据
        /// </summary>
        /// <param name="caster">将数据T转为数据U</param>
        /// <returns>返回可观察类型U</returns>
        public abstract IObservable<U> CastTo<U>(Func<T, U> caster) where U : struct;


        /// <summary>
        /// 类型隐式转换 IObservable<T> ==> T
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator T(IObservable<T> d)
        {
            return d.GetValue();
        }
    }

//使用Event实现的可观察类型
public class Observable<T> : IObservable<T> where T : struct
{
    private T value;
    private FEvent<T> _fevent;

    public override T GetValue()
    {
        return Value;
    }
    public T Value
    {
        get { return value; }
        set
        {
            this.value = value;
            _fevent.BroadCastEvent(value);
        }
    }

    public override IRelease Subscribe(Action<T> cb)
    {
        return _fevent.Subscribe(cb);
    }
    public FEventRegister<T> Event
    {
        get { return _fevent; }
    }
    public Observable(T v = default(T), FEvent<T> outEvent = null)
    {
        _fevent = outEvent == null ? new FEvent<T>() : outEvent;
        value = v;
    }
    public override string ToString()
    {
        return value.ToString();
    }

    private class ReadOnlyObservale<U> : IObservable<U> where U : struct
    {
        private Func<U> _valueGeter;
        private FEventRegister<U> _fEventRegister;
        public override U GetValue()
        {
            return _valueGeter();
        }

        public override IRelease Subscribe(Action<U> cb)
        {
            return _fEventRegister.Subscribe(cb);
        }

        public override IObservable<W> CastTo<W>(Func<U, W> caster)
        {
            return new ReadOnlyObservale<W>(_fEventRegister.CastTo(caster), () => caster(this));
        }
        public ReadOnlyObservale(FEventRegister<U> fEventRegister, Func<U> valueGeter)
        {
            _fEventRegister = fEventRegister;
            _valueGeter = valueGeter;
        }
    }
    //类型转换，可以实现同一份数据，从不同的角度来观察
    override public IObservable<U> CastTo<U>(Func<T, U> caster)
    {
        return new ReadOnlyObservale<U>(Event.CastTo(caster), () => caster(this));
    }

    //创建可观察的数据
    public static IObservable<T> CreateObservable(FEventRegister<T> fEvent, Func<T> valueGeter)
    {
        return new ReadOnlyObservale<T>(fEvent, valueGeter);
    }
    //创建可观察的数据
    public static IObservable<T> CreateObservable(FEventRegisterBase fEvent, Func<T> valueGeter)
    {
        return CreateObservable(fEvent.CastDown().CastTo(valueGeter), valueGeter);
    }
}

