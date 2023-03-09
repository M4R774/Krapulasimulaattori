using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FPSControllerLPFP;

public class BlackoutScreen : MonoBehaviour
{
    [SerializeField] public GameObject blackoutSquare;
    [SerializeField] public GameObject whiteoutSquare;

    // First voiceline
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _bgAudioSource;
    [Tooltip("The voice line which will play after the screen fades to light"), SerializeField]
    private AudioClip fadeFromBlackReactionClip;
    [SerializeField] string fadeFromBlackReactionText;
    [SerializeField] MessageManager messageManager;
    [SerializeField] protected List<AudioClip> audioClips;

    // Game Start logic
    [SerializeField] FpsControllerLPFP fpsController;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] Bed bed;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] GameObject pointerCanvas;
    [SerializeField] GameObject heart;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(FadeFromBlack());
        heart.SetActive(false);
    }

    private void Start()
    {
        if(_audioSource == null)
            _audioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
        bed.WakeUp();
    }

    public IEnumerator FadeFromBlack(float fadeSpeed = 0.15f) {
        
        float newFade;
        Color newColor;
        while (blackoutSquare.GetComponent<Image>().color.a > 0) {
            var color = blackoutSquare.GetComponent<Image>().color;
            newFade = color.a - (fadeSpeed * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            blackoutSquare.GetComponent<Image>().color = newColor;
            yield return null;
        }
        //_audioSource.PlayOneShot(fadeFromBlackReactionClip);
        messageManager.DisplayDialogueAndPlayAudio(fadeFromBlackReactionText, audioClips);
        _bgAudioSource.Play();
        yield return StartCoroutine(LightsOn());
    }

    public IEnumerator LightsOn(float fadeSpeed = 0.15f) {
        float newFade;
        Color newColor;
        whiteoutSquare.SetActive(true);
        while (whiteoutSquare.GetComponent<Image>().color.a < 0.9f) {
            var color = whiteoutSquare.GetComponent<Image>().color;
            newFade = color.a + (fadeSpeed * Time.deltaTime);
            newColor = new Color(color.r, color.g, color.b, newFade);
            whiteoutSquare.GetComponent<Image>().color = newColor;
            yield return null;
        }
        fpsController.canMove = true;
        fpsController.toggleInversion = true;
        cameraShake.enabled = true;
        pointerCanvas.SetActive(true);
        playerStatus.canPlayerMove = true;

        messageManager.DisplayNotification("GET TO WORK!\n      ");
        heart.SetActive(true);
        yield return null;
    }
}

