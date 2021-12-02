using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangibleController : MonoBehaviour
{
    public bool select;

    private float _ypos;

    private Vector3 _startpos;
    public float rotScaler;
    public Vector3 dir;

    public Transform shootButton;

    public bool revealRules = false;
    public Rule selectedRule;
    public List<Rule> allRules = new List<Rule>();
    public List<Rule> activeRules =  new List<Rule>();

    public GameObject placeholder;
    // Start is called before the first frame updates
    void Start()
    {
        _ypos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_startpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        dir = shootButton.transform.position - transform.position;
        dir = Vector3.Normalize(new Vector3(dir.x, 0, dir.z));

    }

    public void selectRule(Rule r)
    {
        if (selectedRule != null)
        {
            selectedRule.GetDeselected();
        }
        selectedRule = r;
    }
    /*
    private void OnMouseDrag()
    {
        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objPosition = new Vector3(objPosition.x, _ypos, objPosition.z);
        
        transform.Rotate(new Vector3(0, Vector3.SignedAngle(_startpos, objPosition, Vector3.up)*rotScaler, 0));
 
        transform.position = objPosition;
        _startpos = objPosition;
    }
    */
    public void addRule(GameObject Button)
    {
        //placeholder.SetActive(true);
    }
    
}
