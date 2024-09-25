using Code.Gameplay.Features.LevelUp.Services;
using Entitas;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class UpdateExperienceMeterSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ILevelUpService _levelUpService;
        private readonly IGroup<GameEntity> _meter;
        private readonly IGroup<GameEntity> _heroes;

        public UpdateExperienceMeterSystem(GameContext game, ILevelUpService levelUpService)
        {
            _game = game;
            _levelUpService = levelUpService;
            _meter = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.ExperienceMeter));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero,
                GameMatcher.Experience));
        }

        public void Execute()
        {
            foreach (GameEntity meter in _meter)
            {
                foreach (GameEntity hero in _heroes)
                {
                    meter.ExperienceMeter.SetExperience(hero.Experience, _levelUpService.ExperienceForLevelUp);
                }
            }
        }
    }
}
