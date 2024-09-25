using Code.Gameplay.Features.LevelUp.Services;
using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class ColletExperienceSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ILevelUpService _levelUpService;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _heroes;

        public ColletExperienceSystem(GameContext game, ILevelUpService levelUpService)
        {
            _game = game;
            _levelUpService = levelUpService;
            _entities = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Experience,
                GameMatcher.Collected));

            _heroes = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Hero));
        }

        public void Execute()
        {
            foreach (var experience in _entities)
            {
                foreach (var hero in _heroes)
                {
                    _levelUpService.AddExperience(experience.Experience);
                    hero.ReplaceExperience(_levelUpService.CurrentExperience);
                }
            }
        }
    }
}

