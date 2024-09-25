using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.LevelUp
{
    [CreateAssetMenu(fileName = nameof(LevelUpConfig), menuName = "ECS Survivors/LevelUpConfig")]
    public class LevelUpConfig : ScriptableObject
    {
        [field: SerializeField] public List<float> experienceForLevels { get; private set; }

        public int maxlevel => experienceForLevels.Count;
    }
}
