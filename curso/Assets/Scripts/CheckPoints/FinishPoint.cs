using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Animator anim => GetComponent<Animator>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            anim.SetTrigger("activate");
            Debug.Log("lvl complete");
        }
    }
}
