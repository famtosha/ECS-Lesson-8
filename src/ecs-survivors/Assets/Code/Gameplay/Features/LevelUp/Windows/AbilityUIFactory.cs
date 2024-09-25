using Code.Gameplay.Features.LevelUp.Behaviours;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class AbilityUIFactory : IAbilityUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _provider;

        public AbilityUIFactory(IInstantiator instantiator, IAssetProvider provider)
        {
            _instantiator = instantiator;
            _provider = provider;
        }

        public AbilityCard CreateAbilityCard(Transform parent)
        {
            var prefab = _provider.LoadAsset<AbilityCard>("UI/Abilities/AbilityCard");
            var instance = _instantiator.InstantiatePrefabForComponent<AbilityCard>(prefab, parent);
            return instance;
        }
    }
}
