using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInteract : InteractObject, IInteractable
{
    [SerializeField] private string LookAtMessage;
    [SerializeField] private List<ChangeOption> selectOption = new List<ChangeOption>();

    PlayerController controller;
    void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }
    public bool CanInteract()
    {
        if(!controller.HasControl) // 컨트롤 할 권한이 없다면
        {
            return false;
        }

        return true;
    }

    public void Interact()
    {
        if(CanInteract())
        {
            controller.SetControl(false); // 카메라, 이동, 마우스가 풀림. 추후 버튼 클릭(선택)시 다시 true로 바꿔주는거 기억!
            //Instantiate // 내일구현.
        }
    }

    public void LookAt(Transform transform)
    {
        showUI(LookAtMessage, transform);
    }
}


