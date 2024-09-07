using OpenAI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
public class GPTInteract : InteractObject, IInteractable
{
    PlayerController controller;
    Transform TrackTarget;
    [SerializeField] private string LookAtMessage;
    [SerializeField] GameObject GptPanel;
    private List<ChatMessage> messages = new List<ChatMessage>();
    [Header("Todo")]
    [SerializeField] private string todoString;
    [SerializeField] private int todoID;

    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        QuestManager.Instance.AddTodoList(todoString, todoID);
    }
    public bool CanInteract()
    {
        if (!controller.HasControl) // ��Ʈ�� �� ������ ���ٸ�
        {
            return false;
        }

        return true;
    }

    public void Interact()
    {
        if (CanInteract())
        {
            controller.SetControl(false); // ī�޶�, �̵�, ���콺�� Ǯ��. ���� ��ư Ŭ��(����)�� �ٽ� true�� �ٲ��ִ°� ���!
            GameObject ui = Instantiate(GptPanel);
            ui.transform.parent = GameObject.FindWithTag("Canvas").transform;
            ui.GetComponent<TrackUI>().Subject = TrackTarget;
            ui.GetComponent<TrackUI>().PlayerCamera = Camera.main;
            QuestManager.Instance.UpdateCheckList(true, todoID);

        }
    }

    public void LookAt(Transform transform)
    {
        TrackTarget = transform;
        showUI(LookAtMessage, transform);
    }

    


}
