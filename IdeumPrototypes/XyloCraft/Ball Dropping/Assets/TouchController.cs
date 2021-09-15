﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler
{
    public bool useTouchTracker;
    public List<TouchTracker> touchTrackers;
    public GameObject touchTrackerPrefab;
    public int count;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("touch " + eventData.pointerId);
        if (touchTrackers.Count <= eventData.pointerId)
        {
            GameObject trackerObj = GameObject.Instantiate(touchTrackerPrefab, this.transform);
            TouchTracker tracker = trackerObj.GetComponent<TouchTracker>();
            tracker.SetIndex(eventData.pointerId);
            touchTrackers.Add(tracker);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        if (useTouchTracker)
        {
            touchTrackers = new List<TouchTracker>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        count = Input.touchCount;
    }
}
