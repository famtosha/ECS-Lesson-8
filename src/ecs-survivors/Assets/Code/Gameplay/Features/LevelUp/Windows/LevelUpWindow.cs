using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Features.Abilities;
using Code.Gameplay.Features.Abilities.Upgrade;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.LevelUp.Windows
{
    public class LevelUpWindow : BaseWindow
    {
        [SerializeField] private Transform _container;

        private IAbilityUIFactory _factory;
        private IAbilityUpgradeService _upgradeService;
        private IStaticDataService _configs;
        private IWindowService _windows;

        [Inject]
        private void Construct(IAbilityUIFactory factory, IAbilityUpgradeService upgradeService, IStaticDataService configs, IWindowService windows)
        {
            Id = WindowId.LevelUpWindow;

            _factory = factory;
            _upgradeService = upgradeService;
            _configs = configs;
            _windows = windows;
        }

        protected override void Initialize()
        {
            foreach (var item in _upgradeService.GetUpgradeOptions())
            {
                var level = _configs.GetAbilityLevel(item.Id, item.Level);

                _factory
                    .CreateAbilityCard(_container)
                    .Setup(item.Id, level, OnSelected);
            }
        }

        private void OnSelected(AbilityId id)
        {
            CreateEntity
                .Empty()
                .AddAbilityId(id)
                .With(x => x.isUpgradeRequest = true);

            _windows.Close(Id);
        }
    }
}
