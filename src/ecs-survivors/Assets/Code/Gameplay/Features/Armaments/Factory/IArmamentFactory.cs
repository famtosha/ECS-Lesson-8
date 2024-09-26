using Code.Gameplay.Features.Abilities;
using UnityEngine;

namespace Code.Gameplay.Features.Armaments.Factory
{
  public interface IArmamentFactory
  {
    GameEntity CreateVegetableBolt(int level, Vector3 at);
    GameEntity CreateMushroom(int level, Vector3 at, float phase);
    GameEntity CreateEffectAura(AbilityId parentAbilityId, int producerId, int level);
    GameEntity CreateExplosion(int producerId, Vector3 at);
        GameEntity CreateBomb(Vector3 at, Vector3 target, int level);
        GameEntity CreateBombZone(Vector3 at, int level, int producerId);
    }
}