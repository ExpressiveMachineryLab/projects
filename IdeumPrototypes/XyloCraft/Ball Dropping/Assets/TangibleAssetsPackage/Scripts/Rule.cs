using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Rule : MonoBehaviour
{

    [Header("Rule Setting")]
    public TangibleGameController.RuleType RuleType;
    public TangibleGameController.TargetColors TargetColor;
    public bool Active = false;
    public bool Invert = false;
    
    [Header("Default Colors")]
    public Color BgColor;
    public Color IcColor;
    
    [Header("Components")]
    public List<Toggle> ColorToggles;
    public Image ToggleBackground;
    public Image ToggleIcon;
    public GameObject RuleBlockVertical;
    public GameObject RuleBlockHorizontal;
        
    [Header("Invertible Component")]
    public RectTransform HorBackground;
    public RectTransform DeleteButton;
    public RectTransform QuestionButton;
    public RectTransform TutorialBlock;


    void Start()
    {
        RuleBlockVertical.SetActive(false);
        RuleBlockHorizontal.SetActive(false);
    }
    


    void Update()
    {
        if (ColorToggles.Count == 0)
        {
            var colorOptions = GetComponentsInChildren<TargetColorOption>();
            foreach (var co in colorOptions)
            {
                var t = co.gameObject.GetComponent<Toggle>();
                var img = t.GetComponentInChildren<Image>();
                img.color = TangibleGameController.Singleton.ColorBinding[co.MyColor];
                ColorToggles.Add(t);
                if (co.GetColor() == TargetColor)
                {
                    t.isOn = true;
                }
                else
                {
                    t.isOn = false;
                }
            }
        }
        
        if (!RuleBlockVertical.activeInHierarchy && !RuleBlockHorizontal.activeInHierarchy)
        {
            return;
        }
        ToggleIcon.color = Color.white;
        foreach (Toggle t in ColorToggles)
        {
            if (t.isOn)
            {
                TargetColor = t.gameObject.GetComponent<TargetColorOption>().GetColor();
                ToggleBackground.color = t.GetComponentInChildren<Image>().color;
            }
        }
    }

    public void ResetColorToggles()
    {
        ColorToggles = new List<Toggle>();
    }

    public void ResetColor()
    {
        if (Active)
        {
            ToggleBackground.color = TangibleGameController.Singleton.ColorBinding[TargetColor];
            ToggleIcon.color = Color.white;
        }
        else
        {
            ToggleBackground.color = BgColor;
            ToggleIcon.color = IcColor;
        }
    }

    public void ShowRuleBlock()
    {
        if (Active)
        {
            RuleBlockHorizontal.SetActive(!RuleBlockHorizontal.activeInHierarchy);
        }
        else
        {
            RuleBlockVertical.SetActive(!RuleBlockVertical.activeInHierarchy);
        }
    }

    public void CheckSide(bool i)
    {
        if (!Active) return;
        if (i != Invert)
        {
            Invert = i;
            RuleBlockHorizontal.GetComponent<RectTransform>().localPosition = HorInvert(RuleBlockHorizontal.GetComponent<RectTransform>());
            var s = i ? -1 : 1;
            HorBackground.localScale = new Vector3(s, 1, 1);
            DeleteButton.localPosition = HorInvert(DeleteButton);
            QuestionButton.localPosition = HorInvert(QuestionButton);
            TutorialBlock.localPosition = HorInvert(TutorialBlock);
        }       
    }

    private Vector3 HorInvert(RectTransform rt)
    {
        var p = rt.localPosition;
        return new Vector3(-p.x, p.y, p.z);
    }

    public void SetActive(bool a)
    {
        Active = a;
    }
    
    
    
    
}
