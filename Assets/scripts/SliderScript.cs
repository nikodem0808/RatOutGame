using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public string valueName;
    public SaveButtonScript localSaveButton;
    public Slider sliderComponent;
    // Start is called before the first frame update
    void Start()
    {
        sliderComponent = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        sliderComponent ??= GetComponent<Slider>();
        sliderComponent.value = PlayerPrefs.GetFloat(valueName);
    }

    public void OnValueChange()
    {
        localSaveButton.valuesChanged = true;
        if (localSaveButton.saveFloatsDict.ContainsKey(valueName)) localSaveButton.saveFloatsDict[valueName] = sliderComponent.value;
        else localSaveButton.saveFloatsDict.Add(valueName, sliderComponent.value);
    }
}
