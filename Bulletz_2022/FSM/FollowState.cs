using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulletz_2022
{
    class FollowState : State
    {
        private Enemy enemy;

        public FollowState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public override void Update()
        {
            if(!enemy.CanSeePlayer())
            {
                stateMachine.GoTo(StateEnum.WALK);
            }
            else if(enemy.CanAttackPlayer())
            {
                stateMachine.GoTo(StateEnum.SHOOT);
            }
            else
            {
                enemy.HeadToPlayer();
            }
        }
    }
}
