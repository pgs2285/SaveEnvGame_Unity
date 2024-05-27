using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] List<string> _context = new List<string>();
    [SerializeField, Range(0.01f, 0.5f)]  private float _typeDelay;
    [SerializeField] private float _nextContext = 2f;
    [SerializeField] private TextMeshProUGUI _textBox;
    private string _fullText;
    private string _currentText = "";

    private int _stringIdx = 0;

    private Quiz quiz;

    private void Awake()
    {
        quiz = GetComponent<Quiz>();
    }
    private void Start()
    {
        StartCoroutine(typewrite(_stringIdx));
        _stringIdx++;
    }

    public void goToNextScript()
    {
        StopAllCoroutines();
        if (_stringIdx == _context.Count && quiz != null)
        {
            _textBox.gameObject.SetActive(false);
            quiz.ShowPanel();
            return;
        }
        
        StartCoroutine(typewrite(_stringIdx));
        _stringIdx++;
    }
    
    IEnumerator typewrite(int stringIdx)
    {
        int index = 0;
        _fullText = _context[stringIdx];
        while (_fullText.Length != _currentText.Length) // 길이가 같아질떄 까지
        {
            index++; // index 증가후
            _currentText = _fullText.Substring(0, index);
            _textBox.SetText(_currentText);
            yield return new WaitForSeconds(_typeDelay);
        }

        yield return new WaitForSeconds(_nextContext);
    }
}
