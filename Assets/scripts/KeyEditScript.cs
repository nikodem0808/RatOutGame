using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyEditScript : MonoBehaviour
{
    public static GameObject editDisplayText;
    public SaveButtonScript localSaveButton;
    public Text appendedButtonText;
    public string inputName;
    bool editingThisKey = false;
    // Start is called before the first frame update
    void Start()
    {
        appendedButtonText = GetComponentInChildren<Text>();
        appendedButtonText.text = ((KeyCode)PlayerPrefs.GetInt(inputName)).ToString();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (editingThisKey && Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0)) localSaveButton.saveIntsDict[inputName] = (int)KeyCode.Mouse0;
            if (Input.GetMouseButtonDown(1)) localSaveButton.saveIntsDict[inputName] = (int)KeyCode.Mouse1;
            if (Input.GetMouseButtonDown(2)) localSaveButton.saveIntsDict[inputName] = (int)KeyCode.Mouse2;
            if (Input.inputString != null && Input.inputString != "") localSaveButton.saveIntsDict[inputName] = (int)(KeyCode)(Input.inputString[0]);
            appendedButtonText.text = ((KeyCode)localSaveButton.saveIntsDict[inputName]).ToString();
            editDisplayText.SetActive(false);
            localSaveButton.gameObject.SetActive(true);
            editingThisKey = false;
            localSaveButton.valuesChanged = true;
        }
    }

    void OnEnable()
    {
        appendedButtonText ??= GetComponentInChildren<Text>();
        appendedButtonText.text = ((KeyCode)PlayerPrefs.GetInt(inputName)).ToString();
    }

    public void OnKeyEdit()
    {
        if (editDisplayText.activeSelf) return;
        editDisplayText.SetActive(true);
        localSaveButton.gameObject.SetActive(false);
        editingThisKey = true;
    }
}
