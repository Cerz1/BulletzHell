using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulletz_2022
{
    class WalkState : State
    {
        private Enemy enemy;

        public WalkState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Update()
        {
            if(enemy.CanSeePlayer())
            {
                stateMachine.GoTo(StateEnum.FOLLOW);
            }
            else
            {
                this.enemy.RigidBody.Velocity.X = -enemy.walkSpeed;
            }
        }
    }
}
