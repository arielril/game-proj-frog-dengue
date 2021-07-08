using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public FlyingEnemyController fEnemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private void ExecuteEnemy() {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else
            {
                Schedule<PlayerDeath>();
            }
        }

        private void ExecuteFlyingEnemy() {
            var willHurtFlyingEnemy = player.transform.position.y >= fEnemy.transform.position.y;
            
            if (willHurtFlyingEnemy) {
                var enemyHealth = fEnemy.GetComponent<Health>();
                
                if (enemyHealth != null) {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive) {
                        Schedule<EnemyDeath>().fEnemy = fEnemy;
                        player.Bounce(2);
                    } else {
                        player.Bounce(7);
                    }
                } else {
                    Schedule<EnemyDeath>().fEnemy = fEnemy;
                    player.Bounce(2);
                }
            } else {
                Schedule<PlayerDeath>();
            }
        }

        public override void Execute()
        {
            if (enemy != null)
                this.ExecuteEnemy();
            if (fEnemy != null)
                this.ExecuteFlyingEnemy();
        }
    }
}