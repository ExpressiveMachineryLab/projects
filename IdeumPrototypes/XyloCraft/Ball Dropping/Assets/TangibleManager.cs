using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TE;
using UnityEngine;

public class TangibleManager : MonoBehaviour, IOnTangibleAdded, IOnTangibleUpdated, IOnTangibleRemoved
{
    private GameManager gameManager;
    static int count;
    public static string ProfilePath = "C:/Users/MT-User/AppData/Roaming/Ideum/TangibleEngine/Profiles/XyloPROFILE.json";
    public GameObject emitterPrefabRed;
    public GameObject emitterPrefabBlue;
    public GameObject emitterPrefabYellow;
    public GameObject emitterPrefabGreen;
    Dictionary<int, GameObject> tangibleObjectLookup;

    Dictionary<string, ElemColor> tangibleIDtoColorLookup = new Dictionary<string, ElemColor>
    {
        {"tangible_1C",ElemColor.red}, // 2 of these
        {"tangible_B",ElemColor.red},
        {"tangible_2A",ElemColor.blue}, // 2 of these
        {"tangible_D",ElemColor.yellow},
        {"tangible_E",ElemColor.yellow},
        {"tangible_G",ElemColor.green},
        {"tangible_I",ElemColor.yellow},
    };

    void Start()
    {
        TangibleEngine.Subscribe(this);
        //TangibleEngine.Instance.p
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tangibleObjectLookup = new Dictionary<int, GameObject>();
        count++;
        TangibleEngine refTE = GameObject.FindGameObjectWithTag("TangibleEngine").GetComponent<TangibleEngine>();
        
    }

    void OnDestroy()
    {
        TangibleEngine.Unsubscribe(this);
    }

    public void OnTangibleAdded(Tangible t) {

        if (!tangibleObjectLookup.TryGetValue(t.Id, out GameObject obj) || obj == null)
        {
            Debug.Log("added tangible with id: " + t.Id + " and pattern " + t.Pattern.Name);
            ElemColor color;
            if (tangibleIDtoColorLookup.TryGetValue(t.PatternName, out ElemColor c))
            {
                color = c;
            }
            else
            {
                color = ElemColor.red;
            }
            //ElemColor color = tangibleIDtoColorLookup[t.Id];
            GameObject emitter;
            switch (color) {
                default:
                case ElemColor.red:
                    emitter = gameManager.AssignEmitter(emitterPrefabRed);
                    break;
                case ElemColor.blue:
                    emitter = gameManager.AssignEmitter(emitterPrefabBlue);
                    break;
                case ElemColor.green:
                    emitter = gameManager.AssignEmitter(emitterPrefabGreen);
                    break;
                case ElemColor.yellow:
                    emitter = gameManager.AssignEmitter(emitterPrefabYellow);
                    break;
            }
            EmitterTangible et = emitter.GetComponent<EmitterTangible>();
            et.tangible = t;
            tangibleObjectLookup[t.Id] = emitter;
            //float frogRot = Vector3.SignedAngle(emitter.transform.up, Vector3.up, Vector3.forward);
            //et.origFrogRot = frogRot;
            //et.angleOffset = TangibleManager.GetRotation(t) - et.lastRot;
            et.color = color;
        }
    }

    public void OnTangibleUpdated(Tangible t) {

        if (tangibleObjectLookup.TryGetValue(t.Id, out GameObject emitterObj))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(t.Pos); ;
            emitterObj.transform.position = new Vector3(worldPos.x, worldPos.y);
            EmitterTangible emitterTangible = emitterObj.GetComponent<EmitterTangible>();
            emitterTangible.lastRot = TangibleManager.GetRotation(t) - emitterTangible.angleOffset;
            emitterObj.transform.rotation = Quaternion.AngleAxis(emitterTangible.lastRot, Vector3.forward);
        }
    
    }

    public void OnTangibleRemoved(Tangible t) 
    {
        if (tangibleObjectLookup.TryGetValue(t.Id, out GameObject emitterObj))
        {
            emitterObj.SetActive(false);
            tangibleObjectLookup[t.Id] = null;
        }
    }

    public static float GetRotation(Tangible t)
    {
        return (t.R * Mathf.Rad2Deg);
    }

    public static List<Pattern> ForcePatternsFromJSON()
    {
        try
        {
            string json = System.IO.File.ReadAllText(ProfilePath);
            List<Pattern> patterns = new List<Pattern>();
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonPatterns jsonData = jsonSerializer.Deserialize<JsonPatterns>(new JsonTextReader(new StringReader(json)));
            patterns.AddRange(jsonData.Patterns);
            return patterns;
        } catch (Exception ex)
        {
            Debug.LogError(ex);
            return null;
        }
        
    }
}
