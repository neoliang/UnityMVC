using UnityEngine;
using UnityEngine.UI;
public class TodoItemView : FUIView {

    [SerializeField] Text _desct;
    [SerializeField] Toggle _completedToggle;

    ToDoItem _item;
    void OnCompletedToggleClicked(bool isCompleted)
    {
        if(_item != null && isCompleted != _item.IsCompleted)
        {
           TodoListAction.CompletedToDo(_item.ID, isCompleted);
        }
    }
    protected override void OnInit()
    {
        base.OnInit();
        _completedToggle.onValueChanged.AddListener(OnCompletedToggleClicked);
    }
    void OnCompletedStateChanged(bool c)
    {
        _completedToggle.isOn = c;
    }
    public void SetItem(ToDoItem item)
    {
        _item = item;
        Bind(item.IsCompleted,OnCompletedStateChanged);
        _desct.text = _item.Desc;
    }
}
