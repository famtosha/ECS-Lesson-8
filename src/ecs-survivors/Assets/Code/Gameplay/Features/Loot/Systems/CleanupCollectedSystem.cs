using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class CleanupCollectedSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _collected;

        public CleanupCollectedSystem(GameContext game)
        {
            _game = game;
            _collected = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Collected));
        }

        public void Cleanup()
        {
            foreach (var collected in _collected)
            {
                collected.isDestructed = true;
            }
        }
    }
}
