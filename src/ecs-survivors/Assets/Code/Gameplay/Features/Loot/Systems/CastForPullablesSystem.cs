using Code.Common.Extensions;
using Code.Gameplay.Common.Physics;
using Entitas;
using System;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CastForPullablesSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IPhysicsService _physics;
        private readonly IGroup<GameEntity> _looters;
        private readonly GameEntity[] _buffer = new GameEntity[128];

        public CastForPullablesSystem(GameContext game, IPhysicsService physics)
        {
            _game = game;
            _physics = physics;
            _looters = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.PickupRadius,
                GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity looter in _looters)
            {
                var hitted = _physics.CircleCastNonAlloc(looter.WorldPosition, looter.PickupRadius, CollisionLayer.Collectable.AsMask(), _buffer);
                for (int i = 0; i < hitted; i++)
                {
                    var target = _buffer[i];
                    if (target.isPullable)
                    {
                        target.isPullable = false;
                        target.isPulling = true;
                    }
                }
            }
        }
    }
}
