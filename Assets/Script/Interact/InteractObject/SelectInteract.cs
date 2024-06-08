using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectInteract : InteractObject, IInteractable
{
    [SerializeField] private string LookAtMessage;
    [SerializeField] private List<ChangeOption> selectOption = new List<ChangeOption>();
    [SerializeField] private GameObject SelectUI;
    [SerializeField] private GameObject SelectButton;

    [Header("Todo")]
    [SerializeField] private string todoString;
    [SerializeField] private int todoID;
    PlayerController controller;
    Transform TrackTarget;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        QuestManager.Instance.AddTodoList(todoString, todoID);
    }
    public bool CanInteract()
    {
        if(!controller.HasControl) // ��Ʈ�� �� ������ ���ٸ�
        {
            return false;
        }

        return true;
    }

    public void Interact()
    {
        if(CanInteract())
        {
            controller.SetControl(false); // ī�޶�, �̵�, ���콺�� Ǯ��. ���� ��ư Ŭ��(����)�� �ٽ� true�� �ٲ��ִ°� ���!
            GameObject ui = Instantiate(SelectUI);
            ui.transform.parent = GameObject.FindWithTag("Canvas").transform;
            ui.GetComponent<TrackUI>().Subject = TrackTarget;
            ui.GetComponent<TrackUI>().PlayerCamera = Camera.main;
            foreach(ChangeOption option in selectOption)
            {
                GameObject button = Instantiate(SelectButton);
                button.GetComponentInChildren<TextMeshProUGUI>().SetText(option.optionText);
                button.GetComponent<SelectInfo>().SetOption(option, todoID);
                button.transform.parent = ui.transform;

                
            }

        }
    }

    public void LookAt(Transform transform)
    {
        TrackTarget = transform;
        showUI(LookAtMessage, transform);
    }
}


