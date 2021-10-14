using System.Collections;
using System.Collections.Generic;
using TE;
using UnityEngine;

public class TangibleManager : MonoBehaviour, IOnTangibleAdded, IOnTangibleUpdated, IOnTangibleRemoved
{
    private GameManager gameManager;
    public GameObject emitterPrefab;
    Dictionary<int, GameObject> tangibleObjectLookup;
    void Start()
    {
        TangibleEngine.Subscribe(this);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tangibleObjectLookup = new Dictionary<int, GameObject>();
    }

    void OnDestroy()
    {
        TangibleEngine.Unsubscribe(this);
    }

    public void OnTangibleAdded(Tangible t) {

        GameObject emitter = gameManager.AssignEmitter(emitterPrefab);
        EmitterTangible et = emitter.GetComponent<EmitterTangible>();
        et.tangible = t;
        tangibleObjectLookup[t.Id] = emitter;
        Debug.Log(emitter);
    }

    public void OnTangibleUpdated(Tangible t) {

        if (tangibleObjectLookup.TryGetValue(t.Id, out GameObject emitterObj))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(t.Pos); ;
            emitterObj.transform.position = new Vector3(worldPos.x, worldPos.y);
        }
    
    }

    public void OnTangibleRemoved(Tangible t) 
    {
        if (tangibleObjectLookup.TryGetValue(t.Id, out GameObject emitterObj))
        {
            GameObject.Destroy(emitterObj);
        }
    }
}
