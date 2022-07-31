using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityContainerScript : MonoBehaviour
{
    public int abilityId;
    public GameObject nameContainer, keyContainer, iconContainer, cooldownPanel;
    RectTransform panelTransform;
    Ability refAbility;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        refAbility = SceneMaster.sceneMaster.pControl.abilities[abilityId];
        nameContainer.GetComponent<Text>().text = refAbility.abilityDisplayedName;
        iconContainer.GetComponent<Image>().sprite = refAbility.abilityIcon;
        keyContainer.GetComponentInChildren<Text>().text = playerControl.controlKeys[$"a{abilityId}"].ToString();
        panelTransform = cooldownPanel.GetComponent<RectTransform>();
        panelTransform.sizeDelta = Vector2.right * panelTransform.sizeDelta.x + 64.0f * Vector2.up * refAbility.localTimer / refAbility.cooldown;
    }
}
