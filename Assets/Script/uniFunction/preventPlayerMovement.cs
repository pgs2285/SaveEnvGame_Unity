using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class preventPlayerMovement : MonoBehaviour
{
    // 이 gameObject가 Active상태일떄는 player의 이동을 방지합니다.
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
