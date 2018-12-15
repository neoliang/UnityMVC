using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class FUIView : MonoBehaviour {

    private List<IRelease> _autoReleaseResources = null;

    protected virtual void OnViewInited()
    {
    }
    protected virtual void OnInit()
    {

    }
    private void Awake()
    {
        OnInit();
    }
    private void Start()
    {
        OnViewInited();
    }
    private void OnDestroy()
    {
        ProcessAutoRelease();
    }
    private void ProcessAutoRelease()
    {
        if (_autoReleaseResources != null)
        {
            for (int i = 0; i < _autoReleaseResources.Count; ++i)
            {
                _autoReleaseResources[i].Release();
            }
            _autoReleaseResources.Clear();
            _autoReleaseResources = null;
        }
    }
    protected void AutoRelease(IRelease e)
    {
        if (_autoReleaseResources == null)
        {
            _autoReleaseResources = new List<IRelease>();
        }
        _autoReleaseResources.Add(e);
    }

    /// <summary>
    /// 绑定文本
    /// </summary>
    /// <param name="text">view</param>
    /// <param name="data">data</param>
    /// <returns></returns>
    protected void BindText<U>(Text text, IObservable<U> data) where U : struct
    {
        Bind(data, v => text.text = v.ToString());
    }
    /// <summary>
    /// 绑定函数
    /// </summary>
    /// <param name="data">data</param>
    /// <param name="cb">数据发生变化后的回调</param>
    /// <returns></returns>
    protected void Bind<U>(IObservable<U> data, Action<U> cb) where U : struct
    {
        AutoRelease(data.Subscribe(cb));
        cb(data);
    }

    protected void AddEventHandler(FEventRegister register, Action cb)
    {
        AutoRelease(register.Subscribe(cb));
    }

    protected void AddEventHandler<T1>(FEventRegister<T1> register, Action<T1> cb)
    {
        AutoRelease(register.Subscribe(cb));
    }

    protected void AddEventHandler<T1, T2>(FEventRegister<T1, T2> register, Action<T1, T2> cb)
    {
        AutoRelease(register.Subscribe(cb));
    }

    protected void AddEventHandler<T1, T2, T3>(FEventRegister<T1, T2, T3> register, Action<T1, T2, T3> cb)
    {
        AutoRelease(register.Subscribe(cb));
    }

    protected void AddEventHandler<T1, T2, T3, T4>(FEventRegister<T1, T2, T3, T4> register, Action<T1, T2, T3, T4> cb)
    {
        AutoRelease(register.Subscribe(cb));
    }
}
