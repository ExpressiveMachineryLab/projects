using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SelectionManager selectionManager;
    private int[] colors = new int[4];
    private int Sound = 0;
    private float speed = 1;
    private Slider speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = GameObject.Find("GlobalSpeedSlider").GetComponent<Slider>();
        selectionManager = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSpeed() 
    {
        speed = speedMultiplier.value;
    }

    public float GetSpeedMultiplier() 
    {
        return speed;
    }

    public int GetSoundState() 
    {
        return Sound;
    }

    public void OnClickDelete()
    {
        if (selectionManager.GetColor() > -1)
        {
            DeleteBird(selectionManager.GetColor());
        }
        selectionManager.DeleteSelection(selectionManager.selectedObject.gameObject);
    }
    public void ResetApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 0 = blue
    // 1 = green
    // 2 = red
    // 3 = yellow
    public void InstantiateBird(int color) 
    {
        colors[color]++;
    }

    public void DeleteBird(int color) 
    {
        colors[color]--;
    }

    public int GetBird(int color) 
    {
        return colors[color];
    }

    public void SetToSound1() 
    {
        Sound = 0;
    }

    public void SetToSound2()
    {
        Sound = 1;
    }

    public void SetToSound3() 
    {
        Sound = 2;
    }
}
