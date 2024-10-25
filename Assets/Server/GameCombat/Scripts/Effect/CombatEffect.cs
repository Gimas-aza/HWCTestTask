using Assets.DI;

namespace Assets.GameCombat.Effect
{
    public abstract class CombatEffect
    {
        public int Duration { get; set; }
        public int MaxDuration { get; set; }
        public bool IsFlaggedForUndo { get; private set; }

        public abstract int Index { get; }

        public abstract void Execute(DIContainer container);

        public void SetFlagForUndoEffect()
        {
            IsFlaggedForUndo = true;
            Duration = 0;
        }
    }
}