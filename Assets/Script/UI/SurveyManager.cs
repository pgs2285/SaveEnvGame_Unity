using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SurveyManager : MonoBehaviour
{
    public List<SurveyQuestion> surveyQuestions;
    public Text questionText;
    public ToggleGroup toggleGroup;
    public Toggle togglePrefab;
    public Button nextButton;
    public GameObject[] ansPos;

    [SerializeField] GameObject _quizPanel;
    private Animation _animation;

    private int currentQuestionIndex = 0;
    private int health = 50;
    private int money = 50;
    private int environment = 50;
    private int cleanliness = 50;
    private int hunger = 50;

    private void Awake()
    {
        _animation = _quizPanel.GetComponent<Animation>();
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
                    OnOptionSelected(option);
                }
            });
            i++;
        }
    }

    void OnOptionSelected(SurveyOption option)
    {
        // �⺻ �������� ���õ� ������(option)�� ��� ��ġ��ŭ �� ����
        health += option.healthChange;
        money += option.moneyChange;
        environment += option.environmentChange;
        cleanliness += option.cleanlinessChange;
        hunger += option.hungerChange;

        // �ִ�, �ּ� ���� 100������ ȯ��
        health = Mathf.Clamp(health, 0, 100);
        money = Mathf.Clamp(money, 0, 100);
        environment = Mathf.Clamp(environment, 0, 100);
        cleanliness = Mathf.Clamp(cleanliness, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    void OnNextButtonClicked()
    {
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
        //�����
        Debug.Log("Final Scores - Health: " + health + ", Money: " + money + ", Environment: " + environment + ", Cleanliness: " + cleanliness + ", Hunger: " + hunger);
    }

    public void ShowPanel()
    {
        _animation.Play();
    }
}
