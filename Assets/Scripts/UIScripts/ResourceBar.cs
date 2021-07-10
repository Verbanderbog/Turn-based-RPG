using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text valueText;

    public void updateBar(ResourceStat resourceStat)
    {
        if (slider != null)
        {
            slider.maxValue = resourceStat.Value;
            slider.value = resourceStat.CurrentAmount;
        }
        if (valueText != null)
        {
            valueText.text = resourceStat.CurrentAmount + " / " + resourceStat.Value + " <size=70%>";
            if (resourceStat.Name == StatType.Health)
                valueText.text += "HP";
            if (resourceStat.Name == StatType.ActionPoints)
                valueText.text += "AP";
        }
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
