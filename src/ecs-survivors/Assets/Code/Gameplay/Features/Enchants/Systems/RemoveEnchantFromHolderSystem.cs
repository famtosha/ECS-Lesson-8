using Entitas;
using System.Collections.Generic;

namespace Code.Gameplay.Features.Enchants.Systems
{
    public class RemoveEnchantFromHolderSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _holders;

        public RemoveEnchantFromHolderSystem(GameContext game) : base(game)
        {
            _holders = game.GetGroup(GameMatcher
                .AllOf(
                GameMatcher.EnchantHolder));
        }

        protected override void Execute(List<GameEntity> enchants)
        {
            foreach (GameEntity enchant in enchants)
            {
                foreach (GameEntity holder in _holders)
                {
                    holder.EnchantHolder.RemoveEnchant(enchant.EnchantTypeId);
                }
            }
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher
                .AllOf(
                GameMatcher.EnchantTypeId,
                GameMatcher.Unapplied)
                .Added());
        }
    }
}
