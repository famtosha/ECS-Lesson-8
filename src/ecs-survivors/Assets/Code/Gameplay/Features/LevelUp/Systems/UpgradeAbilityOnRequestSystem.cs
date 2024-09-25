using Code.Gameplay.Features.Abilities.Upgrade;
using Entitas;
using Unity.VisualScripting;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class UpgradeAbilityOnRequestSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IAbilityUpgradeService _upgradeService;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _levelUps;

        public UpgradeAbilityOnRequestSystem(GameContext game, IAbilityUpgradeService upgradeService)
        {
            _game = game;
            _upgradeService = upgradeService;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.UpgradeRequest,
                GameMatcher.AbilityId));

            _levelUps = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.LevelUp));
        }

        public void Execute()
        {
            foreach (GameEntity request in _entities)
            {
                foreach (var levelUp in _levelUps)
                {
                    _upgradeService.UpgradeAbility(request.AbilityId);
                    request.isDestructed = true;
                    levelUp.isProcessed = true;
                }
            }
        }
    }
}
