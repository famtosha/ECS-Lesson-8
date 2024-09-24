using Code.Gameplay.Features.LevelUp.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.LevelUp
{
    public class LevelUpFeature : Feature
    {
        public LevelUpFeature(ISystemFactory systems)
        {
            Add(systems.Create<UpdateExperienceMeterSystem>());
        }
    }
}
