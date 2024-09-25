using Code.Gameplay.Common.Time;
using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class StopTimeOnLevelUpSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeService _time;

        public StopTimeOnLevelUpSystem(GameContext context,ITimeService time) : base(context)
        {
            _time = time;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                _time.StopTime();
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
