using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

//
// Manages displaying messages and dialogue on the in-game UI. Eg. "Picked up kitchen key".
// Uses itemDescription from Item class for the message.
//
// Contains some dead code, like checking for isAudioClipListEmpty
// This code is retained in case the future proves this system faulty and I want to use the older code
//
// TO DO:   Showing messages and dialogue at the same time seems messy, so let's find a way to not show both
// while retaining the ability to interrupt messages. It's important that in all cases the information delivered to the player.
//

public class MessageManager : MonoBehaviour
{
    public bool isActive = true;
    [Header("Messages")]
    [SerializeField] TextMeshProUGUI messageUI;
    [Header("Dialogue")]
    [SerializeField] TextMeshProUGUI dialogueUI;
    [Header("Notification")]
    [SerializeField] TextMeshProUGUI notificationUI;
    private Coroutine messageDisplayCoroutine;
    private string textReference; // current text stored
    [SerializeField] AudioSource audioSource;
    [SerializeField] Transform playerAudioSource;
    [SerializeField] List<AudioClip> placeholderForEmptyList;
    bool isAudioClipListEmpty;
    // this int enables us to not play audio when there is no corresponding clip to match the text
    int missingAudioClipHack = 100;

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
    public void DisplayNotification(string text)
    {
        if(messageDisplayCoroutine == null)
        {
            string message = text;
            textReference = text;
            messageDisplayCoroutine = StartCoroutine(TextDisplay(notificationUI, message));
        }
        // in case another message wants to interrupt the current one 
        if(messageDisplayCoroutine != null && text != textReference)
        {
            //StopCoroutine(messageDisplayCoroutine); // Stopping makes the dialogue screen stuck if there is one
            //messageDisplayCoroutine = null;
            string message = text;
            textReference = text;
            messageDisplayCoroutine = StartCoroutine(TextDisplay(notificationUI, message));
        }
    }

    // Functions to display just text
    public void DisplayDialogue(string text)
    {
        if(messageDisplayCoroutine == null)
        {
            messageDisplayCoroutine = StartCoroutine(TextDisplay(dialogueUI, text));
        }
    }
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

    // Functions to display text and to play audio
    public void DisplayDialogueAndPlayAudio(string text, List<AudioClip> clips)
    {
        if (!isActive) {
            return;
        }
        List<AudioClip> tempClips = new List<AudioClip>(clips); // This way we don't manipulate the list given as input
        if(messageDisplayCoroutine == null)
        {   
            // Let's check the input parameters to display dialogue properly
            if(tempClips.Count == 0 && !text.Contains("*"))
            {
                tempClips = placeholderForEmptyList;
                //isAudioClipListEmpty = true;
                missingAudioClipHack = 0;
            }
            else if(tempClips.Count == 0 && text.Contains("*"))
            {
                string[] phrases = text.Split('*');
                while(phrases.Length > tempClips.Count)
                {
                    tempClips.Add(placeholderForEmptyList[0]);
                }
                //isAudioClipListEmpty = true;
                missingAudioClipHack = 0;
            }
            else if(tempClips.Count != 0 && text.Contains("*"))
            {
                string[] phrases = text.Split('*');
                if(phrases.Length > tempClips.Count)
                {
                    missingAudioClipHack = tempClips.Count;
                    while(phrases.Length > tempClips.Count)
                    {
                        tempClips.Add(placeholderForEmptyList[0]);
                    }
                }
                else if(phrases.Length < tempClips.Count)
                {
                    while(phrases.Length < tempClips.Count)
                    {
                        tempClips.RemoveAt(tempClips.Count);
                    }
                }
            }
            messageDisplayCoroutine = StartCoroutine(TextDisplayAndAudioPlay(dialogueUI, text, tempClips));
        }
    }
    IEnumerator TextDisplayAndAudioPlay(TextMeshProUGUI ui, string text, List<AudioClip> audioClips)
    {
        // Debug.Log(missingAudioClipHack);
        float waitTime = 0.0f;
        if(text.Contains("*"))
        {
            string[] phrases = text.Split('*');
            for (int i = 0; i < phrases.Length; i++)
            {
                if(missingAudioClipHack > 0)
                {
                    //audioSource.PlayOneShot(audioClips[i]);
                    AudioSource.PlayClipAtPoint(audioClips[i], playerAudioSource.position);
                    GameObject.Find("One shot audio").transform.SetParent(playerAudioSource);
                }
                ui.text = phrases[i];
                if(missingAudioClipHack > 0)
                    waitTime = audioClips[i].length;
                else
                {
                    // for flow reasons wait time for dialogue phrases can be shorter than otherwise
                    if(phrases[i].Length < 10)
                    {
                        waitTime = phrases[i].Length * (0.10f * (10 / phrases[i].Length));
                    }
                    else
                    {
                        waitTime = phrases[i].Length * 0.10f;   
                    }
                }
                yield return new WaitForSeconds(waitTime);
                missingAudioClipHack--;
            }
        }
        else
        {
            if(!isAudioClipListEmpty && missingAudioClipHack > 0)
            {
                //audioSource.PlayOneShot(audioClips[0]);
                AudioSource.PlayClipAtPoint(audioClips[0], playerAudioSource.position);
                GameObject.Find("One shot audio").transform.SetParent(playerAudioSource);
            }
            ui.text = text;
            if(!isAudioClipListEmpty && missingAudioClipHack > 0)
                waitTime = audioClips[0].length;
            else
            {
                if(text.Length < 10)
                {
                    waitTime = text.Length * (0.10f * (10 / text.Length));
                }
                else
                {
                    waitTime = text.Length * 0.10f;   
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
        
        ui.text = "";
        textReference = null;
        messageDisplayCoroutine = null;
        isAudioClipListEmpty = false;
        missingAudioClipHack = 100;
        yield return null;
    }
}
