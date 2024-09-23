using Code.Gameplay.Features.Effects;
using Code.Gameplay.Features.Statuses;
using Code.Infrastructure.View;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Loot.Configs
{
    [CreateAssetMenu(fileName = nameof(LootConfig), menuName = "ECS Survivors/LootConfig")]
    public class LootConfig : ScriptableObject
    {
        [field: SerializeField] public LootTypeId LootTypeId { get; private set; }
        [field: SerializeField] public float Experience { get; private set; }
        [field: SerializeField] public EntityBehaviour Prefab { get; private set; }

        [field: SerializeField] public List<EffectSetup> EffectSetups { get; private set; }
        [field: SerializeField] public List<StatusSetup> StatusSetups { get; private set; }
    }
}
