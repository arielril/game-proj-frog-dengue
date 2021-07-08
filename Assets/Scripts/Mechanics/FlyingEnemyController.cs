using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class FlyingEnemyController : MonoBehaviour
    {
        public float playerRange;
        public float moveSpeed;
        public LayerMask playerLayer;
        public bool playerInRange;
        private PlayerController thePlayer;

        public PatrolPath path;
        public AudioClip ouch;
        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        // Start is called before the first frame update
        void Start()
        {
            thePlayer = FindObjectOfType<PlayerController>();
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter2D(Collision2D collision) {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null) {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.fEnemy = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

            if (playerInRange) {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    thePlayer.transform.position,
                    moveSpeed * Time.deltaTime
                );
            }
        }

        void OnDrawGizmosSelected() {
            Gizmos.DrawSphere(transform.position, playerRange);
        }
    }

}