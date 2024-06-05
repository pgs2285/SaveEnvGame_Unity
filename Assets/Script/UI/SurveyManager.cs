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
        // OnNextButtonClicked������ ���������� ������ ���������� DisplayQuestion�� ȣ��
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void DisplayQuestion()
    {
        // ���� ������Ʈ ����.
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
    { // �̱����� Resource Manager�� ����
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
            return; // ���õ� �ɼ��� ������ return 
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
