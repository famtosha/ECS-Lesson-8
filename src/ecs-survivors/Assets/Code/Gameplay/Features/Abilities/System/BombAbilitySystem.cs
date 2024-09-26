using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.StaticData;
using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.Abilities.System
{
    public class BombAbilitySystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ICameraProvider _camera;
        private readonly IRandomService _random;
        private readonly IArmamentFactory _armaments;
        private readonly IStaticDataService _configs;
        private readonly IAbilityUpgradeService _upgradeService;
        private readonly IGroup<GameEntity> _abilities;
        private readonly IGroup<GameEntity> _heroes;
        private readonly List<GameEntity> _buffer = new List<GameEntity>();

        public BombAbilitySystem(GameContext game, ICameraProvider camera, IRandomService random, IArmamentFactory armaments, IStaticDataService configs, IAbilityUpgradeService upgradeService)
        {
            _game = game;
            _camera = camera;
            _random = random;
            _armaments = armaments;
            _configs = configs;
            _upgradeService = upgradeService;

            _abilities = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.BombAbility,
                GameMatcher.AbilityId,
                GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero,
                GameMatcher.WorldPosition,
                GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (GameEntity ability in _abilities.GetEntities(_buffer))
            {
                foreach (GameEntity hero in _heroes)
                {
                    _armaments.CreateBomb(hero.WorldPosition, _camera.GetRandomPositionOnScreen(_random), _upgradeService.GetAbilityLevel(AbilityId.Bomb));
                }

                ability.PutOnCooldown(_configs.GetAbilityLevel(AbilityId.Bomb, _upgradeService.GetAbilityLevel(AbilityId.Bomb)).Cooldown);
            }
        }
    }
}