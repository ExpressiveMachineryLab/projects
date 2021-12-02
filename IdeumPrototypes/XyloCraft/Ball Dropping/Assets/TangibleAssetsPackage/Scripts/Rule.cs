using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{
    public GameObject block;

    private TangibleController parentFrog;

    public bool selected;
    public Material mat;
    public Color defaultColor;

    public Vector3 defaultSize;
    public Vector3 selectSize;
    // Start is called before the first frame update
    void Start()
    {
        parentFrog = GetComponentInParent<TangibleController>();
        mat = GetComponent<MeshRenderer>().material;
        defaultColor = mat.GetColor("_BaseColor");
        defaultSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSelected()
    {
        transform.localScale = selectSize;
        mat.SetColor("_BaseColor", Color.white);
        parentFrog.selectRule(this);
    }
    public void GetDeselected()
    {
        
    }

    public void setSelectedState(bool s)
    {
        if (s)
        {
            selected = true;
            GetSelected();
        }
        else
        {
            selected = false;
            GetDeselected();
        }
    }
    
    public void changeSelectedState()
    {
        if (selected)
        {
            selected = false;
            GetDeselected();
        }
        else
        {
            selected = true;
            GetSelected();
        }
    }
    
}
