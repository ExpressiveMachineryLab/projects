using System.Collections;
using System.Collections.Generic;
using TE;
using UnityEngine;

public class TangibleManager : MonoBehaviour, IOnTangibleAdded, IOnTangibleUpdated, IOnTangibleRemoved
{
    private GameManager gameManager;
    static int count;
    public GameObject emitterPrefab;
    Dictionary<int, GameObject> tangibleObjectLookup;

    void Start()
    {
        TangibleEngine.Subscribe(this);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tangibleObjectLookup = new Dictionary<int, GameObject>();
        count++;
        Debug.Log(count + "tangible manager in scene");
    }

    void OnDestroy()
    {
        TangibleEngine.Unsubscribe(this);
    }

    public void OnTangibleAdded(Tangible t) {

        if (!tangibleObjectLookup.TryGetValue(t.Id, out GameObject obj) || obj == null)
        {
            GameObject emitter = gameManager.AssignEmitter(emitterPrefab);
            EmitterTangible et = emitter.GetComponent<EmitterTangible>();
            et.tangible = t;
            tangibleObjectLookup[t.Id] = emitter;
            //float frogRot = Vector3.SignedAngle(emitter.transform.up, Vector3.up, Vector3.forward);
            //et.origFrogRot = frogRot;
            et.angleOffset = TangibleManager.GetRotation(t) - et.lastRot;
            Debug.Log(et.angleOffset);
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
