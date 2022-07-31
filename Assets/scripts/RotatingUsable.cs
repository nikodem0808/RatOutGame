using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingUsable : UsableScript
{
    public GameObject startObject, endObject;
    public Quaternion start, end;
    public bool canChangeDirection, canSlideBackwards, canStop, slidesSmoothly, facesForward, slides, loops, triggered;
    public float slideTime;
    float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        facesForward = true;
        start = startObject.transform.rotation;
        end = endObject.transform.rotation;
        Destroy(startObject);
        Destroy(endObject);
        timeElapsed = 0.0f;
        if (loops)
        {
            canChangeDirection = true;
            canSlideBackwards = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slides)
        {
            if (facesForward)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed > slideTime)
                {
                    gameObject.transform.rotation = end;
                    timeElapsed = slideTime;
                    slides = false;
                }
            }
            else
            {
                timeElapsed -= Time.deltaTime;
                if (timeElapsed < 0.0f)
                {
                    gameObject.transform.rotation = start;
                    timeElapsed = 0.0f;
                    slides = false;
                }
            }
            if (!slides && canSlideBackwards) facesForward = !facesForward;
            if (slidesSmoothly)
            {
                gameObject.transform.rotation = Quaternion.Lerp(start, end,  0.5f - Mathf.Cos(Mathf.PI * timeElapsed / slideTime) / 2.0f);
            }
            else
            {
                gameObject.transform.rotation = Quaternion.Lerp(start, end, timeElapsed / slideTime);
            }
        }
        if (loops && triggered) slides = true;
    }

    public override void Use()
    {
        if (slides)
        {
            if (canStop)
            {
                triggered = false;
                slides = false;
            }
            if (canChangeDirection) facesForward = !facesForward;
            return;
        }
        //
        if (gameObject.transform.rotation == end && !canSlideBackwards) return;
        triggered = true;
        slides = true;
        SFXController.controller.PlaySFX("Servo");
    }
}
