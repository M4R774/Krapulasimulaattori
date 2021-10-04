using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//
// Manages displaying messages and dialogue on the in-game UI. Eg. "Picked up kitchen key".
// Uses itemDescription from Item class for the message.
// TO DO:   Showing messages and dialogue at the same time seems messy, so let's find a way to not show both
// while retaining the ability to interrupt messages. It's important that in all cases the information delivered to the player.
//

public class MessageManager : MonoBehaviour
{
    [Header("Messages")]
    [SerializeField] TextMeshProUGUI messageUI;
    [Header("Dialogue")]
    [SerializeField] TextMeshProUGUI dialogueUI;
    private Coroutine messageDisplayCoroutine;
    private string textReference; // current text stored
    [SerializeField] AudioSource audioSource;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            DisplayDialogue("Aah.");
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            DisplayPickUpMessage("kitchen key");
        }
    }

    // The method is complicated to make sure that the coroutine is only started once
    // even if this method would be called multiple times
    public void DisplayPickUpMessage(string text)
    {
        if(messageDisplayCoroutine == null)
        {
            string message = "Picked up " + text + ".";
            textReference = text;
            messageDisplayCoroutine = StartCoroutine(TextDisplay(messageUI, message));
        }
        // in case another message wants to interrupt the current one 
        if(messageDisplayCoroutine != null && text != textReference)
        {
            //StopCoroutine(messageDisplayCoroutine); // Stopping makes the dialogue screen stuck if there is one
            //messageDisplayCoroutine = null;
            string message = "Picked up " + text + ".";
            textReference = text;
            messageDisplayCoroutine = StartCoroutine(TextDisplay(messageUI, message));
        }
    }
    public void DisplayDialogue(string text)
    {
        if(messageDisplayCoroutine == null)
        {
            messageDisplayCoroutine = StartCoroutine(TextDisplay(dialogueUI, text));
        }
    }

    // TO DO
    // Modify this coroutine to expect a list of audio clips
    IEnumerator TextDisplay(TextMeshProUGUI ui, string text)
    {
        if(text.Contains("*"))
        {
            string[] phrases = text.Split('*');
            for (int i = 0; i < phrases.Length; i++)
            {
                ui.text = phrases[i];
                // for flow reasons wait time for dialogue phrases can be shorter than otherwise
                float waitTime = 0.0f;
                if(phrases[i].Length < 10)
                {
                    waitTime = phrases[i].Length * (0.10f * (10 / phrases[i].Length));
                }
                else
                {
                    waitTime = phrases[i].Length * 0.10f;   
                }
                yield return new WaitForSeconds(waitTime);
            }
        }
        else
        {
            ui.text = text;
            float waitTime = text.Length * 0.15f;
            yield return new WaitForSeconds(waitTime);
        }
        
        ui.text = "";
        textReference = null;
        messageDisplayCoroutine = null;
        yield return null;
    }

    public void DisplayDialogueAndPlayAudio(string text, AudioClip[] clips)
    {
        if(messageDisplayCoroutine == null)
        {
            messageDisplayCoroutine = StartCoroutine(TextDisplayAndAudioPlay(dialogueUI, text, clips));
        }
    }
    IEnumerator TextDisplayAndAudioPlay(TextMeshProUGUI ui, string text, AudioClip[] audioClips)
    {
        if(text.Contains("*"))
        {
            string[] phrases = text.Split('*');
            for (int i = 0; i < phrases.Length; i++)
            {
                audioSource.PlayOneShot(audioClips[i]);
                ui.text = phrases[i];
                // for flow reasons wait time for dialogue phrases can be shorter than otherwise
                float waitTime = 0.0f;
                /*if(phrases[i].Length < 10)
                {
                    waitTime = phrases[i].Length * (0.10f * (10 / phrases[i].Length));
                }
                else
                {
                    waitTime = phrases[i].Length * 0.10f;   
                }*/
                waitTime = audioClips[i].length;
                Debug.Log(audioClips[i].length);
                yield return new WaitForSeconds(waitTime);
                //yield return null;
            }
        }
        else
        {
            audioSource.PlayOneShot(audioClips[0]);
            ui.text = text;
            /*float waitTime = text.Length * 0.15f;
            //yield return new WaitForSeconds(waitTime);
            while(audioSource.isPlaying)
                ui.text = text;
            //yield return new WaitForSeconds(waitTime);
            yield return null;*/
            float waitTime = audioClips[0].length;
            yield return new WaitForSeconds(waitTime);
        }
        
        ui.text = "";
        textReference = null;
        messageDisplayCoroutine = null;
        yield return null;
    }
}
