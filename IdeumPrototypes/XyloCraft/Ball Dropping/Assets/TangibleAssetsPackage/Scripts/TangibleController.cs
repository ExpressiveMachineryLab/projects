using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TangibleController : MonoBehaviour
{

    public Guid Guid;
    private float _yPos;
    public Vector3 Dir;

//    public List<Rule> AllRules = new List<Rule>();
    public List<Rule> ActiveRules =  new List<Rule>();

    public Canvas Canvas;
//    public GameObject PlaceHolder;
    // Start is called before the first frame updates
    void Start()
    {
        _yPos = transform.position.y;
        Guid = new Guid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * GameController.Singleton.RotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * GameController.Singleton.RotationSpeed);
        }

        Dir = transform.forward;

    }

    
    private void OnMouseDrag()
    {
        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objPosition = new Vector3(objPosition.x, _yPos, objPosition.z);
        transform.position = objPosition;
    }

    public void AddRule(GameObject toggle)
    {
        GameObject t = Instantiate(toggle, transform.position + new Vector3(1,1,0), Quaternion.identity);
        Rule r = t.GetComponent<Rule>();
        ActiveRules.Add(r);
        r.SetActive(true);
        r.ToggleBackground.color = GameController.Singleton.ColorBinding[r.TargetColor];
        r.ToggleIcon.color = Color.white;
        t.transform.SetParent(Canvas.transform, false);
        r.ResetColorToggles();
        Reposition();

    }
    
    public void RemoveRule(GameObject toggle)
    {
        
        Rule r = toggle.GetComponent<Rule>();
        ActiveRules.Remove(r);
        toggle.SetActive(false);
        Reposition();
        Destroy(toggle);

    }

    public void Reposition()
    {
        var count = ActiveRules.Count;
        var angle = Mathf.PI / (Mathf.Ceil(count * 0.5f) + 1);
        for (int i = 0; i < count; i++)
        {
            var sideIndex = i % 2;
            var secIndex = Mathf.Ceil((i+1)*0.5f);
            var go = ActiveRules[i].gameObject;
            var composIndex = sideIndex * Mathf.PI + secIndex * angle;
            go.GetComponent<RectTransform>().localPosition = new Vector3(4 * Mathf.Sin(composIndex),4 * Mathf.Cos(composIndex),0);
            ActiveRules[i].CheckSide(sideIndex==1);
        }
    }    

    public void Shoot()
    {
        var bullet = Instantiate(GameController.Singleton.BulletPrefab, transform.position + Vector3.up*0.02f, Quaternion.identity);
        var b = bullet.GetComponent<Bullet>();
        b.SetDir(Dir);
        b.SetParent(this);
    }  
    
}
