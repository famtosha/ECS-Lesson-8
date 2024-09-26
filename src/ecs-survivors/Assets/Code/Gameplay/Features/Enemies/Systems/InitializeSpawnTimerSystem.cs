using Code.Common.Entity;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class InitializeSpawnTimerSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
              .AddSpawnTimer(1)
              .AddSpawnTimerLeft(1)
              .AddEnemyTypeId(EnemyTypeId.GoblinRed)
              .AddEnemiesPerWave(1)
              .AddEnemiesLeftInWave(1)
              .AddCooldown(0.5f)
              .AddCooldownLeft(0.5f);

            CreateEntity.Empty()
              .AddSpawnTimer(0.5f)
              .AddSpawnTimerLeft(0.5f)
              .AddEnemyTypeId(EnemyTypeId.GoblinBlue)
              .AddEnemiesPerWave(5)
              .AddEnemiesLeftInWave(5)
              .AddCooldown(10f)
              .AddCooldownLeft(20f);

            CreateEntity.Empty()
              .AddSpawnTimer(1.25f)
              .AddSpawnTimerLeft(1.25f)
              .AddEnemyTypeId(EnemyTypeId.GoblinYellow)
              .AddEnemiesPerWave(3)
              .AddEnemiesLeftInWave(3)
              .AddCooldown(15f)
              .AddCooldownLeft(30f);
        }
    }
}