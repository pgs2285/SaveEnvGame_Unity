using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] List<string> _context = new List<string>();
    [SerializeField, Range(0.01f, 0.5f)]  private float _typeDelay;
    //[SerializeField] private float _nextContextTime = 2f;
    [SerializeField] private TextMeshProUGUI _textBox;
    [SerializeField] private GameObject _fingerSnapAnim;
    private string _fullText;
    private string _currentText = "";

    private int _stringIdx = 0;

    private SurveyManager surveyManager;

    private void Awake()
    {
        surveyManager = GetComponent<SurveyManager>();
    }
    private void Start()
    {
        try
        {
            StartCoroutine(typewrite(_stringIdx));
        }catch (Exception e)
        {
            Debug.Log("Text Index초과");
        }
        _stringIdx++;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Space"))
        {
            goToNextScript();
        }
    }

    public void goToNextScript()
    {
        StopAllCoroutines();
        string sceneName = SceneManager.GetActiveScene().name;
        if (_stringIdx == _context.Count)
        {
            switch (sceneName)
            {
                case "EnvSurvey": // scene name에 따라서 현재 Active상태 조절
                    _fingerSnapAnim.GetComponent<Animator>().gameObject.SetActive(true);
                    return;
                case "1.InMyHouse":
                    gameObject.SetActive(false);
                    return;
            }
        }
        if (_context[_stringIdx].Equals("-") && surveyManager != null)
        {
            _textBox.gameObject.SetActive(false);
            surveyManager.ShowPanel();
            _stringIdx++;
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

       // yield return new WaitForSeconds(_nextContextTime);
    }


}
