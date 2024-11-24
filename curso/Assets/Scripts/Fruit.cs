using UnityEngine;

public class Fruit : MonoBehaviour
{
    private GameManager gameManager;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            gameManager.AddFruit();
            Destroy(gameObject);
        }
    }

}
