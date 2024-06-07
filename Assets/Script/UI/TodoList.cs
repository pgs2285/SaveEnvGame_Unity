using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TodoList : MonoBehaviour
{
    bool isScrollOpened = false;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        List<int> keys = QuestManager.Instance.checkList.Keys.ToList<int>();
        foreach (int key in keys)
        {
            if(QuestManager.Instance.checkList[key])
            {
                for(int i = 0; i < this.transform.childCount; i++)
                {
                    checklistToggle _checklistToggle = this.transform.GetChild(i).GetComponent<checklistToggle>(); // 여기서 id가져온후 비교하고 true면 active , false면 deactive
                    
                    if(_checklistToggle?.questID == key)
                    {
                        _checklistToggle.Check.SetActive(QuestManager.Instance.checkList[key]);
                    }
                }
            }
        }
    }


    public void scrollStateChange()
    {
        isScrollOpened = !isScrollOpened;
        animator.SetBool("IsOpened", isScrollOpened);
    }
}
