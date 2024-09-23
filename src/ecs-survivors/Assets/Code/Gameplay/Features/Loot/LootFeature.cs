using Code.Gameplay.Features.Loot.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Loot
{
    public class LootFeature : Feature
    {
        public LootFeature(ISystemFactory systems)
        {
            Add(systems.Create<CastForPullablesSystem>());
            Add(systems.Create<PullTowardsHeroSystem>());
            Add(systems.Create<CollectWhenNearSystem>());

            Add(systems.Create<ColletExperienceSystem>());
            Add(systems.Create<ColletEffectItemSystem>());
            Add(systems.Create<ColletStatusItemSystem>());

            Add(systems.Create<CleanupCollectedSystem>());
        }
    }
}
