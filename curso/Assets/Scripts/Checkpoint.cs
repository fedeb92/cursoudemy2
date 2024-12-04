using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    [SerializeField] private bool canBeReactivated;
    private bool active;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && canBeReactivated == false)
            return;
     
        Player player = collision.GetComponent<Player>();

        if (player != null)
            ActivateChekpoint();
    }

    private void ActivateChekpoint()
    {
        active = true; 
        anim.SetTrigger("activate");
        GameManager.instance.UpdateRespawnPosition(transform);
    }
}
