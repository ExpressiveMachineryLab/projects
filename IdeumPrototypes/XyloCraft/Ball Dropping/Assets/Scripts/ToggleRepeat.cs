using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRepeat : MonoBehaviour
{
    public Sprite UnselectedSprite;
    public Sprite SelectedSprite;
    public bool isToggled;
    //public GameObject emitter;
    //private EmitterController TangibleController;

    // Start is called before the first frame update
    void Start()
    {
        //isToggled = false;
        //TangibleController = GameObject.Find("Controller").GetComponent<EmitterController>();
        if (isToggled)
        {
            this.gameObject.GetComponent<Image>().sprite = SelectedSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggled()
    {
        isToggled = !isToggled;

        if (isToggled)
        {
            //Debug.Log("Toggled On");
            this.gameObject.GetComponent<Image>().sprite = SelectedSprite;
            //TangibleController.SetEmitter(emitter);
        }
        else
        {
            //Debug.Log("Toggled Off");
            this.gameObject.GetComponent<Image>().sprite = UnselectedSprite;
        }
    }
}
