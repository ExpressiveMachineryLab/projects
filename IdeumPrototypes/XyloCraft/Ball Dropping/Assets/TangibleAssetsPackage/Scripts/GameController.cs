using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameController : MonoBehaviour
{
    public List<TangibleController> tangibles = new List<TangibleController>();

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) OnMouseClick();
    }

    private void OnMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer = 7;
        int layerMask = 1 << layer;
//        layerMask = ~layerMask;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            var frog = hit.transform.gameObject.GetComponentInParent<TangibleController>();
            
            if (frog != null)
            {
                switch (hit.transform.name)
                {
                    case "Shoot":
                        var b = Instantiate(bulletPrefab, frog.transform.position + Vector3.up*0.02f, Quaternion.identity);
                                            b.GetComponent<Bullet>().setDir(frog.dir);
                        break;
                    case "Plus":
                        Debug.Log("Plus Hit");
                        frog.revealRules = !frog.revealRules;
                        foreach (var rule in frog.allRules)
                        {
                            rule.gameObject.SetActive(frog.revealRules);
                            rule.setSelectedState(false);
                        }
                        break;
                    case "Next": case "Prev": case "Repeat":
                        var r = hit.transform.gameObject.GetComponent<Rule>();
                        r.changeSelectedState();
                        break;
                        
                    default:
                        
                        break;
                        
                }
              
            }
            
                
            
           
          
        }
    }
}
