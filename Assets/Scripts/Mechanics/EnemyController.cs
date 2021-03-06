using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : KinematicObject
    {
        public PatrolPath path;
        public AudioClip ouch;
        public float playerRange;
        public float moveSpeed;
        public LayerMask playerLayer;
        public bool playerInRange;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        internal PlayerController thePlayer;
        SpriteRenderer spriteRenderer;
        Rigidbody2D mBody;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            thePlayer = FindObjectOfType<PlayerController>();
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            mBody = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        public void TakeDamage(Vector3 playerPosition)
        {
            playerPosition = playerPosition.normalized * moveSpeed * Time.deltaTime;
            mBody.MovePosition(transform.position + playerPosition);
       
            Schedule<EnemyDeath>().enemy = this;
        }

        protected override void Update()
        {
            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }

       

            playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

            if (playerInRange) {
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
            }
        }


        void OnDrawGizmosSelected() {
            Gizmos.DrawSphere(transform.position, playerRange);
        }
    }
}