using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    private int D_stringIdx = 0;
    private SurveyManager surveyManager;

    [Header("Dialogue System일 시만 적용")]
    [SerializeField] GameObject _dialoguePref;

    private void Awake()
    {
        surveyManager = GetComponent<SurveyManager>();
    }
    private void Start()
    {
        if (_textBox == null) return;
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
        if(Input.GetButtonDown("Space") && _textBox != null) // textBox가 null인 경우가 하나 있는데, timeline에서 조정할 경우
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
                case "1.InMyHouse_ToonVersion":
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
        
    }
    public void setDialogueEffect()
    {
        StartCoroutine(dialogueEffect(D_stringIdx));
        D_stringIdx++; 
    }
    IEnumerator dialogueEffect(int stringIdx)
    {
        Image[] Images = GetComponentsInChildren<Image>();
        gameObject.GetComponent<RectTransform>().sizeDelta += new Vector2(0,60);


        foreach (Image image in Images)
        {
            
            
            Color tempColor = image.color;
            Color texttempColor = image.GetComponentInChildren<TextMeshProUGUI>().color;

            tempColor.a *= 0.5f;
            texttempColor.a *= 0.5f;
            
            image.color = tempColor;
            image.GetComponentInChildren<TextMeshProUGUI>().color = texttempColor;
        }
        GameObject dialogueBox = Instantiate(_dialoguePref);
        dialogueBox.transform.SetParent(gameObject.transform); // 하위 오브젝트로 등록합니다.

        int index = 0;
        _fullText = _context[stringIdx];
        while (_fullText.Length != _currentText.Length) // 길이가 같아질떄 까지
        {
            index++; // index 증가후
            _currentText = _fullText.Substring(0, index);
            dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = _currentText;
            yield return new WaitForSeconds(_typeDelay);
        }


    }
    public void DestroyChilds(Transform transform)
    {
        int childCount = transform.childCount;
        for(int i = childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
