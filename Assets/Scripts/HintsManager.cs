using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsManager : MonoBehaviour
{   
    bool isHintActive;
    [SerializeField] string GrabButton = "Grab";
    [SerializeField] Texture lightHintImage;
    [SerializeField] Texture shakingHintImage;
    [SerializeField] Texture inverseMovementHintImage;
    [SerializeField] Texture heartRateHintImage;
    [SerializeField] RawImage hintUIImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHintActive)
        {
            if(Input.anyKeyDown)
            {
                isHintActive = false;
                hintUIImage.gameObject.SetActive(false);
                hintUIImage.texture = null;
            }
        }
    }
    public void ShowHint(hints hint)
    {
        isHintActive = true;
        hintUIImage.gameObject.SetActive(true);
        if(hint == hints.lightHint)
            hintUIImage.texture = lightHintImage;
        else if(hint == hints.shakingHint)
            hintUIImage.texture = shakingHintImage;
        else if(hint == hints.invertedHint)
            hintUIImage.texture = inverseMovementHintImage;
        else if(hint == hints.heartRateHint)
            hintUIImage.texture = heartRateHintImage;
    }
}
