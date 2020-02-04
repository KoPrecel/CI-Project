using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ColorChange : MonoBehaviour {
    public Text Title;
    public Color currentColor = Color.white;

    public Color[] arrayColor = { Color.white, Color.red, Color.green, Color.blue };
    public float timer;
    public int arrayIndex;

    public Color previousColor;

    void Update()
    {
        currentColor = Color.Lerp(previousColor, arrayColor[arrayIndex], timer);
        Title.color = currentColor;
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer -= 1f;
            arrayIndex++;
            if(arrayIndex >= arrayColor.Length)
            {
                arrayIndex = 0;
            }
            previousColor = currentColor;
        }
    }
}

