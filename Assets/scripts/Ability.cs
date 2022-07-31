using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public abstract class Ability : MonoBehaviour
{
    public float cooldown, localTimer;
    public Sprite abilityIcon;
    public string abilityName, abilityDisplayedName;
    // Start is called before the first frame update
    public virtual void Start()
    {
        abilityIcon = Resources.Load<Sprite>("Sprites/" + abilityName);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (localTimer > 0.0f)
        {
            localTimer -= Time.deltaTime;
            if (localTimer <= 0.0f && SceneMaster.sfxData.ContainsKey("Load")) SFXController.controller.PlaySFX("Load");
        }
    }

    public abstract void Trigger(bool keyUp);
}
