using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TodoListView : FUIView {

    [SerializeField] ViewComponent.FUIListView _list;
    [SerializeField] InputField _input;
    [SerializeField] Text _ShowStyleText;
    [SerializeField] Toggle toggle;
    protected override void OnViewInited()
    {
        base.OnViewInited();
        var listdata = TodoListViewModelAgent.ViewModel.ListItems;
        Bind(listdata.Count, c =>
        {
            _list.ElementCount = c;
            for(int i = 0;i<_list.ElementCount;++i)
            {
                var element = _list.GetElement(i);
                element.GetComponent<TodoItemView>().SetItem(listdata[i]);
            }
        });
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
        if(showAll)
        {
            TodoListAction.SetShowStyle(ToDoShowStyle.ShowAll);
        }
        else
        {
            TodoListAction.SetShowStyle(ToDoShowStyle.ShowUnCompletedOnly);
        }
    }
}
