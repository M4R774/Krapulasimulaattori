using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Whiteout : MonoBehaviour
{
    public bool canUseEffect = true;
    void OnEnable()
    {
        if(canUseEffect)
            StartCoroutine(Intensify());
    }

    public IEnumerator Intensify() {
        float newFade;
        Color newColor;
        while (GetComponent<Image>().color.a < 1f) {
            var color = GetComponent<Image>().color;
            newFade = color.a + (0.5f * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return StartCoroutine(Weaken());
    }

    public IEnumerator Weaken() {
        float newFade;
        Color newColor;
        while (GetComponent<Image>().color.a > 0.7f) {
            var color = GetComponent<Image>().color;
            newFade = color.a - (0.5f * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            GetComponent<Image>().color = newColor;
            yield return null;
        }
        yield return StartCoroutine(Intensify());
    }

}
