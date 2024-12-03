using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private bool active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
            return;
     
        Player player = collision.GetComponent<Player>();

        if (player != null)
            ActivateChekpoint();
    }

    private void ActivateChekpoint()
    {
        active = true; 
        anim.SetBool("activate", active);
    }
}
