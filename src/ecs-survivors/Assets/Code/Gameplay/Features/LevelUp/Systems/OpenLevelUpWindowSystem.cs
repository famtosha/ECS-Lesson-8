using Code.Gameplay.Windows;
using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class OpenLevelUpWindowSystem : ReactiveSystem<GameEntity>
    {
        private readonly IWindowService _windows;

        public OpenLevelUpWindowSystem(GameContext context, IWindowService windows) : base(context)
        {
            _windows = windows;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                _windows.Open(WindowId.LevelUpWindow);
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isLevelUp;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.LevelUp.Added());
        }
    }
}
