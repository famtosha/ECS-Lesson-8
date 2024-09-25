using Code.Gameplay.Common.Time;
using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.LevelUp.Systems
{
    public class StartTimeOnLevelUpSystem : ReactiveSystem<GameEntity>
    {
        private readonly ITimeService _time;

        public StartTimeOnLevelUpSystem(GameContext context,ITimeService time) : base(context)
        {
            _time = time;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                _time.StartTime();
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isLevelUp && entity.isProcessed;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Processed.Added());
        }
    }
}
