using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CollectWhenNearSystem : IExecuteSystem
    {
        private const float CloseDistance = 0.2f;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _hero;
        private readonly IGroup<GameEntity> _pullables;

        public CollectWhenNearSystem(GameContext game)
        {
            _game = game;
            _hero = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero,
                GameMatcher.WorldPosition));

            _pullables = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Pulling,
                GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var hero in _hero)
            {
                foreach (var pullable in _pullables)
                {
                    if (Vector3.Distance(hero.WorldPosition, pullable.WorldPosition) < CloseDistance)
                    {
                        pullable.isCollected = true;
                    }
                }
            }
        }
    }
}
