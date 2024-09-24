using Code.Gameplay.Features.LevelUp.Behaviours;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Code.Gameplay.Features.LevelUp.Registrars
{
    public class ExperienceMeterRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private ExperienceMeter _meter;

        public override void RegisterComponents()
        {
            Entity.AddExperienceMeter(_meter);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasExperienceMeter)
            {
                Entity.RemoveExperienceMeter();
            }
        }
    }
}
