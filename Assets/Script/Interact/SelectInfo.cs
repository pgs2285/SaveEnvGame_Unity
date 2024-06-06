using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInfo : MonoBehaviour
{
    ChangeOption changeOption;

    public void SetOption(ChangeOption changeOption)
    {
        this.changeOption = changeOption;
    }
    public void applyOption()
    {
        ResourceManager.Instance.UpdateHunger(changeOption.hungerChange);
        ResourceManager.Instance.UpdateCleanliness(changeOption.cleanlinessChange);
        ResourceManager.Instance.UpdateEnvironment(changeOption.environmentChange);
        ResourceManager.Instance.UpdateMoney(changeOption.moneyChange);
        ResourceManager.Instance.UpdateHealth(changeOption.healthChange);

        GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetControl(true);
        Destroy(gameObject.transform.parent.gameObject);
        GameObject.Find("ResourceIndicator").GetComponent<ResourceUIManager>().showChange(changeOption);
        
    }

}
