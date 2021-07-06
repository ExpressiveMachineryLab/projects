using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSelector : MonoBehaviour
{
    public BoxCollider2D box;
    public bool selecting;
    public SelectionManager selectionManager;

    public Vector3 startPoint;
    public Vector3 endPoint;

    public static SquareSelector square;
    public Transform sceneContainer;
    public Collider2D rotationKnob;
    List<GameObject> selected;
    bool isRotating;

    private void Awake()
    {
        square = this;
    }

    private void Start()
    {
    }
    public void StartSelecting()
    {
        StopSelecting();
        box = this.GetComponent<BoxCollider2D>();
        this.gameObject.SetActive(true);
        box.enabled = true;
        selecting = true;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.Scale(new Vector3(1f, 1f, 0f));
        this.transform.rotation = Quaternion.identity;
        //box.transform.position = startPoint;

        selected = new List<GameObject>();
    }

    public void StopSelecting()
    {
        selecting = false;
        foreach (Transform t in this.transform)
        {
            t.SetParent(sceneContainer, true);

        }
        if (selected != null)
        {
            
            foreach (GameObject s in selected)
            {
                s.GetComponent<ISelectableObj>().Deselect();
                s.transform.SetParent(sceneContainer, true);
            }
            selected.Clear();
        }
        //selectParent.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (selecting)
        {
            rotationKnob.gameObject.SetActive(false);
            if (Input.GetMouseButton(0))
            {
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint.Scale(new Vector3(1f, 1f, 0f));
                box.transform.position = (startPoint + endPoint) / 2f;

                Vector3 scale = (endPoint - startPoint) / 2f;

                float minScale = 0.01f;

                box.transform.localScale = new Vector3(
                    Mathf.Abs(scale.x) > minScale ? scale.x : minScale,
                    Mathf.Abs(scale.y) > minScale ? scale.y : minScale,
                    Mathf.Abs(scale.z) > minScale ? scale.z : minScale
                    );


                Collider2D[] results = new Collider2D[99];
                int r = box.OverlapCollider(new ContactFilter2D().NoFilter(), results);

                List<GameObject> data = new List<GameObject>();
                for (int i = 0; i < results.Length; i++)
                {
                    Collider2D c = results[i];
                    if (c != null)
                    {
                        data.Add(c.gameObject);
                    }
                }
                //selected.Clear();
                foreach (GameObject g in data)
                {
                    if (g.TryGetComponent(out ISelectableObj newObj))
                    {
                        if (!selected.Contains(g.gameObject))
                        {
                            selected.Add(g.gameObject);
                            newObj.Select();
                        }

                    }
                }
                selected.RemoveAll((GameObject g) =>
                {
                    if (!data.Contains(g))
                    {
                        g.GetComponent<ISelectableObj>().Deselect();
                        return true;
                    }
                    return false;
                });
            }
            if (Input.GetMouseButtonUp(0))
            {
                selecting = false;

                box.enabled = false;
                foreach (GameObject obj in selected)
                {

                    obj.transform.SetParent(this.transform, true);
                }
            }
        }
        else
        {
            rotationKnob.gameObject.SetActive(true);


            rotationKnob.transform.position = this.transform.position + this.transform.up * ((startPoint.y - endPoint.y) / 2f + 1f);
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.collider == rotationKnob)
                {
                    isRotating = true;
                }
            }
        }
        if (!Input.GetMouseButton(0))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            Rotate();
        }
    }

    public List<GameObject> GetSelected()
    {
        return selected;
    }

    private void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
    public void ShootAll()
    {
        foreach (Transform t in this.transform)
        {
            if (t.TryGetComponent(out Emitter e))
            {
                e.ShootBall();
            }
        }
    }

    public void Delete()
    {
        foreach (Transform t in this.transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
