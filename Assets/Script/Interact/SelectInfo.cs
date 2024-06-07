using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInfo : MonoBehaviour
{
    int questID;
    ChangeOption changeOption;
    public void SetOption(ChangeOption changeOption, int questID)
    {
        this.changeOption = changeOption;
        this.questID = questID;
    }
    public void applyOption()
    {
        ResourceManager.Instance.UpdateHunger(changeOption.hungerChange);
        ResourceManager.Instance.UpdateCleanliness(changeOption.cleanlinessChange);
        ResourceManager.Instance.UpdateEnvironment(changeOption.environmentChange);
        ResourceManager.Instance.UpdateMoney(changeOption.moneyChange);
        ResourceManager.Instance.UpdateHealth(changeOption.healthChange);

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetControl(true);
        GameObject.Find("ResourceIndicator").GetComponent<ResourceUIManager>().showChange(changeOption);
        QuestManager.Instance.UpdateCheckList(true, questID);
        Destroy(gameObject.transform.parent.gameObject);

    }

}
