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
                questionText = "주말에 어떤 활동을 선호하시나요?",
                options = new List<ChangeOption>
                {
                    new ChangeOption { optionText = "숲 속 트레킹", healthChange = 20, moneyChange = -10, environmentChange = 20, cleanlinessChange = -10, hungerChange = -10 },
                    new ChangeOption { optionText = "쇼핑몰에서 쇼핑", healthChange = -10, moneyChange = -30, environmentChange = -20, cleanlinessChange = 10, hungerChange = -10 },
                    new ChangeOption { optionText = "집에서 영화 보기", healthChange = 10, moneyChange = 10, environmentChange = 10, cleanlinessChange = 10, hungerChange = -10 },
                    new ChangeOption { optionText = "친구들과 외식", healthChange = -10, moneyChange = -20, environmentChange = -10, cleanlinessChange = -10, hungerChange = 20 }
                }
            },
            new SurveyQuestion
            {
                questionText = "출근 방법을 선택해 주세요.",
                options = new List<ChangeOption>
                {
                    new ChangeOption { optionText = "자전거 타기", healthChange = 20, moneyChange = 10, environmentChange = 20, cleanlinessChange = -20, hungerChange = -10 },
                    new ChangeOption { optionText = "대중교통 이용하기", healthChange = 10, moneyChange = 0, environmentChange = 10, cleanlinessChange = 0, hungerChange = -10 },
                    new ChangeOption { optionText = "자가용 운전하기", healthChange = -10, moneyChange = -30, environmentChange = -30, cleanlinessChange = 10, hungerChange = -10 },
                    new ChangeOption { optionText = "도보로 걷기", healthChange = 20, moneyChange = 20, environmentChange = 20, cleanlinessChange = -10, hungerChange = -20 }
                }
            },
            new SurveyQuestion
            {
                questionText = "점심 식사로 무엇을 드시겠습니까?",
                options = new List<ChangeOption>
                {
                    new ChangeOption { optionText = "비건 샐러드", healthChange = 10, moneyChange = -10, environmentChange = 20, cleanlinessChange = 10, hungerChange = 10 },
                    new ChangeOption { optionText = "햄버거 세트", healthChange = 10, moneyChange = -20, environmentChange = -20, cleanlinessChange = -10, hungerChange = 20 },
                    new ChangeOption { optionText = "집밥 도시락", healthChange = 10, moneyChange = 10, environmentChange = 10, cleanlinessChange = 20, hungerChange = 20 },
                    new ChangeOption { optionText = "즉석 식품", healthChange = -10, moneyChange = 10, environmentChange = -10, cleanlinessChange = -20, hungerChange = 10 }
                }
            },
            new SurveyQuestion
            {
                questionText = "휴가를 보내는 방식은?",
                options = new List<ChangeOption>
                {
                    new ChangeOption { optionText = "해외여행", healthChange = 10, moneyChange = -30, environmentChange = -20, cleanlinessChange = 20, hungerChange = -10 },
                    new ChangeOption { optionText = "캠핑", healthChange = 20, moneyChange = -20, environmentChange = 20, cleanlinessChange = -10, hungerChange = -10 },
                    new ChangeOption { optionText = "호텔에서 쉬기", healthChange = 20, moneyChange = -30, environmentChange = -10, cleanlinessChange = 20, hungerChange = -10 },
                    new ChangeOption { optionText = "집에서 휴식", healthChange = 10, moneyChange = 20, environmentChange = 10, cleanlinessChange = 10, hungerChange = 0 }
                }
            },
            new SurveyQuestion
            {
                questionText = "다음 중 어떤 제품을 구매하시겠습니까?",
                options = new List<ChangeOption>
                {
                    new ChangeOption { optionText = "재활용 소재 의류", healthChange = 0, moneyChange = -20, environmentChange = 30, cleanlinessChange = 10, hungerChange = 0 },
                    new ChangeOption { optionText = "최신 전자제품", healthChange = 0, moneyChange = -30, environmentChange = -20, cleanlinessChange = 20, hungerChange = 0 },
                    new ChangeOption { optionText = "친환경 식료품", healthChange = 10, moneyChange = -20, environmentChange = 20, cleanlinessChange = 0, hungerChange = 20 },
                    new ChangeOption { optionText = "일반 가전제품", healthChange = 0, moneyChange = -10, environmentChange = -10, cleanlinessChange = 10, hungerChange = 0 }
                }
            }
        };
    }
}
[System.Serializable]
public class ChangeOption  // 각 선택지가 가질 수치 변동값들
{
    public string optionText;
    public int healthChange;
    public int moneyChange;
    public int environmentChange;
    public int cleanlinessChange;
    public int hungerChange;
    public List<string> SelectDialogue;
}



[System.Serializable]
public class SurveyQuestion // 질문과 선택지
{
    public string questionText;
    public List<ChangeOption> options;
}
