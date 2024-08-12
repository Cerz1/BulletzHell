using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Bulletz_2022
{
    class Enemy : Actor
    {
        private float visionRadius;
        public float walkSpeed;
        public float followSpeed;
        private float shootDistance;

        private StateMachine fsm;

        public Enemy():base("player", Game.PixelsToUnits(58), Game.PixelsToUnits(58))
        {
            spriteOffsetX = 0;

            // Color the sprite differently from Player
            sprite.SetAdditiveTint(200, 10, 10, 0);

            // Set RB
            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.Type = RigidBodyType.Enemy;
            bulletType = BulletType.PlayerBullet;

            // FSM Set
            visionRadius = 5.0f;
            walkSpeed = 1.5f;
            followSpeed = walkSpeed * 2.0f;
            shootDistance = 3.0f;

            fsm = new StateMachine();
            fsm.AddState(StateEnum.WALK, new WalkState(this));
            fsm.AddState(StateEnum.FOLLOW, new FollowState(this));
            fsm.AddState(StateEnum.SHOOT, new ShootState(this));
            fsm.GoTo(StateEnum.WALK);

            IsActive = true;
        }

        public bool CanAttackPlayer()
        {
            Player player = ((PlayScene)Game.CurrentScene).Player;
            Vector2 distVect = player.Position - Position;

            //return distVect.LengthSquared < shootDistance * shootDistance;

            if (distVect.LengthSquared < shootDistance * shootDistance)
            {
                spriteOffsetX = frameWidth;
                return true;
            }

            spriteOffsetX = 0;
            return false;
        }

        public void HeadToPlayer()
        {
            Player player = ((PlayScene)Game.CurrentScene).Player;
            Vector2 distVect = player.Position - Position;
            RigidBody.Velocity.X = followSpeed * Math.Sign(distVect.X);
        }

        public bool CanSeePlayer()
        {
            Player player = ((PlayScene)Game.CurrentScene).Player;
            Vector2 distVect = player.Position - Position;
            return distVect.LengthSquared < visionRadius * visionRadius;
        }

        public void ShootPlayer()
        {
            Player player = ((PlayScene)Game.CurrentScene).Player;
            Vector2 direction = player.Position - Position;
            Shoot(direction.Normalized());
        }

        public override void Update()
        {
            fsm.Update();
        }

        public override void Draw()
        {
            sprite.DrawTexture(texture, spriteOffsetX, 0, frameWidth, frameWidth);
        }
    }
}
