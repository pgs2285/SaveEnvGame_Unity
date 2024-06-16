using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;

public class GPTManager : Singleton<GPTManager>
{
    private OpenAIApi openAI = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();
    

    public async IAsyncEnumerable<CreateChatCompletionResponse> AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        yield return await openAI.CreateChatCompletion(request); // 진행하기 이전에 이 함수가 끝나는 것이 보장되어야 하므로, await.

        //if(response.Choices != null && response.Choices.Count > 0)
        //{
        //    var chatResponse = response.Choices[0].Message;
        //    messages.Add(chatResponse);
        //    Debug.Log(chatResponse.Content);
        //}
         
        
    }
}
