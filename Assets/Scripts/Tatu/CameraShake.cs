using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
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
	float lerpDuration = 0.35f;
	float valueToLerp;

    float startTime;
	
	Vector3 originalPos;

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
		startTime = Time.time;
		startPos = camTransform.localPosition;
		endPos = Random.insideUnitSphere * shakeAmount;
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{

		if (shakeDuration > 0 && shakeCooldown == 0f)
		{

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

    void SetCooldown() {
        shakeCooldown = (float)UnityEngine.Random.Range(3, 8);
    }
}
