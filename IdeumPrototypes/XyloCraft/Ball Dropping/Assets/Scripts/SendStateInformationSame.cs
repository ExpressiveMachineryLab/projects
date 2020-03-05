using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationSame : MonoBehaviour
{
    public int ballNumber;

    public Dropdown localCodeStateDropdown;
    public Dropdown localLineStateDropdown;
    public Dropdown localLoopDropdown;
    public Dropdown localColorDropdown;
    public Slider localVolumeSlider;
    public Image flashBorder;

    private Color thisColor;

    // Start is called before the first frame update
    void Start()
    {
        localCodeStateDropdown.onValueChanged.AddListener(delegate {
            CodeStateHandler(localCodeStateDropdown);
        });

        localLineStateDropdown.onValueChanged.AddListener(delegate
        {
            LineStateHandler(localLineStateDropdown);
        });
        
        thisColor = this.gameObject.GetComponent<Image>().color;
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
        if (localCodeStateDropdown.value == 1)
        {
            SetSecondary(localVolumeSlider.gameObject);
        }
        else if (localCodeStateDropdown.value == 2)
        {
            SetSecondary(localColorDropdown.gameObject);
        }
        else if (localCodeStateDropdown.value == 4)
        {
            SetSecondary(localLoopDropdown.gameObject);
        }
        else
        {
            SetSecondary(null);
        }

    }

    private void SetSecondary(GameObject activeSecondary)
    {
        localLoopDropdown.gameObject.SetActive(false);
        localColorDropdown.gameObject.SetActive(false);
        localVolumeSlider.gameObject.SetActive(false);
        if (activeSecondary) { activeSecondary.SetActive(true); }

    }

    

    public int GetBallNumber()
    {
        return ballNumber;
    }

    public int GetCodeState()
    {
        return localCodeStateDropdown.value;
    }

    public int GetLineState()
    {
        return localLineStateDropdown.value;
    }

    public int GetLoopState()
    {
        return localLoopDropdown.value;
    }

    public int GetColorState()
    {
        return localColorDropdown.value;
    }

    public float GetVolumeState()
    {
        return localVolumeSlider.value;
    }

    private void LineStateHandler(Dropdown localLineStateDropdown)
    {
        localCodeStateDropdown.value = 0;
    }

    public void FlashBox(int color)
    {
        StartCoroutine(Flash(color));
    }

    private IEnumerator Flash(int color)
    {
        flashBorder.color += new Color(0, 0, 0, 0.5f);
        if (color == 0)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 0, 1, 0.1f);
        }
        if (color == 1)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 0.3f);
        }
        if (color == 2)
        {
            //this.gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
        }
        if (color == 3)
        {
            this.gameObject.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 0.3f);
        }

        yield return new WaitForSeconds(0.3f);
        flashBorder.color -= new Color(0, 0, 0, 0.5f);
        this.gameObject.GetComponent<Image>().color = thisColor;
        yield return new WaitForSeconds(0.1f);
    }
}
