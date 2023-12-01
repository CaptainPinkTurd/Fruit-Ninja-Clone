using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScreenBombEffects : MonoBehaviour
{
    [Header("Screen Shake Elements")]
    [SerializeField] float duration = 5f;
    [SerializeField] AnimationCurve curve;
    static internal bool shake = false;
    Vector3 startingPos;

    [Header("Background Color and UI Elements")]
    [SerializeField] GameObject UI;
    [SerializeField] Color[] screenColorChange; //White and Game Background color only
    private float colorChangeRate = 0.6f;
    private Color currentColor;
    private int colorIndex = 0;

    private void Awake()
    {
        currentColor = Camera.main.backgroundColor;
        startingPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            colorIndex = 1;
            shake = false;
            StartCoroutine(bombEffects());
        }
    }

    IEnumerator bombEffects ()
    {
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            
            //screenShake effect
            float strength = curve.Evaluate(elapsedTime/duration);
            transform.position = startingPos + UnityEngine.Random.insideUnitSphere * strength;

            //screen color change effect
            currentColor = Camera.main.backgroundColor;
            if (elapsedTime >= 1.75f)
            {
                UI.SetActive(true);
                colorIndex = 0;
                colorChangeRate = 0.035f;
                Camera.main.backgroundColor = Color.Lerp(currentColor, screenColorChange[colorIndex], colorChangeRate);
                FruitsBehaviors.DestroyAllFruitsAndBombs();
            }
            else if(elapsedTime >= 0.75f)
            {
                UI.SetActive(false);
                Camera.main.backgroundColor = Color.Lerp(currentColor, screenColorChange[colorIndex], colorChangeRate);
                FruitsBehaviors.DestroyAllFruitsAndBombs();
            }
            yield return null;
        }
        transform.position = startingPos;
    }
}
