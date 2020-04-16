using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TE.Examples;

public class ToggleSelected : MonoBehaviour
{
    public Sprite UnselectedSprite;
    public Sprite SelectedSprite;
    private bool isToggled;
    //public GameObject emitter;
    //private EmitterController TangibleController;

    // Start is called before the first frame update
    void Start()
    {
        isToggled = false;
        //TangibleController = GameObject.Find("Controller").GetComponent<EmitterController>();
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
            this.gameObject.GetComponent<Image>().sprite = SelectedSprite;
            //TangibleController.SetEmitter(emitter);
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = UnselectedSprite;
        }
    }
}
