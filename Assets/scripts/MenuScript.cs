using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuScript : MonoBehaviour
{
    public ViewControl[] views;
    public ViewControl currentView;
    public string lastArg = null, lastView = null;
    // Start is called before the first frame update
    public virtual void Start()
    {
        ButtonScript.menuScript = this;
        views = FindObjectsOfType<ViewControl>(true);
        foreach (var i in views)
        {
            i.Hide();
        }
        currentView.Show();
    }

    public abstract void OnButtonClicked(string _arg);
    public virtual void DisplayView(string vName)
    {
        lastView = currentView.name;
        currentView.Hide();
        foreach (var i in views)
        {
            if (i.name == vName)
            {
                i.Show();
                currentView = i;
                return;
            }
        }
    }
}
