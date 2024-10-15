using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public GameObject particle;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            for (int i = 0; i < 6; i++)
            {
                GameObject newParticle = Instantiate(particle);
                Rigidbody2D rb = newParticle.GetComponent<Rigidbody2D>();

                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * Random.Range(0.5f, 5), ForceMode2D.Impulse);

                spriteRenderer = newParticle.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Getcolor();
            }

            

        }
    }

    Color Getcolor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        return new Color(r, g, b, 1f);
    }
}
