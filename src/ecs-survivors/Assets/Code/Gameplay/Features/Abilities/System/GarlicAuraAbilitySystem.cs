using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.System
{
    public class GarlicAuraAbilitySystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(1);

        private readonly IArmamentFactory _armamentFactory;
        private readonly IAbilityUpgradeService _upgradeService;
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;

        public GarlicAuraAbilitySystem(GameContext game, IArmamentFactory armamentFactory, IAbilityUpgradeService upgradeService)
        {
            _armamentFactory = armamentFactory;
            _upgradeService = upgradeService;
            _abilities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.GarlicAuraAbility)
        .NoneOf(GameMatcher.Active));

            _heroes = game.GetGroup(GameMatcher
              .AllOf(
                GameMatcher.Id,
                GameMatcher.Hero));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
                foreach (GameEntity hero in _heroes)
                {
                    _armamentFactory.CreateEffectAura(AbilityId.GarlicAura, hero.Id, _upgradeService.GetAbilityLevel(AbilityId.GarlicAura));

                    ability.isActive = true;
                }
        }
    }
}