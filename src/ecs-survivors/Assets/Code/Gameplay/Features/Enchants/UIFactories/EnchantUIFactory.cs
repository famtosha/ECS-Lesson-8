using Code.Gameplay.Features.Enchants.Behaviours;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.UIFactories
{
    public class EnchantUIFactory : IEnchantUIFactory
    {
        private const string PrefabPath = "UI/Enchants/Enchant";
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _configs;

        public EnchantUIFactory(IInstantiator instantiator, IAssetProvider assets, IStaticDataService configs)
        {
            _instantiator = instantiator;
            _assets = assets;
            _configs = configs;
        }

        public Enchant CreateEnchant(Transform parent, EnchantTypeId type)
        {
            var config = _configs.GetEnchantConfig(type);
            var enchant = _instantiator.InstantiatePrefabForComponent<Enchant>(_assets.LoadAsset<Enchant>(PrefabPath), parent);
            enchant.Set(config);
            return enchant;
        }
    }
}
