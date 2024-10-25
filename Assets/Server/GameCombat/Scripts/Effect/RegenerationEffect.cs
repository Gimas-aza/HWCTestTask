using Assets.DI;
using Assets.Entity;
using Assets.MVP;

namespace Assets.GameCombat.Effect
{
    public class RegenerationEffect : CombatEffect
    {
        public int Heal { get; set; }
        public override int Index { get; } = 2;

        public override void Execute(DIContainer container)
        {
            var entity = container.Resolve<EntityType>();
            var uiEvents = container.Resolve<UIEvents>();

            if (Duration <= 0)
            {
                SetFlagForUndoEffect();
                return;
            }
            
            container.Resolve<IHealable>().Heal(Heal);
            uiEvents.OnEffectDuration(entity, Index, Duration);
            Duration--;
        }
    }
}