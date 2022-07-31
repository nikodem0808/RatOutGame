using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneMaster;

public sealed class StrayShadowAbility : Ability
{
    public bool strayPresent = false, phasePause = false, sa = false;
    GameObject strayShadow;
    Vector3 pv, lmov;
    float dbclickTimer;
    // Start is called before the first frame update
    public override void Start()
    {
        strayPresent = false;
        phasePause = false;
        strayShadow = Resources.Load<GameObject>("Prefabs/StrayShadow");
        pv = Vector3.zero;
        lmov = Vector3.zero;
        //
        abilityName = "StrayShadow";
        abilityDisplayedName = "Stray Shadow";
        cooldown = 5.0f;
        localTimer = 0.0f;
        //
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //
        base.Update();
    }

    public override void Trigger(bool keyUp)
    {
        if (localTimer > 0.0f) return;
        //
        if (keyUp && sa)
        {
            phasePause = false;
            //
            return;
        }
        //
        if (!strayPresent)
        {
            Instantiate(strayShadow, sceneMaster.pMov.camPivot.transform.position + sceneMaster.pMov.playerFrontPoint, Quaternion.identity);
            strayPresent = true;
        }
        if (strayPresent && !sa)
        {
            sa = true;
        }
        if (sa)
        {
            phasePause = true;
        }
        if (strayPresent)
        {
            ;
        }
    }
}
