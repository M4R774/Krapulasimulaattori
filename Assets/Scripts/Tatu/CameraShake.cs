using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// Sounds related to the camera shake
	[SerializeField] MessageManager messageManager;
	[SerializeField] string shakeReactionText;
	[Tooltip("The audio clip that will be played when the screen shake."), SerializeField]
	private AudioClip shakeReactionClip;
	private AudioSource _shakeReactionAudioSource;
	private int shakeCounter = 0;
	[Tooltip("The sound clip will play every nth occurence of the shake effect"), SerializeField]
	private int shakeInterval;

	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.04f;
	public float decreaseFactor = 1.0f;
    public float shakeCooldown = 0f;

	// Slower shaking
	Vector3 startPos;
	Vector3 endPos;
	Vector3 lastShakedPos; // Where the shake ended
	float timeElapsed;
	float lerpDuration = 0.05f;
	float valueToLerp;
    float startTime;
	Vector3 originalPos;

	// Shake reaction
	bool canTalk = true;

	// Reaction audio
    [SerializeField] AudioClip reactionClip;
    AudioSource _innerAudioSource;
	[SerializeField] protected List<AudioClip> audioClips;

    public bool IsShaking {
        get { return shakeDuration > 0; }
    }
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void Start()
	{
		_innerAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
		startTime = Time.time;
		startPos = camTransform.localPosition;
		endPos = Random.insideUnitSphere * shakeAmount;
		_shakeReactionAudioSource = GameObject.Find("PlayerAudioSource").GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{

		if (shakeDuration > 0 && shakeCooldown == 0f)
		{

			if(canTalk)
			{
				canTalk = false;
				//_innerAudioSource.PlayOneShot(reactionClip);
				messageManager.DisplayDialogueAndPlayAudio(shakeReactionText, audioClips);
			}
			if (timeElapsed < lerpDuration)
			{
				camTransform.localPosition = Vector3.Lerp(startPos, endPos, timeElapsed / lerpDuration);
				timeElapsed += Time.deltaTime;
				lastShakedPos = camTransform.localPosition;
			}
			else
			{
				timeElapsed = 0;
				startPos = camTransform.localPosition;
				endPos = Random.insideUnitSphere * shakeAmount;
			}

			//camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount; // Petteri's orig shake
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
            if (shakeDuration <= 0) {
                shakeDuration = 0f;
                SetCooldown();
            }
		}
		else
		{
			if(!canTalk)
			{
				canTalk = true;
			}
			shakeDuration = 0f;
			//camTransform.localPosition = originalPos;
			if (timeElapsed < lerpDuration)
			{
				camTransform.localPosition = Vector3.Lerp(lastShakedPos, originalPos, timeElapsed / lerpDuration);
				timeElapsed += Time.deltaTime;
			}
            if (shakeCooldown > 0) {
                shakeCooldown -= Time.deltaTime * decreaseFactor;
            } else {
                shakeCooldown = 0f;
            }
		}
	}

    void SetCooldown()
    {
        shakeCooldown = (float)UnityEngine.Random.Range(3, 8);
        playShakeReaction();
    }

    private void playShakeReaction()
    {
        shakeCounter++;
        if (shakeCounter == 0 || shakeCounter % shakeInterval == 0)
        {
           //_shakeReactionAudioSource.PlayOneShot(shakeReactionClip);
        }
    }
}
