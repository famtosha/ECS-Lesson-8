using Code.Gameplay.Features.Effects.Factory;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class ColletEffectItemSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IEffectFactory _effectsFactory;
        private readonly IGroup<GameEntity> _effects;
        private readonly IGroup<GameEntity> _heroes;

        public ColletEffectItemSystem(GameContext game, IEffectFactory effects)
        {
            _game = game;
            _effectsFactory = effects;
            _effects = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.EffectSetups,
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
                    foreach (var setup in effect.EffectSetups)
                    {
                        _effectsFactory.CreateEffect(setup, hero.Id, hero.Id);
                    }
                }
            }
        }
    }
}
