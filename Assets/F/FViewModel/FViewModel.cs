/********************************************************************
	created:	2018/12/04
	created:	4:12:2018   10:28
	author:		feefiliang
	
	purpose:	可以在外围系统展示的数据基类
*********************************************************************/


public abstract class FViewModel
{
    public void __PrivateInit()
    {
        OnInit();
    }
    /// <summary>
    /// 这里初始化成员变量等，当然也可以用构建函数，但是这里可以保证所有的PipeData已经全部创建完成
    /// </summary>
    /// <returns></returns>
    protected virtual void OnInit() { }       
}

