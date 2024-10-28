using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAspectRatioController : MonoBehaviour
{
    public float targetAspect = 1.0f; // 1:1 aspect ratio.

    void Start()
    {
        // Ber�knar sk�rmaspekten (bredd/h�jd) f�r den aktuella sk�rmen samt ber�knar h�jdskalan som beh�vs f�r att matcha den aspekt vi vill ha.
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f) // Om h�jdskalan �r mindre �n 1 inneb�r det att sk�rmen �r bredare �n m�laspekten.
        {
            // Justerar kamerans viewport f�r att l�gga till svarta kanter vertikalt.
            camera.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else // Annars, om sk�rmen �r smalare, ber�knas breddskalan.
        {
            // Justerar kamerans viewport f�r att l�gga till svarta kanter horisontellt.
            float scaleWidth = 1.0f / scaleHeight;
            camera.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}
