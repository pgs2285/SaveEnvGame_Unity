using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private GameObject _QuizPanel;
    private Animation animation;

    private void Awake()
    {
        animation = _QuizPanel.GetComponent<Animation>();
    }

    public void ShowPanel()
    {
        animation.Play();
    }
}
