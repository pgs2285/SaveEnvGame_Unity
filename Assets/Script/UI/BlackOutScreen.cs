using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BlackOutScreen : MonoBehaviour
{
    [SerializeField] GameObject BlackScreen;
    public void TurnOffScreen()
    {
        BlackScreen.SetActive(true);
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("1.InMyHouse");
    }
}
