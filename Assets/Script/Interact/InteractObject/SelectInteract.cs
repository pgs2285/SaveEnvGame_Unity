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
            //Instantiate // ���ϱ���.
        }
    }

    public void LookAt(Transform transform)
    {
        showUI(LookAtMessage, transform);
    }
}


