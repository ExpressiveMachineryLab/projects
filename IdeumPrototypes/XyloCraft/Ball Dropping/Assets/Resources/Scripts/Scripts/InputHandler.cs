using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class InputHandler : MonoBehaviour
{
    public float forceMaxRadius = 1.5f;
    Vector2 forcePosition = Vector2.zero;
    private bool exertForce = false;
    private bool canPush = true;
    private float currentRadius = 0f;

    ForceVisualizer fv;

    public float forceGunCooldown = 1f;
    private float currentRadiusPercentage = 0;
    

    public static Vector2 GetForcePosition() {
        return Input.mousePosition;
    }

    void Start()
    {
        fv = GetComponent<ForceVisualizer>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            ActivateForceWave();
        }

        if (exertForce) {
            currentRadius = Mathf.Lerp(currentRadius, forceMaxRadius, Time.deltaTime * 30);
            ExertForce(forcePosition, currentRadius);
        }

        currentRadiusPercentage = currentRadius / forceMaxRadius * 100;

        if (currentRadiusPercentage > 70) {
            if (!fv.fadeForceWave) {fv.Fade();}
        }
        if (currentRadiusPercentage > 99) {
            DisableForceWave();
        }
    }

    private void ActivateForceWave() {
        currentRadius = 0;
        forcePosition = Camera.main.ScreenToWorldPoint(GetForcePosition());
        exertForce = true;
        canPush = true;
    }

    private void DisableForceWave() {
        currentRadius = 0;
        exertForce = false;
        canPush = false;
    }

    private void ExertForce(Vector2 position, float radius) {
        fv.VisualizeForce(radius, forcePosition);

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(position, radius);
        foreach(Collider2D c in hitObjects) {
            if (c.tag.Equals("Pushable")) {
                if (canPush) {PushObject(position, c);}
                canPush = false;
            }
        }
    }

    private void PushObject(Vector2 position, Collider2D c) {
        Vector2 pos = new Vector2(c.transform.position.x, c.transform.position.y);

        Rigidbody2D rig = c.GetComponent<Rigidbody2D>();

        //clear momentrum before hit
        rig.velocity = Vector3.zero;

        rig.AddForce((pos-position).normalized * 1.5f, ForceMode2D.Impulse);
    }
}
