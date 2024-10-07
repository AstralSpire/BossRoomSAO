using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float swingRate = .3f;
    private bool canSwing = true;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource swingSFX;
    [SerializeField] private Collider swordHitbox;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canSwing)
        {
            StartCoroutine(swordSwingcrt());
        }
    }

    IEnumerator swordSwingcrt()
    {
        Debug.Log("Player Anim is: " + playerAnimator);
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Attacking");
            Debug.Log("Attacking");
        }

        if(swingSFX != null)
        {
            swingSFX.Play();
        }


        //Enable hitbox for swing
        if(swordHitbox != null)
        {
            swordHitbox.enabled = true;
        }

        //swing and wait for anim to pass
        canSwing = false;

        yield return new WaitForSeconds(1.5f);

        //re disable the hitbox
        if(swordHitbox != null)
        {
            swordHitbox.enabled = false;
        }

        //put the swing on cooldown
        yield return new WaitForSeconds(swingRate);
        canSwing = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boss" || other.gameObject.tag == "Target")
        {
            Debug.Log("Boss or Target Hit");
            //HEALTH LOGIC HERE
        }
    }
}
