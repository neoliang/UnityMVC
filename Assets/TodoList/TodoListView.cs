using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TodoListView : FUIView {

    [SerializeField] ViewComponent.FUIListView _list;

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
    }
}
