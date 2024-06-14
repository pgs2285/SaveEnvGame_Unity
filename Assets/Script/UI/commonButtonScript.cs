using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class commonButtonScript : MonoBehaviour
{
    [SerializeField] Text _answerText;
    public void SendToGpt()
    {
        GPTManager.Instance.AskChatGPT(_answerText.text);
    }
}
