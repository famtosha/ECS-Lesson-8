using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.Features.Armaments.Factory;
using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Gameplay.Features.Abilities.System
{
    public class BombExplosionSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IStaticDataService _configs;
        private readonly IArmamentFactory _armaments;
        private readonly IAbilityUpgradeService _abilityUpgrade;
        private readonly IGroup<GameEntity> _bombs;
        private readonly IGroup<GameEntity> _heroes;

        public BombExplosionSystem(GameContext game, IStaticDataService configs, IArmamentFactory factory, IAbilityUpgradeService abilityUpgrade)
        {
            _game = game;
            _configs = configs;
            _armaments = factory;
            _abilityUpgrade = abilityUpgrade;
            _bombs = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.TargetPositionReached,
                GameMatcher.WorldPosition));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero,
                GameMatcher.Id));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                foreach (GameEntity bomb in _bombs)
                {
                    _armaments.CreateBombZone(bomb.WorldPosition, _abilityUpgrade.GetAbilityLevel(AbilityId.Bomb), hero.Id);
                    bomb.isDestructed = true;
                }
            }
        }
    }
}