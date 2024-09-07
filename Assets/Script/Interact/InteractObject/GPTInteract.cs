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
        if (!controller.HasControl) // 컨트롤 할 권한이 없다면
        {
            return false;
        }

        return true;
    }

    public void Interact()
    {
        if (CanInteract())
        {
            controller.SetControl(false); // 카메라, 이동, 마우스가 풀림. 추후 버튼 클릭(선택)시 다시 true로 바꿔주는거 기억!
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
