using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceVisualizer : MonoBehaviour
{
    int size;
    float theta_scale = 0.01f;
    public bool fadeForceWave = false;
    LineRenderer lineRender;
    Color defaultColor = Color.white;
    float currentAlpha = Color.white.a;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeRender();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeForceWave) {
            currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * 20);
            Color newColor = defaultColor;
            newColor.a = currentAlpha;
            lineRender.material.SetColor("_TintColor", newColor);

            float currentAlphaPercentage = currentAlpha/defaultColor.a *100;

            if (currentAlphaPercentage < 0.1) {
                newColor.a = 0;
                lineRender.material.SetColor("_TintColor", newColor);
                StopFade();
            }
        }
    }

    //allows for clicks of mouse/touch
    private void InitializeRender() {

        lineRender = GetComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Particles/Additive"));
        lineRender.SetWidth(0.0006f, 0.0006f);
        lineRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRender.receiveShadows = false;

    }

    // calculates physics for the force of how the ball bounces in the game
    public void VisualizeForce(float radius, Vector2 position) {
        lineRender.material.SetColor("_Tintcolor", Color.white);
        float sizeValue = (2.0f*Mathf.PI)/ theta_scale;
        size = (int)sizeValue;
        size++;
        float theta = 0f;

        lineRender.SetVertexCount(size);

        DrawCircle(radius, position, theta);
    }


    //Draws circle
    private void DrawCircle(float radius, Vector2 position, float theta) {
        Vector3 pos;

        for(int i = 0; i<size; i++){
            theta += (2.0f*Mathf.PI)*theta_scale;

            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            x += position.x;
            y += position.y;

            pos = new Vector3(x, y, 0);

            lineRender.SetPosition(i, pos);

        }
    }

    public void Fade() {
        fadeForceWave = true;
    }

    public void StopFade() {
        fadeForceWave = false;
        currentAlpha = Color.white.a;
    }
}
