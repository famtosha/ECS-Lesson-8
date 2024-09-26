using System;
using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.CharacterStats;
using Code.Gameplay.Features.Effects;
using Code.Infrastructure.Identifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IIdentifierService _identifiers;

        public EnemyFactory(IIdentifierService identifiers)
        {
            _identifiers = identifiers;
        }

        public GameEntity CreateEnemy(EnemyTypeId typeId, Vector3 at)
        {
            switch (typeId)
            {
                case EnemyTypeId.GoblinRed:
                    return CreateGoblinRed(at);

                case EnemyTypeId.GoblinBlue:
                    return CreateGoblinBlue(at);

                case EnemyTypeId.GoblinYellow:
                    return CreateGoblinYellow(at);
            }

            throw new Exception($"Enemy with type id {typeId} does not exist");
        }

        private GameEntity CreateGoblinRed(Vector2 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                .With(x => x[Stats.Speed] = 1f)
                .With(x => x[Stats.MaxHp] = 3)
                .With(x => x[Stats.Damage] = 1);

            return
                CreateGoblin(at, baseStats)
                .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_red");
        }

        private GameEntity CreateGoblinYellow(Vector2 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                .With(x => x[Stats.Speed] = 3)
                .With(x => x[Stats.MaxHp] = 3)
                .With(x => x[Stats.Damage] = 1);

            return
                CreateGoblin(at, baseStats)
                .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_yellow");
        }

        private GameEntity CreateGoblinBlue(Vector2 at)
        {
            Dictionary<Stats, float> baseStats = InitStats.EmptyStatDictionary()
                .With(x => x[Stats.Speed] = 0.8f)
                .With(x => x[Stats.MaxHp] = 10)
                .With(x => x[Stats.Damage] = 2);

            return
                CreateGoblin(at, baseStats)
                .AddViewPath("Gameplay/Enemies/Goblins/Torch/goblin_torch_blue");
        }

        private GameEntity CreateGoblin(Vector2 at, Dictionary<Stats, float> baseStats)
        {
            return CreateEntity.Empty()
                            .AddId(_identifiers.Next())
                            .AddEnemyTypeId(EnemyTypeId.GoblinRed)
                            .AddWorldPosition(at)
                            .AddDirection(Vector2.zero)
                            .AddBaseStats(baseStats)
                            .AddStatModifiers(InitStats.EmptyStatDictionary())
                            .AddSpeed(baseStats[Stats.Speed])
                            .AddCurrentHp(baseStats[Stats.MaxHp])
                            .AddMaxHp(baseStats[Stats.MaxHp])
                            .AddEffectSetups(new List<EffectSetup> { new EffectSetup() { EffectTypeId = EffectTypeId.Damage, Value = baseStats[Stats.Damage] } })
                            .AddRadius(0.3f)
                            .AddTargetBuffer(new List<int>(1))
                            .AddCollectTargetsInterval(0.5f)
                            .AddCollectTargetsTimer(0f)
                            .AddLayerMask(CollisionLayer.Hero.AsMask())
                            .With(x => x.isEnemy = true)
                            .With(x => x.isTurnedAlongDirection = true)
                            .With(x => x.isMovementAvailable = true);
        }
    }
}