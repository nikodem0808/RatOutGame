using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlatformAbility : Ability
{
    GameObject platform, _prefab;
    float range, minRange, maxRange, speedDelta;
    bool setUp, isHeld;
    // Start is called before the first frame update
    public override void Start()
    {
        _prefab = Resources.Load<GameObject>("Prefabs/ShadowPlatform");
        isHeld = false;
        setUp = false;
        range = 0.3f;
        platform = null;
        minRange = 0.3f;
        maxRange = 20.0f;
        speedDelta = 3.3333f;
        //
        abilityName = "ShadowPlatform";
        abilityDisplayedName = "Shadow Platform";
        cooldown = 30.0f;
        localTimer = 0.0f;
        //
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isHeld)
        {
            range = Mathf.Clamp(range + speedDelta * Time.deltaTime, minRange, maxRange);
            platform.transform.position = SceneMaster.sceneMaster.pMov.camPivot.transform.position + SceneMaster.sceneMaster.pMov.camPivot.transform.forward * range;
        }
        //
        base.Update();
    }

    public override void Trigger(bool keyUp)
    {
        if (localTimer > 0.0f) return;
        if (keyUp) {
            if (platform != null)
            {
                if (platform.GetComponent<Collider>().bounds.Intersects(SceneMaster.sceneMaster.pMov.GetComponent<Collider>().bounds))
                {
                    Destroy(platform);
                    platform = null;
                    setUp = false;
                    SFXController.controller.PlaySFX("Bone");
                }
                else
                {
                    SFXController.controller.PlaySFX("Stone");
                    platform.GetComponent<Collider>().enabled = true;
                }
                }
            isHeld = false;
            return;
        }
        if (setUp)
        {
            Destroy(platform);
            platform = null;
            setUp = false;
            SFXController.controller.PlaySFX("Bone");
            localTimer = cooldown;
            return;
        }
        range = minRange;
        platform = Instantiate(_prefab);
        SFXController.controller.PlaySFX("Splat");
        isHeld = true;
        setUp = true;
    }
}
