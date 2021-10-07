using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioTriggerer : MonoBehaviour
{
    [SerializeField] MessageManager messageManager;
    [SerializeField] string text;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            messageManager.DisplayDialogueAndPlayAudio(text, audioClips);
        }

        /*while (i < 10) // condition
        {
            Debug.Log("i = " + i);

            i++; // increment
        }*/
    }
}
