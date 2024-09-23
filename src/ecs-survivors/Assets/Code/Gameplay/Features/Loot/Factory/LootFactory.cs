using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IStaticDataService _config;
        private readonly IIdentifierService _ids;

        public LootFactory(IStaticDataService config, IIdentifierService ids)
        {
            _config = config;
            _ids = ids;
        }

        public GameEntity CreateLoot(LootTypeId type, Vector2 worldPosition)
        {
            var config = _config.GetLootConfig(type);
            return CreateEntity
                .Empty()
                .AddId(_ids.Next())
                .AddWorldPosition(worldPosition)
                .AddViewPrefab(config.Prefab)
                .AddLootTypeId(type)
                .With(x => x.AddExperience(config.Experience), when: config.Experience > 0)
                .With(x => x.AddStatusSetups(config.StatusSetups), when: !config.StatusSetups.IsNullOrEmpty())
                .With(x => x.AddEffectSetups(config.EffectSetups), when: !config.EffectSetups.IsNullOrEmpty())
                .With(x => x.isPullable = true)
                ;
        }
    }
}
