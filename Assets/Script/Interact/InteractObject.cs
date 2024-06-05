using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _interactText;
    [SerializeField] GameObject _textBox;
    protected void showUI(string text, Transform trackObject)
    {
        
        _textBox.SetActive(true);
        _interactText.text = text;
        _textBox.GetComponent<TrackUI>().Subject = trackObject;
    }
}
