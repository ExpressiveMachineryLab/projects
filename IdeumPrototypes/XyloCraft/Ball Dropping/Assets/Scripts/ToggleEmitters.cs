using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEmitters : MonoBehaviour
{
    public GameObject SlowLeft;
    public GameObject SlowRight;
    public GameObject FastLeft;
    public GameObject FastRight;

    private bool slow = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePanels()
    {
        if (slow)
        {
            SlowLeft.SetActive(false);
            SlowRight.SetActive(false);
            FastLeft.SetActive(true);
            FastRight.SetActive(true);
            slow = false;
        }
        else
        {
            SlowLeft.SetActive(true);
            SlowRight.SetActive(true);
            FastLeft.SetActive(false);
            FastRight.SetActive(false);
            slow = true;
        }
    }
}
