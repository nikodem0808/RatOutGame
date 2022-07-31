using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController controller;
    AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        controller = this;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        src.volume = SceneMaster.sceneMaster.audioVolume.x * SceneMaster.sceneMaster.audioVolume.z;
    }

    public void PlaySFX(string _clipName)
    {
        src.PlayOneShot(SceneMaster.sfxData[_clipName]);
    }
}
