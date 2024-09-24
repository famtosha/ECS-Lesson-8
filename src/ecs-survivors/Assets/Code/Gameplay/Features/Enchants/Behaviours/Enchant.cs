using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Enchants.Behaviours
{
    public class Enchant : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        private EnchantTypeId _type;

        public EnchantTypeId Type => _type;

        public void Set(EnchantConfig type)
        {
            _type = type.TypeId;
            _icon.sprite = type.Icon;
        }
    }
}
