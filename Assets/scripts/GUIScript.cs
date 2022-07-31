using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : ViewControl
{
    public GameObject pausePanel, infobox, usebox;
    // Start is called before the first frame update
    void Start()
    {
        infobox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneMaster.sceneMaster.gamePausedImperative) return;
        if (Input.GetKeyDown(KeyCode.Return)) EndInfo();
        //
        RaycastHit[] rhit;
        rhit = Physics.BoxCastAll(SceneMaster.sceneMaster.pMov.camPivot.transform.position + SceneMaster.sceneMaster.pMov.camPivot.transform.forward * 5.0f, new Vector3(1.25f, 1.25f, 4.5f), SceneMaster.sceneMaster.pMov.camPivot.transform.forward, SceneMaster.sceneMaster.pMov.camPivot.transform.rotation, 0.01f, LayerMask.GetMask("Usable"));
        //
        if (rhit.Length > 0)
            rhit = (
                from _s in rhit
                where !Physics.Raycast(SceneMaster.sceneMaster.pMov.camPivot.transform.position, (_s.collider.gameObject.transform.position - SceneMaster.sceneMaster.pMov.camPivot.transform.position).normalized, (_s.collider.gameObject.transform.position - SceneMaster.sceneMaster.pMov.camPivot.transform.position).magnitude, ~LayerMask.GetMask("NoGroundCollision", "Usable"))
                orderby _s.distance ascending
                select _s
            ).ToArray();
        //
        Debug.Log(rhit.Length);
        if (rhit.Length > 0)
        {
            SceneMaster.sceneMaster.pControl.currentUsable = rhit[0].collider.gameObject.GetComponent<UsableScript>();
            ShowUse(SceneMaster.sceneMaster.pControl.currentUsable.message);
        }
        else
        {
            SceneMaster.sceneMaster.pControl.currentUsable = null;
            EndUse();
        }
    }

    void OnEnable()
    {
        pausePanel.SetActive(false);
    }

    void OnDisable()
    {
        pausePanel.SetActive(true);
    }

   public void ShowInfo(string _info)
    {
        infobox.GetComponent<Text>().text = _info;
        infobox.SetActive(true);
    }

    public void EndInfo()
    {
        infobox.SetActive(false);
    }

    public void ShowUse(string _info)
    {
        usebox.GetComponentInChildren<Text>().text = _info;
        usebox.SetActive(true);
    }

    public void EndUse()
    {
        usebox.SetActive(false);
    }
}
