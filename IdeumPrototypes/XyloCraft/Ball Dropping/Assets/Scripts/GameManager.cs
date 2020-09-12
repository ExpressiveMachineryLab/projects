using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private SelectionManager selectionManager;
    private float speed = 2.5f;
    private Slider speedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GlobalSpeedSlider") != null) 
        {
            speedMultiplier = GameObject.Find("GlobalSpeedSlider").GetComponent<Slider>();
			UpdateSpeed();
        }
        selectionManager = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
    }
	
    public void UpdateSpeed() 
    {
        speed = speedMultiplier.value;
    }

	public float GetSpeedMultiplier()
	{
		return speed;
	}

	public void SetSpeedMultiplier(float newSpeed)
	{
		speed = newSpeed;
		speedMultiplier.value = newSpeed;
	}

    public void OnClickDelete()
    {
        selectionManager.DeleteSelection(selectionManager.selectedObject);
    }

    public void ResetApplication()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	
}
