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
}