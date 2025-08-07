using UnityEngine;

public class Trap_FireButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator anim;
    private Trap_Fire trapfire;

    private void Awake()
    {
     anim = GetComponent<Animator>();
        trapfire = GetComponentInParent<Trap_Fire>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            anim.SetTrigger("activate");
            trapfire.SwitchOffFire();
        }
    }
}
