using Code.Gameplay.Features.Enchants.Behaviours;
using Code.Infrastructure.View.Registrars;
using UnityEngine;

namespace Assets.Code.Gameplay.Features.Enchants.Registrars
{
    public class EnchantHolderRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private EnchantHolder _holder;

        public override void RegisterComponents()
        {
            Entity.AddEnchantHolder(_holder);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasEnchantHolder)
            {
                Entity.RemoveEnchantHolder();
            }
        }
    }
}
