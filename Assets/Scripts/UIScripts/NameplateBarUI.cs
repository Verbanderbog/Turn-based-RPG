using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameplateBarUI : MonoBehaviour
{
    
    public TMP_Text nameText;
    public ResourceBar HP;
    public ResourceBar AP;

    public void setupNameplate(CharacterInstance character)
    {
        nameText.text = character.Name;
        updateHealthBar(character._health);
        updateActionBar(character._actionPoints);
    }
    public void updateHealthBar(ResourceStat health)
    {
        HP.updateBar(health);
    }
    public void updateActionBar(ResourceStat actionPoints)
    {
        AP.updateBar(actionPoints);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
