using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractObject, IInteractable
{
    bool isOpened = false;
    Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void LookAt(Transform transform) // track�� ui ��ġ
    {
        if (!isOpened)
        {
            showUI("���� ������ FŰ�� �����ּ���.", transform);
            return;
        }
        showUI("���� �������� FŰ�� �����ּ���.", transform);
    }

    public bool CanInteract()
    {

        float animTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animTime == 0 || animTime >= 1.0f) return true;
        return false;
    }

    public void Interact()
    {
        if (CanInteract())
        {
            switch (isOpened)
            {
                case false:
                    _animator.SetTrigger("DoorOpen");
                    isOpened = true;
                return;
                
                case true:
                    _animator.SetTrigger("DoorClose");
                    isOpened = false;
                return;
            }
        }
   
    }
}
