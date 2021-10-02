using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Used to switch something on/off
//

public class Switch : MonoBehaviour
{
    [SerializeField] bool isOn;
    [SerializeField] Renderer renderer;
    [SerializeField] Material onMaterial;
    [SerializeField] Material offMaterial;
    [SerializeField] GameObject objectToSwitch;

    void Awake()
    {
        SetMaterial();
        objectToSwitch.GetComponent<Item>().usable = isOn;
    }

    void Update()
    {
        
    }

    public void Use() // Here for now if needed later
    {
        SetMaterial();
		objectToSwitch.SendMessage("UseObject",SendMessageOptions.DontRequireReceiver);
        isOn = !isOn;
    }

    private void SetMaterial()
    {
        if(isOn)
            renderer.material = onMaterial;
        else
            renderer.material = offMaterial;
    }
}
