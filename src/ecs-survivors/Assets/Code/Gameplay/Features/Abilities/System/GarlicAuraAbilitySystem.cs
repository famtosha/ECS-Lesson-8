using System.Collections.Generic;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Entitas;

namespace Code.Gameplay.Features.Abilities.System
{
    public class DestroyAbilityEntitiesOnUpgradeSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _requests;
        private readonly IGroup<GameEntity> _abilities;

        public DestroyAbilityEntitiesOnUpgradeSystem(GameContext game)
        {
            _game = game;
            _requests = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.UpgradeRequest,
                GameMatcher.AbilityId));

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.AbilityId,
                GameMatcher.RecreatedOnUpgrade));
        }

        public void Execute()
        {
            foreach (GameEntity request in _requests)
            {
                foreach (GameEntity ability in _abilities)
                {
                    if (request.AbilityId == ability.AbilityId)
                    {
                        foreach (GameEntity armament in _game.GetEntitiesWithParentAbility(ability.AbilityId))
                        {
                            armament.isDestructed = true;
                        }
                        ability.isActive = false;
                    }
                }
            }
        }
    }

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