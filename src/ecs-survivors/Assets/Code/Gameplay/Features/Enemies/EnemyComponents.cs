using Code.Gameplay.Features.Enemies.Behaviours;
using Entitas;

namespace Code.Gameplay.Features.Enemies
{
    [Game] public class Enemy : IComponent { }
    [Game] public class EnemyAnimatorComponent : IComponent { public EnemyAnimator Value; }
    [Game] public class SpawnTimer : IComponent { public float Value; }
    [Game] public class SpawnTimerLeft : IComponent { public float Value; }
    [Game] public class EnemyTypeIdComponent : IComponent { public EnemyTypeId Value; }
    [Game] public class EnemiesPerWave : IComponent { public int Value; }
    [Game] public class EnemiesLeftInWave : IComponent { public int Value; }
}