﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ChordPanel;
    public GameObject RepeatPanel;
    public GameObject GenrePanel;
    public GameObject EffectsPanel;
    public GameObject TeamPanel;
    public GameObject ActionPanel;
    public GameObject OneBoxPanel;

    public GameObject CodeButton;
    public GameObject ScrollView;
    public GameObject HiddenCodeBoxes;
    public string CurrentCode;

    public Animator CodeBox;

    private SelectionManager selectionManager;
    public string LastSelectedCodeButton;
    public GameObject LastSelectedButton;
    private int[] colors = new int[4];
    private int Sound = 0;
    private float speed = 1;
    private Slider speedMultiplier;
    private Scrollbar CodeBoxScroll;

    public bool OneBox;
    public bool FourBox;
    public bool ThreeBox;
    public Dropdown Action;

    public int ChordPanelCount;
    public int RhythymPanelCount;
    public int EffectPanelCount;
    private int ActionsPanelCount;

    // Start is called before the first frame update
    void Start()
    {
        if (FourBox) 
        {
            ChordPanelCount = 4;
        }
        if (GameObject.Find("GlobalSpeedSlider") != null) 
        {
            speedMultiplier = GameObject.Find("GlobalSpeedSlider").GetComponent<Slider>();
        }
        
        if (GameObject.Find("Scrollbar Vertical") != null)
        {
            CodeBoxScroll = GameObject.Find("Scrollbar Vertical").GetComponent<Scrollbar>();
        }

        selectionManager = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseChordCount() 
    {
        ChordPanelCount++;
    }
    public void IncreaseRhythymCount()
    {
        RhythymPanelCount++;
    }
    public void IncreaseEffectCount()
    {
        EffectPanelCount++;
    }

    public void LoadOneBox() 
    {
        SceneManager.LoadScene("OneBoxProtoype");
    }

    public void LoadFourBox() 
    {
        SceneManager.LoadScene("FourBox");
    }

    public void LoadTab() 
    {
        SceneManager.LoadScene("Web Prototype Development");
    }

    public void LoadThree() 
    {
        SceneManager.LoadScene("Web Prototype Working");
    }

    public void UpdateSpeed() 
    {
        speed = speedMultiplier.value;
    }

    public void IncreaseSpeed() 
    {
        if (speed != 1)
        {
            speed = 1;
        }
        else 
        {
            speed = 1.5f;
        }
        
    }

    public void DecreaseSpeed() 
    {
        if (speed != 1)
        {
            speed = 1;
        }
        else 
        {
            speed = 0.5f;
        }
        
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

    public void ScrollToChord() 
    {
        CodeBoxScroll.value = 1;
        if (!CodeBox.GetBool("Open")) 
        {
            CodeBox.SetTrigger("Clicked");
            CodeBox.SetTrigger("Open");
        }
    }

    public void ScrollToRepeat() 
    {
        CodeBoxScroll.value = 0.6795676f;
        if (!CodeBox.GetBool("Open"))
        {
            CodeBox.SetTrigger("Clicked");
            CodeBox.SetTrigger("Open");
        }
    }

    public void ScrollToGenre() 
    {
        CodeBoxScroll.value = 0.3483143f;
        if (!CodeBox.GetBool("Open"))
        {
            CodeBox.SetTrigger("Clicked");
            CodeBox.SetTrigger("Open");
        }
    }

    public void ScrollToEffects() 
    {
        CodeBoxScroll.value = 0;
        if (!CodeBox.GetBool("Open"))
        {
            CodeBox.SetTrigger("Clicked");
            CodeBox.SetTrigger("Open");
        }
    }

    public void ScrollToLineActions() 
    {
        CodeBoxScroll.value = 0f;
        if (!CodeBox.GetBool("Open"))
        {
            CodeBox.SetTrigger("Clicked");
            CodeBox.SetTrigger("Open");
        }
    }

    public void ToggleTab() 
    {
        CodeBox.SetBool("Open", !CodeBox.GetBool("Open"));
    }

    public void AddRule()
    {
        int number = ChordPanelCount + RhythymPanelCount + EffectPanelCount + 1;
        OneBoxPanel.GetComponent<SendStateInformationOneBox>().SetNumber(number);
        if (Action.value == 0)
        {
            ChordPanelCount++;
            string name = "ChordPanel" + ChordPanelCount;
            string ballColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetBallColor();
            string lineColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetLineColor();
            string chord = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetSelectedChord();
            string repeat = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetRepeatState();

            GameObject ChordTemp = Instantiate(CodeButton);
            ChordTemp.GetComponentInChildren<Text>().text = number + ". If " + ballColor + " ball hits " + lineColor + " line: Chord " + chord;
            ChordTemp.transform.SetParent(ScrollView.transform, false);
            ChordTemp.GetComponent<CodeList>().SetIdentiferChord(name, ballColor, lineColor, repeat, chord, number);

            GameObject temp = Instantiate(ChordPanel);
            temp.transform.SetParent(HiddenCodeBoxes.transform, false);
            temp.name = name;
            temp.GetComponent<SendStateInformationChord>().SetBallColor(ballColor);
            temp.GetComponent<SendStateInformationChord>().SetLineColor(lineColor);
            temp.GetComponent<SendStateInformationChord>().SetSelectedChord(chord);
            temp.GetComponent<SendStateInformationChord>().SetRepeatState(repeat);

            LastSelectedCodeButton = name;
            LastSelectedButton = ChordTemp;
        }
        else if (Action.value == 1)
        {
            RhythymPanelCount++;

            string name = "RhythymPanel" + RhythymPanelCount;
            string ballColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetBallColor();
            string lineColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetLineColor();
            int rhythym = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetSelectedRhythym();
            string repeat = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetRepeatState();

            GameObject ChordTemp = Instantiate(CodeButton);
            ChordTemp.GetComponentInChildren<Text>().text = number + ". If " + ballColor + " ball hits " + lineColor + " line: Repeat " + rhythym;
            ChordTemp.transform.SetParent(ScrollView.transform, false);
            ChordTemp.GetComponent<CodeList>().SetIdentiferRhythym(name, ballColor, lineColor, repeat, rhythym, number);

            GameObject temp = Instantiate(RepeatPanel);
            temp.transform.SetParent(HiddenCodeBoxes.transform, false);
            temp.name = "RhythymPanel" + RhythymPanelCount;
            temp.GetComponent<SendStateInformationRhythym>().SetBallColor(ballColor);
            temp.GetComponent<SendStateInformationRhythym>().SetLineColor(lineColor);
            temp.GetComponent<SendStateInformationRhythym>().SetSelectedRhythym(rhythym);
            temp.GetComponent<SendStateInformationRhythym>().SetRepeatState(repeat);

            LastSelectedCodeButton = name;
            LastSelectedButton = ChordTemp;
        }
        else if (Action.value == 2)
        {
            EffectPanelCount++;

            string name = "EffectsPanel" + EffectPanelCount;
            string ballColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetBallColor();
            string lineColor = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetLineColor();
            string repeat = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetRepeatState();
            string visual = OneBoxPanel.GetComponent<SendStateInformationOneBox>().GetSelectedChord();
            Debug.Log(repeat);

            GameObject ChordTemp = Instantiate(CodeButton);
            ChordTemp.GetComponentInChildren<Text>().text = number + ". If " + ballColor + " ball hits " + lineColor + " line: Visual " + visual;
            ChordTemp.transform.SetParent(ScrollView.transform, false);

            ChordTemp.GetComponent<CodeList>().SetIdentifer(name, ballColor, repeat, lineColor, number);

            GameObject temp = Instantiate(ChordPanel);
            temp.transform.SetParent(HiddenCodeBoxes.transform, false);
            temp.name = "EffectsPanel" + EffectPanelCount;
            temp.GetComponent<SendStateInformationChord>().SetBallColor(ballColor);
            temp.GetComponent<SendStateInformationChord>().SetLineColor(lineColor);
            temp.GetComponent<SendStateInformationChord>().SetSelectedChord(visual);
            temp.GetComponent<SendStateInformationChord>().SetRepeatState(repeat);

            LastSelectedCodeButton = name;
            LastSelectedButton = ChordTemp;
        }
        
    }

    public void DeleteCode() 
    {
        Destroy(LastSelectedButton);
        Destroy(GameObject.Find(LastSelectedCodeButton));
        LastSelectedCodeButton = null;
    }

    public int GetChordCount() 
    {
        return ChordPanelCount;
    }
    public int GetRhythymCount() 
    {
        return RhythymPanelCount;
    }
    public int GetEffectCount() 
    {
        return EffectPanelCount;
    }
    public void SetOneBoxPanel() 
    {
    
    }
}
