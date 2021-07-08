using Platformer.Core;
using Platformer.Mechanics;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyDeath"></typeparam>
    public class EnemyDeath : Simulation.Event<EnemyDeath>
    {
        public EnemyController enemy;
        public FlyingEnemyController fEnemy;

        public override void Execute()
        {
            if (enemy !=null ) {
                enemy._collider.enabled = false;
                enemy.control.enabled = false;
                if (enemy._audio && enemy.ouch)
                    enemy._audio.PlayOneShot(enemy.ouch);
            }

            if (fEnemy != null) {
                fEnemy._collider.enabled = false;
                // fEnemy.control.enabled = false;
            }
            
        }
    }
}