using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
 
    [SerializeField] GameObject _object;
    [SerializeField] float _blinkCycle;
    void Start() 
    {
        StartCoroutine(textBlink());
    }

    IEnumerator textBlink()
    {
        while (true)
        {
            _object.SetActive(false);
            yield return new WaitForSeconds(_blinkCycle / 2.0f);
            _object.SetActive(true);
            yield return new WaitForSeconds(_blinkCycle / 2.0f);
        }
    }
}
