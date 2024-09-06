using OpenAI;
using System.Collections.Generic;
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
        // GPTManager를 통해 응답을 받아옵니다.
        var response = await GPTManager.Instance.AskChatGPT(_answerText.text);

        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            Debug.Log(changeOption);

            // 응답 내용을 changeOption에 추가합니다.
            Debug.Log(GPTManager.Instance.GetStatusChanges(chatResponse.Content));
            changeOption.SelectDialogue.Add(GPTManager.Instance.GetFeedback(chatResponse.Content));
            changeOption.SelectDialogue.Add(GPTManager.Instance.GetStatusChanges(chatResponse.Content));

            // TypeWriterEffect를 통해 대화를 시작합니다.
            GameObject.FindWithTag("Dialogue").GetComponent<TypeWriterEffect>().startDialogue(4.0f, changeOption.SelectDialogue);
        }

        // PlayerController 및 ResourceUIManager를 업데이트합니다.
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetControl(true);
      //  GameObject.Find("ResourceIndicator").GetComponent<ResourceUIManager>().showChange(changeOption);
        QuestManager.Instance.UpdateCheckList(true, questID);

        // 버튼이 속한 부모 오브젝트를 파괴합니다.
        Destroy(gameObject.transform.parent.gameObject);
    }

}