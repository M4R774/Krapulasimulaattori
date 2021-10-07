using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioTriggerer : MonoBehaviour
{
    [SerializeField] MessageManager messageManager;
    [SerializeField] string text;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    AudioClip[] clips;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            clips = audioClips.ToArray();
            messageManager.DisplayDialogueAndPlayAudio(text, clips);
        }
    }
}
