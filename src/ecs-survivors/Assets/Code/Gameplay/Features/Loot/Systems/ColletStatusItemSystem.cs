using Code.Gameplay.Features.Statuses.Applier;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class ColletStatusItemSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IStatusApplier _statusApplier;
        private readonly IGroup<GameEntity> _effects;
        private readonly IGroup<GameEntity> _heroes;

        public ColletStatusItemSystem(GameContext game, IStatusApplier statusApplier)
        {
            _game = game;
            _statusApplier = statusApplier;
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.StatusSetups,
                GameMatcher.Collected));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero,
                GameMatcher.Id,
                GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (var effect in _effects)
            {
                foreach (var hero in _heroes)
                {
                    foreach (var setup in effect.StatusSetups)
                    {
                        _statusApplier.ApplyStatus(setup, hero.Id, hero.Id); 
                    }
                }
            }
        }
    }
}
