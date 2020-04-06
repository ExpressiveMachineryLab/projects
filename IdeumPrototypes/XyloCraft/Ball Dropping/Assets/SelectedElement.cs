using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedElement : MonoBehaviour
{
    public Sprite Blue;
    public Sprite Red;
    public Sprite Green;
    public Sprite Yellow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBlue() 
    {
        this.gameObject.GetComponent<Image>().sprite = Blue;
    }

    public void SetRed()
    {
        this.gameObject.GetComponent<Image>().sprite = Red;
    }
    public void SetGreen()
    {
        this.gameObject.GetComponent<Image>().sprite = Green;
    }
    public void SetYellow()
    {
        this.gameObject.GetComponent<Image>().sprite = Yellow;
    }
    public void SetWild()
    {
        Sprite[] imageArray = {Blue, Red, Green, Yellow};
        this.gameObject.GetComponent<Image>().sprite = imageArray[(int)(Random.value * 10 / 4)];
    }
}
