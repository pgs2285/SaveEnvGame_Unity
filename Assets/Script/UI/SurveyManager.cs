using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SurveyManager : MonoBehaviour
{
    public List<SurveyQuestion> surveyQuestions;
    public Text questionText;
    public ToggleGroup toggleGroup;
    public Toggle togglePrefab;
    public Button nextButton;
    public GameObject[] ansPos;

    [SerializeField] GameObject _quizPanel;
    [SerializeField] GameObject _txtPanel;
    private Animation _animation;

    private ChangeOption _selectedOption;
    private ResorurceDisplay resourceDisplay;
    private int currentQuestionIndex = 0;


    private void Awake()
    {
        _animation = _quizPanel.GetComponent<Animation>();
        resourceDisplay = GetComponent<ResorurceDisplay>();
    }
    void Start()
    {
        DisplayQuestion();
        // OnNextButtonClicked에서는 설문조사의 개수가 끝날때까지 DisplayQuestion을 호출
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void DisplayQuestion()
    {
        // 하위 오브젝트 제거.
        foreach (GameObject ans in ansPos)
        {
            try
            {
                Destroy(ans.transform.GetChild(0).gameObject);
            } catch { }
        }

        SurveyQuestion question = surveyQuestions[currentQuestionIndex];
        questionText.text = question.questionText;
        int i = 0;
        foreach (var option in question.options)
        {
            
            Toggle toggle = Instantiate(togglePrefab, ansPos[i].transform);
            toggle.GetComponentInChildren<Text>().text = option.optionText;
            toggle.group = toggleGroup;
            toggle.onValueChanged.AddListener((isSelected) =>
            {
                if (isSelected)
                {
                    _selectedOption = option;
                }
            });
            i++;
        }
    }

    void UpdateResources(ChangeOption option)
    { // 싱글톤인 Resource Manager에 접근
        ResourceManager.Instance.UpdateHealth(option.healthChange);
        ResourceManager.Instance.UpdateMoney(option.moneyChange);
        ResourceManager.Instance.UpdateEnvironment(option.environmentChange);
        ResourceManager.Instance.UpdateCleanliness(option.cleanlinessChange);
        ResourceManager.Instance.UpdateHunger(option.hungerChange);
        Debug.Log($"Updated Resources - Health: {ResourceManager.Instance.Health}, Money: {ResourceManager.Instance.Money}, Environment: {ResourceManager.Instance.Environment}, Cleanliness: {ResourceManager.Instance.Cleanliness}, Hunger: {ResourceManager.Instance.Hunger}");
    }

    void OnNextButtonClicked()
    {
        if(_selectedOption != null)
        {
            UpdateResources(_selectedOption);
            resourceDisplay.showChange(_selectedOption);
            _selectedOption = null;


        }
        else
        {
            return; // 선택된 옵션이 없으면 return 
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < surveyQuestions.Count)
        {
            DisplayQuestion();
        }
        else
        {
            EndSurvey();
        }
    }

    void EndSurvey()
    {

        ShowDownPanel();
    }

    public void ShowPanel()
    {
        _animation.Play("PanelAppear");

    }
    public void ShowDownPanel()
    {
        _animation.Play("FadeAwayQuizPanel");
        Destroy(_quizPanel, 2f);
        _txtPanel.SetActive(true);
        _txtPanel.GetComponent<TextMeshProUGUI>().text = ".";
    }

}
