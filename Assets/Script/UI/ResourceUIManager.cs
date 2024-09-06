using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ResourceUIManager : Singleton<ResourceUIManager>
{
    [SerializeField] TextMeshProUGUI[] textList;
    [SerializeField] GameObject[] upDownImages;

    // 이전 상태를 저장할 변수들
    private int previousHealth;
    private int previousMoney;
    private int previousEnvironment;
    private int previousCleanliness;
    private int previousHunger;

    private Coroutine[] imageHideCoroutines; // 이미지를 숨기기 위한 코루틴 배열

    void Start()
    {
        // 초기 상태 설정
        previousHealth = ResourceManager.Instance.Health;
        previousMoney = ResourceManager.Instance.Money;
        previousEnvironment = ResourceManager.Instance.Environment;
        previousCleanliness = ResourceManager.Instance.Cleanliness;
        previousHunger = ResourceManager.Instance.Hunger;

        imageHideCoroutines = new Coroutine[upDownImages.Length]; // 코루틴 배열 초기화
    }

    void Update()
    {
        // 현재 상태 가져오기
        int currentHealth = ResourceManager.Instance.Health;
        int currentMoney = ResourceManager.Instance.Money;
        int currentEnvironment = ResourceManager.Instance.Environment;
        int currentCleanliness = ResourceManager.Instance.Cleanliness;
        int currentHunger = ResourceManager.Instance.Hunger;

        // 텍스트 업데이트
        textList[0].text = currentHealth.ToString();
        textList[1].text = currentMoney.ToString();
        textList[2].text = currentEnvironment.ToString();
        textList[3].text = currentCleanliness.ToString();
        textList[4].text = currentHunger.ToString();

        // 변화가 생긴 경우에만 이미지 업데이트
        if (currentHealth != previousHealth)
        {
            SetImageState(0, currentHealth - previousHealth);
            previousHealth = currentHealth; // 이전 상태 업데이트
        }

        if (currentMoney != previousMoney)
        {
            SetImageState(1, currentMoney - previousMoney);
            previousMoney = currentMoney;
        }

        if (currentEnvironment != previousEnvironment)
        {
            SetImageState(2, currentEnvironment - previousEnvironment);
            previousEnvironment = currentEnvironment;
        }

        if (currentCleanliness != previousCleanliness)
        {
            SetImageState(3, currentCleanliness - previousCleanliness);
            previousCleanliness = currentCleanliness;
        }

        if (currentHunger != previousHunger)
        {
            SetImageState(4, currentHunger - previousHunger);
            previousHunger = currentHunger;
        }
    }

    void SetImageState(int index, float changeValue)
    {
        if (changeValue != 0)
        {
            upDownImages[index].SetActive(true);

            if (changeValue > 0)
            {
                upDownImages[index].GetComponent<Image>().color = Color.red;
                upDownImages[index].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                upDownImages[index].GetComponent<Image>().color = Color.blue;
                upDownImages[index].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }

            // 이전 코루틴이 실행 중이면 중지
            if (imageHideCoroutines[index] != null)
            {
                StopCoroutine(imageHideCoroutines[index]);
            }

            // 3초 뒤에 이미지를 비활성화하는 코루틴 시작
            imageHideCoroutines[index] = StartCoroutine(HideImageAfterDelay(index, 3f));
        }
    }

    IEnumerator HideImageAfterDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        upDownImages[index].SetActive(false);
    }
}
