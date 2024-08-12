using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Bulletz_2022
{
    class ShootState : State
    {
        private Enemy enemy;

        private float shootTimeLimit = 0.25f;
        private float shootCountDown = 0.0f;

        public ShootState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void OnEnter()
        {
            this.enemy.RigidBody.Velocity = Vector2.Zero;
        }

        public override void Update()
        {
            shootCountDown -= Game.Window.DeltaTime;

            if(!enemy.CanAttackPlayer())
            {
                stateMachine.GoTo(StateEnum.FOLLOW);
            }
            else
            {
                if(shootCountDown <= 0.0f)
                {
                    shootCountDown = shootTimeLimit;
                    enemy.ShootPlayer();
                }
            }
        }
    }
}
