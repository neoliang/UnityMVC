using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
class ToDoListEditorView : FUIView
{
    [SerializeField] InputField _input;
    [SerializeField] Text _ShowStyleText;
    [SerializeField] Toggle toggle;

    protected override void OnViewInited()
    {
        base.OnViewInited();
        Bind(TodoListViewModelAgent.ViewModel.ShowStyle, (s) =>
        {
            toggle.isOn = s == ToDoShowStyle.ShowAll;
            _ShowStyleText.text = s.ToString();
        });
        toggle.onValueChanged.AddListener(OnToggleShowAll);
    }
    public void OnAddItem()
    {
        TodoListAction.AddToDoItem(_input.text);
    }
    public void OnDeleteItem()
    {
        var list = TodoListViewModelAgent.ViewModel.ListItems;
        if (list.Count > 0)
        {
            TodoListAction.DeleteToDoItem(list[list.Count - 1].ID);
        }
    }
    public void OnToggleShowAll(bool showAll)
    {
        if (showAll)
        {
            TodoListAction.SetShowStyle(ToDoShowStyle.ShowAll);
        }
        else
        {
            TodoListAction.SetShowStyle(ToDoShowStyle.ShowUnCompletedOnly);
        }
    }
}

