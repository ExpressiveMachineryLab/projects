using System.Collections;
using System.Collections.Generic;
using TE;
using UnityEngine;

public class TangibleManager : MonoBehaviour, IOnTangibleAdded, IOnTangibleUpdated, IOnTangibleRemoved
{
    private GameManager gameManager;
    static int count;
    public GameObject emitterPrefabRed;
    public GameObject emitterPrefabBlue;
    public GameObject emitterPrefabYellow;
    public GameObject emitterPrefabGreen;
    Dictionary<int, GameObject> tangibleObjectLookup;

    Dictionary<int, ElemColor> tangibleIDtoColorLookup = new Dictionary<int, ElemColor>
    {
        {0,ElemColor.red},
        {1,ElemColor.red},
        {2,ElemColor.blue},
        {3,ElemColor.blue},
        {4,ElemColor.green},
        {5,ElemColor.green},
        {6,ElemColor.yellow}
    };
    void Start()
    {
        TangibleEngine.Subscribe(this);
        //TangibleEngine.Instance.p
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tangibleObjectLookup = new Dictionary<int, GameObject>();
        count++;
        TangibleEngine refTE = GameObject.FindGameObjectWithTag("TangibleEngine").GetComponent<TangibleEngine>();
        Debug.Log(count + "tangible manager in scene");
    }

    void OnDestroy()
    {
        TangibleEngine.Unsubscribe(this);
    }

    public void OnTangibleAdded(Tangible t) {

        if (!tangibleObjectLookup.TryGetValue(t.Id, out GameObject obj) || obj == null)
        {
            Debug.Log("added tangible with id: " + t.Id);
            ElemColor color;
            if (tangibleIDtoColorLookup.TryGetValue(t.Id, out ElemColor c))
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
}
