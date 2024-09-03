using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using OpenAI;

public class GPTManager : Singleton<GPTManager>
{
    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();

    async void Start()
    {
        // 초기 시스템 메시지를 추가하여 모델에게 역할을 설정해줍니다.
        messages.Add(new ChatMessage { Role = "system", Content = "당신은 게임 속에서 사용자의 행동에 따라 수치를 조정하고 한 줄 평을 제공하는 역할을 합니다. 사용자가 입력하는 것은 음식, 이동수단, 행동, 물건 등 다양할 수 있으며, 그에 따라 다음의 수치를 조정하고 한 줄 평을 작성하세요: 돈, 체력, 환경수치, 청결도, 배고픔. 각 입력에 맞게 상황을 이해하고, 수치가 어떻게 변할지 결정하세요.\\n\\n- 입력 예시: \\\"샐러드\\\", \\\"햄버거\\\", \\\"버스\\\", \\\"도보\\\", \\\"자동차\\\", \\\"청소\\\", \\\"쇼핑\\\" 등.\\n- 음식: 사용자의 체력과 배고픔을 조정하세요. 건강한 음식은 체력을 증가시키고, 불건강한 음식은 체력을 감소시키세요. 또한, 음식의 환경 영향을 고려하여 환경수치도 조정하세요.\\n- 이동수단: 사용자가 선택한 이동수단에 따라 돈, 체력, 환경수치를 조정하세요. 예를 들어, 자동차는 체력을 덜 소모하지만 돈과 환경수치를 줄이고, 도보는 체력을 소모하지만 환경수치를 증가시킵니다.\\n- 행동: 사용자의 행동이 돈, 체력, 청결도 등에 어떤 영향을 미칠지 적절히 조정하세요.\\n\\n수치를 조정한 후, 그 선택에 대한 한 줄 평을 제공하세요. 예를 들어:\\n\\n1. 입력: \\\"샐러드\\\"\\n   - 체력: +2\\n   - 배고픔: -3\\n   - 환경수치: +1\\n   - 돈: -5\\n   - 한 줄 평: \\\"신선한 샐러드로 건강해졌다! 하지만 조금 비싸다.\\\"\\n\\n2. 입력: \\\"자동차\\\"\\n   - 체력: 0\\n   - 배고픔: 0\\n   - 환경수치: -10\\n   - 돈: -20\\n   - 청결도: -1\\n   - 한 줄 평: \\\"편리하게 목적지에 도착했다! 기름값이 많이 들었다...\\\"\\n\\n3. 입력: \\\"도보\\\"\\n   - 체력: -5\\n   - 배고픔: +2\\n   - 환경수치: +5\\n   - 돈: 0\\n   - 한 줄 평: \\\"걸어서 이동하며 운동했다! 조금 힘들지만 환경에는 좋은 선택이었다.\\\"\\n\\n사용자의 선택에 따라 적절하게 수치를 변경하고, 상황에 맞는 한 줄 평을 추가하세요.\\n" });

        await AskChatGPT("샐러드");

    }

    public string GetFeedback(string chatResponse)
    {
        // 피드백 부분만 추출하여 반환합니다.
        string[] lines = chatResponse.Split('\n');
        foreach (string line in lines)
        {
            if (line.Trim().StartsWith("- 한 줄 평:"))
            {
                return line.Replace("- 한 줄 평:", "").Trim();
            }
        }

        return "none";
    }
    
    public async Task<CreateChatCompletionResponse> AskChatGPT(string newText)
    {
        // 새로운 사용자 입력 메시지를 추가합니다.
        messages.Add(new ChatMessage { Role = "user", Content = newText });

        // 요청을 생성합니다.
        CreateChatCompletionRequest request = new CreateChatCompletionRequest
        {
            Messages = messages,
            Model = "gpt-3.5-turbo"
        };

        // GPT-3.5에게 요청을 보냅니다.
        var response = await openAI.CreateChatCompletion(request);
        string message = GetFeedback(response.Choices[0].Message.Content);
        Debug.Log(response.Choices[0].Message.Content);
        Debug.Log(message);
        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);  // 응답을 messages에 추가하여 대화의 흐름을 유지합니다.
        }

        return response;
    }
}