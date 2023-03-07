using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Whiteout : MonoBehaviour
{   
    public bool isStrobeEffectActive;
    Coroutine strobeCoroutine;

    void OnEnable()
    {
        if(isStrobeEffectActive)
            strobeCoroutine = StartCoroutine(Intensify());
    }

    public IEnumerator Intensify()
    {
        float newFade;
        Color newColor;
        while (GetComponent<Image>().color.a < 1f)
        {
            var color = GetComponent<Image>().color;
            newFade = color.a + (0.5f * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return strobeCoroutine = StartCoroutine(Weaken());
    }

    public IEnumerator Weaken()
    {
        float newFade;
        Color newColor;
        while (GetComponent<Image>().color.a > 0.7f)
        {
            var color = GetComponent<Image>().color;
            newFade = color.a - (0.5f * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return strobeCoroutine = StartCoroutine(Intensify());
    }

    public void StopStrobingWhiteout()
    {
        if(strobeCoroutine != null)
        {
            Debug.Log("Stopped strobing");
            StopCoroutine(strobeCoroutine);
            strobeCoroutine = null;
            GetComponent<Image>().color = new Color(1, 1, 1, 0.7f);
        }
    }

    public void StartStrobingWhiteout()
    {
        if(gameObject.activeSelf && strobeCoroutine == null)
        {   
            Debug.Log("Started strobing");
            strobeCoroutine = StartCoroutine(Intensify());
        }

    }
}
