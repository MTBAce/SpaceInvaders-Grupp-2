using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAspectRatioController : MonoBehaviour
{
    public float targetAspect = 1.0f; // 1:1 aspect ratio.

    void Start()
    {
        // Beräknar skärmaspekten (bredd/höjd) för den aktuella skärmen samt beräknar höjdskalan som behövs för att matcha den aspekt vi vill ha.
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight < 1.0f) // Om höjdskalan är mindre än 1 innebär det att skärmen är bredare än målaspekten.
        {
            // Justerar kamerans viewport för att lägga till svarta kanter vertikalt.
            camera.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else // Annars, om skärmen är smalare, beräknas breddskalan.
        {
            // Justerar kamerans viewport för att lägga till svarta kanter horisontellt.
            float scaleWidth = 1.0f / scaleHeight;
            camera.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}
