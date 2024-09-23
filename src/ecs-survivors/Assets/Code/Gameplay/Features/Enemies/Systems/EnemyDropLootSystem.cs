using Code.Gameplay.Common.Random;
using Code.Gameplay.Features.Loot;
using Code.Gameplay.Features.Loot.Factory;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemyDropLootSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ILootFactory _lootFactory;
        private readonly IRandomService _random;
        private readonly IGroup<GameEntity> _enemies;

        public EnemyDropLootSystem(GameContext game, ILootFactory lootFactory, IRandomService random)
        {
            _game = game;
            _lootFactory = lootFactory;
            _random = random;
            _enemies = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.Enemy,
                GameMatcher.WorldPosition,
                GameMatcher.Dead,
                GameMatcher.ProcessingDeath));
        }

        public void Execute()
        {
            foreach (GameEntity enemy in _enemies)
            {
                if (_random.Range(0, 1f) <= 0.15f)
                {
                    _lootFactory.CreateLoot(LootTypeId.HealingItem, enemy.WorldPosition);
                }
                else if (_random.Range(0, 1f) <= 0.30)
                {
                    _lootFactory.CreateLoot(LootTypeId.PoisonEnchantItem, enemy.WorldPosition);
                }
                else if (_random.Range(0, 1f) <= 0.15f)
                {
                    _lootFactory.CreateLoot(LootTypeId.ExplosionEnchantItem, enemy.WorldPosition);
                }
                else
                {
                    _lootFactory.CreateLoot(LootTypeId.ExpGem, enemy.WorldPosition);
                }
            }
        }
    }
}