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
    public Sprite Wild;

    public string currentColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetCurrentColor() 
    {
        return currentColor;
    }

    public void SetBlue() 
    {
        this.gameObject.GetComponent<Image>().sprite = Blue;
        currentColor = Blue.name;
    }

    public void SetRed()
    {
        this.gameObject.GetComponent<Image>().sprite = Red;
        currentColor = Red.name;
    }
    public void SetGreen()
    {
        this.gameObject.GetComponent<Image>().sprite = Green;
        currentColor = Green.name;
    }
    public void SetYellow()
    {
        this.gameObject.GetComponent<Image>().sprite = Yellow;
        currentColor = Yellow.name;
    }
    public void SetWild()
    {
        this.gameObject.GetComponent<Image>().sprite = Wild;
        currentColor = "All";
        //Sprite[] imageArray = {Blue, Red, Green, Yellow};
        //Sprite randomSprite;
        //do {
        //    randomSprite = imageArray[(int)(Random.value * 4)];
        //} while (currentColor == randomSprite.name);
        //currentColor = randomSprite.name;
        //Debug.Log(currentColor);
        //this.gameObject.GetComponent<Image>().sprite = randomSprite;
    }
    public void SetWildSprite() 
    {
        Sprite[] imageArray = { Blue, Red, Green, Yellow };
        Sprite randomSprite = imageArray[(int)(Random.value * 4)];
        currentColor = randomSprite.name;
        this.gameObject.GetComponent<Image>().sprite = randomSprite;
    }
}
