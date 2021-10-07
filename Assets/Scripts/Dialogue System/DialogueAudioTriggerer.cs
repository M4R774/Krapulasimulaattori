using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioTriggerer : MonoBehaviour
{
    [SerializeField] MessageManager messageManager;
    [SerializeField] string text;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] audioID _audioID;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            messageManager.DisplayDialogueAndPlayAudioTest(text, _audioID);
        }
    }
}
