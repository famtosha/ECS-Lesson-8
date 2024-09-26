using Code.Gameplay.Common.Time;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Movement.Systems
{
    public class MoveTowardTargetPositionSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new List<GameEntity>();

        public MoveTowardTargetPositionSystem(GameContext game, ITimeService time)
        {
            _game = game;
            _time = time;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.WorldPosition,
                GameMatcher.Speed,
                GameMatcher.TargetPosition)
                .NoneOf(
                GameMatcher.TargetPositionReached));
        }

        public void Execute()
        {
            foreach (GameEntity entity in _entities.GetEntities(_buffer))
            {
                var newPosition = Vector3.MoveTowards(entity.WorldPosition, entity.TargetPosition, entity.Speed * _time.DeltaTime);
                entity.ReplaceWorldPosition(newPosition);
                if (Vector3.Distance(newPosition, entity.TargetPosition) <= 0.01f)
                {
                    entity.isTargetPositionReached = true;
                }
            }
        }
    }
}