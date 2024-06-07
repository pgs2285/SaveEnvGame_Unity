using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{

    public Dictionary<int, bool> checkList = new Dictionary<int, bool>();
    [SerializeField] GameObject checkBoxPrefabs;

    // Update is called once per frame
    public void UpdateCheckList(bool isClear, int id)
    {
        checkList[id] = isClear;

    }
    public void AddTodoList(string text, int id)
    {
        GameObject todoList = Instantiate(checkBoxPrefabs);
        todoList.transform.parent = GameObject.FindWithTag("ToDo").GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform; // VerticalLayoutGroup에 있는거로 붙이기.
        todoList.GetComponentInChildren<TextMeshProUGUI>().text = text;
        todoList.name = "check_" + id.ToString();
        todoList.GetComponent<checklistToggle>().questID = id;
        if (todoList != null) checkList.Add(id, false); // todoList에 id와 함꼐 저장, 추후 체크할떄 사용
    }


}

