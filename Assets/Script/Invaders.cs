using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefab = new Invader[4];

    public int row = 4;
    private int col = 11;
    private float invaderSpeed;

    bool gameOver;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.right;

    public Missile missilePrefab;

    public GameManager gameManager;

    private void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), 1, 1); //Hur ofta ska den skjuta iv�g missiler
        gameManager = FindObjectOfType<GameManager>();     
    }

    //Skapar sj�lva griden med alla invaders.
    public void CreateInvaderGrid()
    {
        for(int r = 0; r < row; r++)
        {
            float width = 2f * (col - 1);
            float height = 2f * (row - 1);

            //f�r att centerar invaders
            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2.3f * r) + centerOffset.y, 0f);
            
            for (int c = 0; c < col; c++)
            {
                Invader tempInvader = Instantiate(prefab[r], transform);

                Vector3 position = rowPosition;
                position.x += 2f * c;
                tempInvader.transform.localPosition = position;


            }
        }
    }
    
    //Aktiverar alla invaders igen och placerar fr�n ursprungsposition
    public void ResetInvaders()
    {
        direction = Vector3.right;
        transform.position = initialPosition;

        foreach(Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    //Skjuter slumpm�ssigt iv�g en missil.
    void MissileAttack()
    {
        int nrOfInvaders = GetInvaderCount();

        if (nrOfInvaders == 0)
        {
            return;
        }

        
        if(gameOver == false) 
        
        {
            foreach (Transform invader in transform)
            {

                if (!invader.gameObject.activeInHierarchy) //om en invader �r d�d ska den inte kunna skjuta...
                    continue;


                float rand = UnityEngine.Random.value;
                if (rand < 0.1f + gameManager.numberOfRounds/15)
                {
                    Instantiate(missilePrefab, invader.position + new Vector3(0, -2f, 0), Quaternion.identity);
                    break;
                }
            }
        }   
    }

    //Kollar hur m�nga invaders som lever
    public int GetInvaderCount()
    {
        int nr = 0;

        foreach(Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }

    //Flyttar invaders �t sidan
    void Update()
    {
        invaderSpeed = gameManager.invaderSpeed;
        gameOver = gameManager.gameOver;

        float speed = invaderSpeed;
        //Debug.Log("Actual speed:" + invaderSpeed);
        transform.position += speed * Time.deltaTime * direction;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) //Kolla bara invaders som lever
                continue;

            if (direction == Vector3.right && invader.position.x >= rightEdge.x - 1f)
            {
                AdvanceRow();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1f)
            {
                AdvanceRow();
                break;
            }
        }
    }
    //Byter riktning och flytter ner ett steg.
    void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        
        if (gameOver == false)
        {
            position.y -= 1f;
            transform.position = position;
        }
    
    }
}
