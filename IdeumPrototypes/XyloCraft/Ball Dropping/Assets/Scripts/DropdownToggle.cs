using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DropdownToggle : MonoBehaviour
{

    void Start()
    {
        //codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }
}
