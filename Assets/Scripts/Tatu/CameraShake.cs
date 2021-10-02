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
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0 && shakeCooldown == 0f)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
            if (shakeDuration <= 0) {
                SetCooldown();
            }
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
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
