using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class commonButtonScript : MonoBehaviour
{
    [SerializeField] Text _answerText;
    List<string> _answerList;
    int questID;
    ChangeOption changeOption;
    private void Awake()
    {
        changeOption = new ChangeOption();
    }
    public void SetOption(ChangeOption changeOption, int questID)
    {
        this.changeOption = changeOption;
        this.questID = questID;
    }
    public async void showAnswer()
    {
        // GPTManager에서 비동기 응답을 가져옴
        IAsyncEnumerable<CreateChatCompletionResponse> responses = GPTManager.Instance.AskChatGPT(_answerText.text);

        await foreach (var response in responses)
        {
            if (response.Choices != null && response.Choices.Count > 0)
            {
                var chatResponse = response.Choices[0].Message;
                Debug.Log(changeOption);

                

                changeOption.SelectDialogue.Add(chatResponse.Content);
               // GameObject.FindWithTag("Dialogue").GetComponent<TypeWriterEffect>().startDialogue(3.0f, changeOption.SelectDialogue);
            }
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetControl(true);
        GameObject.Find("ResourceIndicator").GetComponent<ResourceUIManager>().showChange(changeOption);
        QuestManager.Instance.UpdateCheckList(true, questID);
        GameObject.FindWithTag("Dialogue").GetComponent<TypeWriterEffect>().startDialogue(3.0f, changeOption.SelectDialogue);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
