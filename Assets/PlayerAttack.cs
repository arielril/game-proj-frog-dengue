using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        public Animator animator;
        public Transform attackPos;
        public LayerMask whatIsEnemy;
        public float attackY;
        public float attackX;
        public PlayerController player;
    

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                TongueAttack();               
            }

        }

        void TongueAttack()
        {
            animator.SetTrigger("attack");
        }

        void TongueHit()
        {
            Collider2D[] targets = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackY, attackX),0, whatIsEnemy);

            foreach (Collider2D target in targets)
            {
                target.GetComponent<EnemyController>().TakeDamage(player.transform.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPos.position, new Vector3(attackY, attackX, 1));
        }
    }
}
