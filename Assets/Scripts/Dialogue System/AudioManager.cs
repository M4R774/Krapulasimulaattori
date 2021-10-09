using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Holds all audio clips that will be played alongside dialogue
//

public enum audioID
{
    intro,
    shaking,
    inverted
}
public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] List<AudioClip> introAudioClips;
    [SerializeField] List<AudioClip> shakingAudioClips;
    [SerializeField] List<AudioClip> invertedAudioClips;
    
    public List<AudioClip> audioClips(audioID id)
    {
        List<AudioClip> returnableAudioClips = new List<AudioClip>();

        switch(id)
        {
            case audioID.intro:
                Debug.Log("intro");
                returnableAudioClips = introAudioClips;
                return returnableAudioClips;
            case audioID.shaking:
                Debug.Log("shaking");
                returnableAudioClips = shakingAudioClips;
                return returnableAudioClips;
            case audioID.inverted:
                Debug.Log("inverted");
                returnableAudioClips = invertedAudioClips;
                return returnableAudioClips;
            default:
                Debug.Log("Unknown value");
                return null;
        }
    }
}
