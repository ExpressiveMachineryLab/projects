using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SelectionManager selectionManager;
    private int[] colors = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        selectionManager = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
