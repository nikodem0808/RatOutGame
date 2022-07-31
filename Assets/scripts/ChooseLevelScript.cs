using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevelScript : MonoBehaviour
{
    public int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("Progress") + 1 < levelNumber) gameObject.SetActive(false);
    }

    public void Click()
    {
        SceneManager.LoadScene($"Level{levelNumber}");
    }
}
