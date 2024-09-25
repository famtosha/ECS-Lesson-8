using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using System;

namespace Code.Gameplay.Features.LevelUp.Services
{
    public class LevelUpService : ILevelUpService
    {
        private readonly IStaticDataService _configs;

        public int CurrentLevel { get; private set; }
        public float CurrentExperience { get; private set; }

        public float ExperienceForLevelUp => _configs.ExperienceForLevel(CurrentLevel + 1);

        public LevelUpService(IStaticDataService configs)
        {
            _configs = configs;
        }

        public void AddExperience(float experience)
        {
            CurrentExperience += experience;
            UpdateLevel();
        }

        private void UpdateLevel()
        {
            if (CurrentLevel >= _configs.MaxLevel()) return;

            var experienceForLevelUp = _configs.ExperienceForLevel(CurrentLevel + 1);

            if (CurrentExperience < experienceForLevelUp) return;

            CurrentExperience -= experienceForLevelUp;
            CurrentLevel++;
            UpdateLevel();

            CreateEntity
                .Empty()
                .With(x => x.isLevelUp = true);
        }
    }
}
