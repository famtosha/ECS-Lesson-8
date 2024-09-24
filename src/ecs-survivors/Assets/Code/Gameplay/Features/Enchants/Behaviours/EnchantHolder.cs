using Code.Gameplay.Features.Enchants.UIFactories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Enchants.Behaviours
{
    public class EnchantHolder : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        private IEnchantUIFactory _factory;

        private List<Enchant> _enchants = new List<Enchant>();

        [Inject]
        private void Construct(IEnchantUIFactory factory)
        {
            _factory = factory;
        }

        public void AddEnchant(EnchantTypeId type)
        {
            if (_enchants.FirstOrDefault(x => x.Type == type)) return;

            var enchant = _factory.CreateEnchant(_container, type);
            _enchants.Add(enchant);
        }

        public void RemoveEnchant(EnchantTypeId type)
        {
            var instance = _enchants.FirstOrDefault(x => x.Type == type);
            if (instance != null)
            {
                _enchants.Remove(instance); 
                Destroy(instance.gameObject); // 3, 00:00
            }
        }
    }
}
