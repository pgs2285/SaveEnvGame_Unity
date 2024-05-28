using System.Collections.Generic;
using UnityEngine;

public class SurveyInitializer : MonoBehaviour
{
    public SurveyManager surveyManager;

    void Awake()
    {
        surveyManager.surveyQuestions = new List<SurveyQuestion>
        {
            new SurveyQuestion
            {
                questionText = "�ָ��� � Ȱ���� ��ȣ�Ͻó���?",
                options = new List<SurveyOption>
                {
                    new SurveyOption { optionText = "�� �� Ʈ��ŷ", healthChange = 20, moneyChange = -10, environmentChange = 20, cleanlinessChange = -10, hungerChange = -10 },
                    new SurveyOption { optionText = "���θ����� ����", healthChange = -10, moneyChange = -30, environmentChange = -20, cleanlinessChange = 10, hungerChange = -10 },
                    new SurveyOption { optionText = "������ ��ȭ ����", healthChange = 10, moneyChange = 10, environmentChange = 10, cleanlinessChange = 10, hungerChange = -10 },
                    new SurveyOption { optionText = "ģ����� �ܽ�", healthChange = -10, moneyChange = -20, environmentChange = -10, cleanlinessChange = -10, hungerChange = 20 }
                }
            },
            new SurveyQuestion
            {
                questionText = "��� ����� ������ �ּ���.",
                options = new List<SurveyOption>
                {
                    new SurveyOption { optionText = "������ Ÿ��", healthChange = 20, moneyChange = 10, environmentChange = 20, cleanlinessChange = -20, hungerChange = -10 },
                    new SurveyOption { optionText = "���߱��� �̿��ϱ�", healthChange = 10, moneyChange = 0, environmentChange = 10, cleanlinessChange = 0, hungerChange = -10 },
                    new SurveyOption { optionText = "�ڰ��� �����ϱ�", healthChange = -10, moneyChange = -30, environmentChange = -30, cleanlinessChange = 10, hungerChange = -10 },
                    new SurveyOption { optionText = "������ �ȱ�", healthChange = 20, moneyChange = 20, environmentChange = 20, cleanlinessChange = -10, hungerChange = -20 }
                }
            },
            new SurveyQuestion
            {
                questionText = "���� �Ļ�� ������ ��ðڽ��ϱ�?",
                options = new List<SurveyOption>
                {
                    new SurveyOption { optionText = "��� ������", healthChange = 10, moneyChange = -10, environmentChange = 20, cleanlinessChange = 10, hungerChange = 10 },
                    new SurveyOption { optionText = "�ܹ��� ��Ʈ", healthChange = 10, moneyChange = -20, environmentChange = -20, cleanlinessChange = -10, hungerChange = 20 },
                    new SurveyOption { optionText = "���� ���ö�", healthChange = 10, moneyChange = 10, environmentChange = 10, cleanlinessChange = 20, hungerChange = 20 },
                    new SurveyOption { optionText = "�Ｎ ��ǰ", healthChange = -10, moneyChange = 10, environmentChange = -10, cleanlinessChange = -20, hungerChange = 10 }
                }
            },
            new SurveyQuestion
            {
                questionText = "�ް��� ������ �����?",
                options = new List<SurveyOption>
                {
                    new SurveyOption { optionText = "�ؿܿ���", healthChange = 10, moneyChange = -30, environmentChange = -20, cleanlinessChange = 20, hungerChange = -10 },
                    new SurveyOption { optionText = "ķ��", healthChange = 20, moneyChange = -20, environmentChange = 20, cleanlinessChange = -10, hungerChange = -10 },
                    new SurveyOption { optionText = "ȣ�ڿ��� ����", healthChange = 20, moneyChange = -30, environmentChange = -10, cleanlinessChange = 20, hungerChange = -10 },
                    new SurveyOption { optionText = "������ �޽�", healthChange = 10, moneyChange = 20, environmentChange = 10, cleanlinessChange = 10, hungerChange = 0 }
                }
            },
            new SurveyQuestion
            {
                questionText = "���� �� � ��ǰ�� �����Ͻðڽ��ϱ�?",
                options = new List<SurveyOption>
                {
                    new SurveyOption { optionText = "��Ȱ�� ���� �Ƿ�", healthChange = 0, moneyChange = -20, environmentChange = 30, cleanlinessChange = 10, hungerChange = 0 },
                    new SurveyOption { optionText = "�ֽ� ������ǰ", healthChange = 0, moneyChange = -30, environmentChange = -20, cleanlinessChange = 20, hungerChange = 0 },
                    new SurveyOption { optionText = "ģȯ�� �ķ�ǰ", healthChange = 10, moneyChange = -20, environmentChange = 20, cleanlinessChange = 0, hungerChange = 20 },
                    new SurveyOption { optionText = "�Ϲ� ������ǰ", healthChange = 0, moneyChange = -10, environmentChange = -10, cleanlinessChange = 10, hungerChange = 0 }
                }
            }
        };
    }
}
[System.Serializable]
public class SurveyOption  // �� �������� ���� ��ġ ��������
{
    public string optionText;
    public int healthChange;
    public int moneyChange;
    public int environmentChange;
    public int cleanlinessChange;
    public int hungerChange;
}

[System.Serializable]
public class SurveyQuestion // ������ ������
{
    public string questionText;
    public List<SurveyOption> options;
}
