using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlackOutScreen : MonoBehaviour
{
    [SerializeField] GameObject BlackScreen;
    public void TurnOffScreen()
    {
        BlackScreen.SetActive(true);
    }
}
