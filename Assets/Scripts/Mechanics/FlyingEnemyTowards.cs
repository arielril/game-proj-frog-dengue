using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyTowards : MonoBehaviour

{

    private Platformer.Mechanics.PlayerController thePlayer;
    public float moveSpeed;
    public float playerRange;
    public bool playerInRange;
    public LayerMask playerLayer;


    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Platformer.Mechanics.PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if(playerInRange) transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}
