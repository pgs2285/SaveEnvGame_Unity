using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ResourceUIManager : Singleton<ResourceUIManager>
{
    [SerializeField] TextMeshProUGUI[] textList;
    [SerializeField] GameObject[] upDownImages;

    
    void Update()
    {
        textList[0].text = ResourceManager.Instance.Health.ToString();
        textList[1].text = ResourceManager.Instance.Money.ToString();
        textList[2].text = ResourceManager.Instance.Environment.ToString();
        textList[3].text = ResourceManager.Instance.Cleanliness.ToString();
        textList[4].text = ResourceManager.Instance.Hunger.ToString();

    }

    public void showChange(ChangeOption ChangeOption)
    {
        void SetImageState(int index, float changeValue)
        {
            if (changeValue > 0)
            {
                upDownImages[index].SetActive(true);
                upDownImages[index].GetComponent<Image>().color = Color.red;
                upDownImages[index].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (changeValue < 0)
            {
                upDownImages[index].SetActive(true);
                upDownImages[index].GetComponent<Image>().color = Color.blue;
                upDownImages[index].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            else
            {
                upDownImages[index].SetActive(false);
            }
        }

        SetImageState(0, ChangeOption.healthChange);
        SetImageState(1, ChangeOption.moneyChange);
        SetImageState(2, ChangeOption.environmentChange);
        SetImageState(3, ChangeOption.cleanlinessChange);
        SetImageState(4, ChangeOption.hungerChange);
    }
}
