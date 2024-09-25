using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Configs;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.LevelUp.Behaviours
{
    public class AbilityCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private Button _button;
        [SerializeField] private GameObject _stamp;

        private Action<AbilityId> _callback;

        public AbilityId AbilityId { get; private set; }

        public void Setup(AbilityId id, AbilityLevel level, Action<AbilityId> callback)
        {
            AbilityId = id;
            _icon.sprite = level.Icon;
            _description.text = level.Description;
            _callback = callback;
            _button.onClick.AddListener(SelectCard);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void SelectCard()
        {
            StartCoroutine(SelectRoutine());
            _callback?.Invoke(AbilityId);
        }

        private IEnumerator SelectRoutine()
        {
            _stamp.SetActive(true);
            yield return new WaitForSeconds(1);
            _callback?.Invoke(AbilityId);
        }
    }
}
