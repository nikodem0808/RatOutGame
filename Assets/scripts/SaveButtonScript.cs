using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonScript : ButtonScript
{
    public Dictionary<string, float> saveFloatsDict;
    public Dictionary<string, int> saveIntsDict;
    public Dictionary<string, string> saveStringsDict;
    public bool valuesChanged;
    // Start is called before the first frame update
    void Start()
    {
        saveFloatsDict = new Dictionary<string, float>();
        saveIntsDict = new Dictionary<string, int>();
        saveStringsDict = new Dictionary<string, string>();
        valuesChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Click()
    {
        if (valuesChanged)
        {
            foreach (var i in saveFloatsDict.Keys)
            {
                PlayerPrefs.SetFloat(i, saveFloatsDict[i]);
            }
            foreach (var i in saveIntsDict.Keys)
            {
                PlayerPrefs.SetInt(i, saveIntsDict[i]);
            }
            foreach (var i in saveStringsDict.Keys)
            {
                PlayerPrefs.SetString(i, saveStringsDict[i]);
            }
            PlayerPrefs.Save();
        }
        valuesChanged = false;
        base.Click();
    }
}
