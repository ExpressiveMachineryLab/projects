using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineActionsDropdown : MonoBehaviour
{
    public SendStateInformationActions Actions;
    public GameObject Instrument;
    public GameObject Volume;
    public GameObject DestroyLine;
    private int DropdownState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDropdownState() 
    {
        Debug.Log(this.gameObject.GetComponent<Dropdown>().value);
        DropdownState = this.gameObject.GetComponent<Dropdown>().value;

        //Instrument
        if (DropdownState == 0) 
        {
            Actions.SetDropdownState("Instruments");
            Instrument.SetActive(true);
            Volume.SetActive(false);
            DestroyLine.SetActive(false);
        }

        //Volume
        if (DropdownState == 1)
        {
            Actions.SetDropdownState("Volume");
            Instrument.SetActive(false);
            Volume.SetActive(true);
            DestroyLine.SetActive(false);
        }

        //Destroy
        if (DropdownState == 2)
        {
            Actions.SetDropdownState("Destroy");
            Instrument.SetActive(false);
            Volume.SetActive(false);
            DestroyLine.SetActive(true);
        }
    }

}
