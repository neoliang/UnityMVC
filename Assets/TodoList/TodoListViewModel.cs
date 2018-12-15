using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ToDoItem
{
    static int _autoId = 1;
    static int genID()
    {
        return _autoId++;
    }
    public int ID
    {
        get;
        private set;
    }
    public string Desc
    {
        get;
        private set;
    }
    public IObservable<bool> IsCompleted
    {
        get;
        private set;
    }
    public void SetCompleted(bool c)
    {
        (IsCompleted as Observable<bool>).Value = c;
    }
    public ToDoItem(string desc)
    {
        ID = genID();
        Desc = desc;
        IsCompleted = new Observable<bool>();
    }
}

public enum ToDoShowStyle
{
    ShowAll,
    ShowUnCompletedOnly,
}

public class TodoListViewModel : FViewModel
{
    FList<ToDoItem> _listItems;

    public IObservable< ToDoShowStyle> ShowStyle
    {
        get;
        private set;
    }
    public FList<ToDoItem> ListItems
    {
        get { return _listItems; }
    }
    
    protected override void OnInit()
    {
        FList<int> x = new List<int>();
        base.OnInit();
        _listItems = new FList<ToDoItem>();
        ShowStyle = new Observable<ToDoShowStyle>();
    }
}

