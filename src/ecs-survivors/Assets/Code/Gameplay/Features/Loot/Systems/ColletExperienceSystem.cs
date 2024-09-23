using Entitas;

namespace Code.Gameplay.Features.Loot.Systems
{
    public class ColletExperienceSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _heroes;

        public ColletExperienceSystem(GameContext game)
        {
            _game = game;
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
                    hero.ReplaceExperience(hero.Experience + experience.Experience);
                }
            }
        }
    }
}
