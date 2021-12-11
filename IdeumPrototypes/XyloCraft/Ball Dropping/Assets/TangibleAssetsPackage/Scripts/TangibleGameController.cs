using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TangibleGameController : MonoBehaviour
{
    public static TangibleGameController Singleton;
    public List<TangibleController> Tangibles = new List<TangibleController>();

    public enum TargetColors
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        Yellow = 3
    }

    public List<Color> RenderColor = new List<Color>(Enum.GetNames(typeof(TargetColors)).Length);
    
    public Dictionary<TargetColors, Color> ColorBinding =  new Dictionary<TargetColors, Color>();
    
    public enum RuleType
    {
        Next,
        Previous,
        Repeat
    }
    public float RotationSpeed = 200f;
    
    public GameObject BulletPrefab;
    
    
    void Awake()
    {
        for (int i = 0; i < RenderColor.Count; i++)
        {
            ColorBinding.Add((TargetColors)i, RenderColor[i]);
        }

        Tangibles.AddRange(FindObjectsOfType<TangibleController>());
    }

    
    // Start is called before the first frame update
    void Start()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public void AddTangible(TangibleController t)
    {
        Tangibles.Add(t);
    }

    public void RemoveTangible(TangibleController t)
    {
        if (Tangibles.Contains(t))
        {
            Tangibles.Remove(t);
        }
        else
        {
            Debug.Log("CAUTION: Attempt to remove non-existing tangible");
        }
    }

    public static Dictionary<TargetColors, ElemColor> ColorConvertDict = new Dictionary<TargetColors, ElemColor>
    {
        {TargetColors.Red, ElemColor.red},
        {TargetColors.Blue, ElemColor.blue},
        {TargetColors.Yellow, ElemColor.yellow},
        {TargetColors.Green, ElemColor.green}
    };

   

}
