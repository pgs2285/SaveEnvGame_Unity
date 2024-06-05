using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class preventPlayerMovement : MonoBehaviour
{
    // �� gameObject�� Active�����ϋ��� player�� �̵��� �����մϴ�.
    private PlayerController playerController;
    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player")?.GetComponent<PlayerController>();
    }
    private void OnEnable() 
    {
        if(playerController != null)
        {
            playerController.SetControl(false);
        }
    }

    private void OnDisable()
    {
        if(playerController != null)
        {
            playerController.SetControl(true);
        }
    }
}
