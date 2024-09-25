using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class FinalizeProcessedLevelUps : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _levelUps;

        public FinalizeProcessedLevelUps(GameContext game)
        {
            _game = game;
            _levelUps = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.UpgradeRequest,
                GameMatcher.Processed));
        }

        public void Execute()
        {
            foreach (GameEntity levelUp in _levelUps)
            {
                levelUp.isDestructed = true;
            }
        }
    }
}
