using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class TodoListViewModelAgent : FViewModelAgent<TodoListViewModel>
{
    FList<ToDoItem> _listItems;
    protected override void OnInit()
    {
        base.OnInit();
        _listItems = new FList<ToDoItem>();


        TodoListAction.AddToDoItem = AddToDoItem;
        TodoListAction.CompletedToDo = CompletedToDo;
        TodoListAction.DeleteToDoItem = DeleteToDoItem;
        TodoListAction.SetShowStyle = SetShowStyle;
    }

    public void  SetShowStyle(ToDoShowStyle style)
    {
        if (style != ViewModel.ShowStyle)
        {
            (ViewModel.ShowStyle as Observable<ToDoShowStyle>).Value = style;
            if(style == ToDoShowStyle.ShowUnCompletedOnly)
            {
                ViewModel.ListItems.RemoveAll(item => item.IsCompleted);
            }
            else if(style == ToDoShowStyle.ShowAll)
            {
                ViewModel.ListItems.Clear();
                ViewModel.ListItems.AddRange(_listItems);
            }
        }
    }  

    void AddToDoItem(string desc)
    {
        var item = new ToDoItem(desc);
        _listItems.Add(item);
        ViewModel.ListItems.Add(item);
    }
    void DeleteToDoItem(int id)
    {
        var item2remove = _listItems.Find(item => item.ID == id);
        bool removed = _listItems.Remove(item2remove);
        if (removed)
        {
            ViewModel.ListItems.Remove(item2remove);
        }
    }
    void CompletedToDo(int id, bool completed)
    {
        var tobeCompleted = _listItems.Find(item => item.ID == id);
        if (tobeCompleted != null)
        {
            tobeCompleted.SetCompleted(completed);
            if (ViewModel.ShowStyle == ToDoShowStyle.ShowUnCompletedOnly)
            {
                ViewModel.ListItems.Remove(tobeCompleted);
            }
        }
    }
}

