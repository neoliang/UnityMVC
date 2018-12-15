using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class TodoListAction
{
    public static Action<ToDoShowStyle> SetShowStyle;
    public static Action<string> AddToDoItem;
    public static Action<int> DeleteToDoItem;
    public static Action<int, bool>CompletedToDo;
}

