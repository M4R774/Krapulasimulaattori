using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTexture : MonoBehaviour
{
    [SerializeField] MeshRenderer quad;
    Coroutine countdownCoroutine = null;
    [Header("Countdown")]
    float numberOfFrames;
    [SerializeField] Texture[] countdownTexures;

    IEnumerator CountDown()
    {   
        int index = (int) numberOfFrames;
        quad.enabled = true;
        for (int i = 0; i < numberOfFrames; i++)
        {   
            index -= 1;
            quad.material.SetTexture("_MainTex", countdownTexures[index]);
            yield return new WaitForSeconds(1f);
        }
        // foreach (var number in countdownTexures)
        // {
        //     quad.material.SetTexture("_MainTex", number);
        //     yield return new WaitForSeconds(1f);
        // }
        quad.enabled = false;
        yield return null;
    }

    public void StartCountDown(float secondsCount)
    {
        if(countdownCoroutine == null)
        {   
            numberOfFrames = secondsCount;
            countdownCoroutine = StartCoroutine(CountDown());
        }
    }

}
