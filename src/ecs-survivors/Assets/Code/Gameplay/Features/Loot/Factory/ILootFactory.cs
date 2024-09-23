using UnityEngine;

namespace Code.Gameplay.Features.Loot.Factory
{
    public interface ILootFactory
    {
        GameEntity CreateLoot(LootTypeId type, Vector2 worldPosition);
    }
}