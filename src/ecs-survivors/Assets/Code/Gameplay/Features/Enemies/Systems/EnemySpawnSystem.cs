using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Features.Cooldowns;
using Code.Gameplay.Features.Enemies.Factory;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Systems
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private const float SpawnDistanceGap = 0.5f;

        private readonly ITimeService _time;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGroup<GameEntity> _timers;
        private readonly IGroup<GameEntity> _heroes;
        private readonly List<GameEntity> _buffer = new List<GameEntity>();

        public EnemySpawnSystem(GameContext game, ITimeService time, IEnemyFactory enemyFactory, ICameraProvider cameraProvider)
        {
            _time = time;
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;

            _timers = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.SpawnTimer,
                GameMatcher.SpawnTimerLeft,
                GameMatcher.EnemyTypeId,
                GameMatcher.EnemiesLeftInWave,
                GameMatcher.EnemiesPerWave,
                GameMatcher.CooldownUp));

            _heroes = game.GetGroup(GameMatcher
              .AllOf(
                GameMatcher.Hero,
                GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity hero in _heroes)
            {
                foreach (GameEntity timer in _timers.GetEntities(_buffer))
                {
                    timer.ReplaceSpawnTimerLeft(timer.SpawnTimerLeft - _time.DeltaTime);
                    if (timer.SpawnTimerLeft <= 0)
                    {
                        timer.ReplaceSpawnTimerLeft(timer.SpawnTimer);
                        _enemyFactory.CreateEnemy(timer.EnemyTypeId, at: RandomSpawnPosition(hero.WorldPosition));
                        timer.ReplaceEnemiesLeftInWave(timer.EnemiesLeftInWave - 1);
                        if (timer.EnemiesLeftInWave <= 0)
                        {
                            timer.PutOnCooldown();
                            timer.ReplaceEnemiesLeftInWave(timer.EnemiesPerWave);
                        }
                    }
                }
            }
        }

        private Vector2 RandomSpawnPosition(Vector2 heroWorldPosition)
        {
            bool startWithHorizontal = Random.Range(0, 2) == 0;

            return startWithHorizontal
              ? HorizontalSpawnPosition(heroWorldPosition)
              : VerticalSpawnPosition(heroWorldPosition);
        }

        private Vector2 HorizontalSpawnPosition(Vector2 heroWorldPosition)
        {
            Vector2[] horizontalDirections = { Vector2.left, Vector2.right };
            Vector2 primaryDirection = horizontalDirections.PickRandom();

            float horizontalOffsetDistance = _cameraProvider.WorldScreenWidth / 2 + SpawnDistanceGap;
            float verticalRandomOffset = Random.Range(-_cameraProvider.WorldScreenHeight / 2, _cameraProvider.WorldScreenHeight / 2);

            return heroWorldPosition + primaryDirection * horizontalOffsetDistance + Vector2.up * verticalRandomOffset;
        }

        private Vector2 VerticalSpawnPosition(Vector2 heroWorldPosition)
        {
            Vector2[] verticalDirections = { Vector2.up, Vector2.down };
            Vector2 primaryDirection = verticalDirections.PickRandom();

            float verticalOffsetDistance = _cameraProvider.WorldScreenHeight / 2 + SpawnDistanceGap;
            float horizontalRandomOffset = Random.Range(-_cameraProvider.WorldScreenWidth / 2, _cameraProvider.WorldScreenWidth / 2);

            return heroWorldPosition + primaryDirection * verticalOffsetDistance + Vector2.right * horizontalRandomOffset;
        }
    }
}