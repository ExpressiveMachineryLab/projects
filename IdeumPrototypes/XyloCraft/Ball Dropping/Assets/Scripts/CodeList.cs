using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeList : MonoBehaviour
{
    public string identifer;
    public string ballColor;
    public string lineColor;
    public string repeat;
    public string optional;
    public int rhythymOp;
    public int number;

    private GameManager GA;
    private SendStateInformationOneBox OneBoxPanel;
    // Start is called before the first frame update
    void Start()
    {
        GA = GameObject.Find("GameManager").GetComponent<GameManager>();
        OneBoxPanel = GameObject.Find("OneBoxPanel").GetComponent<SendStateInformationOneBox>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyRule() 
    {
        if (identifer.Contains("Chord"))
        {
            OneBoxPanel.SetBallColor(ballColor);
            OneBoxPanel.SetLineColor(lineColor);
            OneBoxPanel.SetSelectedChord(optional);
            OneBoxPanel.SetRepeatState(repeat);
            OneBoxPanel.SetNumber(number);
            OneBoxPanel.UpdateDropdown(0);
        }
        else if (identifer.Contains("Rhythym"))
        {
            OneBoxPanel.SetBallColor(ballColor);
            OneBoxPanel.SetLineColor(lineColor);
            OneBoxPanel.SetSelectedRhythym(rhythymOp);
            OneBoxPanel.SetRepeatState(repeat);
            OneBoxPanel.SetNumber(number);
            OneBoxPanel.UpdateDropdown(1);
        }
        else if (identifer.Contains("Effect")) 
        {
            OneBoxPanel.SetBallColor(ballColor);
            OneBoxPanel.SetLineColor(lineColor);
            OneBoxPanel.SetRepeatState(repeat);
            OneBoxPanel.SetNumber(number);
            OneBoxPanel.UpdateDropdown(2);
        }
        GA.LastSelectedCodeButton = identifer;
        GA.LastSelectedButton = this.gameObject;
    }

    public void SetIdentifer(string name, string ballColor, string repeat, string lineColor, int number) 
    {
        identifer = name;
        this.ballColor = ballColor;
        this.lineColor = lineColor;
        this.repeat = repeat;
        this.number = number;
    }

    public void SetIdentiferChord(string name, string ballColor, string lineColor, string repeat, string optional, int number)
    {
        identifer = name;
        this.ballColor = ballColor;
        this.lineColor = lineColor;
        this.optional = optional;
        this.repeat = repeat;
        this.number = number;
    }

    public void SetIdentiferRhythym(string name, string ballColor, string lineColor, string repeat,  int optional, int number)
    {
        identifer = name;
        this.ballColor = ballColor;
        this.lineColor = lineColor;
        this.rhythymOp = optional;
        this.repeat = repeat;
        this.number = number;
    }
    public void DestroyButton() 
    {
        Destroy(this.gameObject);
    }

}
