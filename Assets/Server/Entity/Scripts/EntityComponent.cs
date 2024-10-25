using System;
using System.Collections.Generic;
using Assets.DI;
using Assets.EntryPoint;
using Assets.GameCombat;
using Assets.GameCombat.Effect;
using Assets.MVP;
using UnityEngine;

namespace Assets.Entity
{
    public class EntityComponent : MonoBehaviour, IInitializer, IModel, IGetDamage, IHealable, IDamageBlocker, IEffectManager 
    {
        [SerializeField] private CombatSystem _combatSystem;
        [SerializeField] private UIEntity _uiEntity;

        private DIContainer _container;
        private List<CombatEffect> _effects;
        private int _maxHealth = 100;
        private int _health;
        private int _mitigateDamage = 0;
        private UIEvents _uiEvents;
        private bool _isCanAction = false;

        public EntityType EntityType;
        public UIEntity UIEntity => _uiEntity;
        public CombatSystem CombatSystem => _combatSystem;
        public bool IsCanAction
        {
            get => _isCanAction;
            set
            {
                _isCanAction = value;
                if (value == false)
                {
                    CanAction?.Invoke(value);
                }
            }
        }

        public event Action<bool> CanAction;
        public event Action OnDead;

        public void Init(IResolver container)
        {
            _container = new DIContainer();
            _effects = new List<CombatEffect>();
            _health = _maxHealth;

            Registers(container);
            _combatSystem.Init(_container);
        }

        public void SubscribeToEvents(IResolver container)
        {
            _uiEvents = container.Resolve<UIEvents>();

            _container.RegisterInstance(_uiEvents);
            _container.RegisterInstance(EntityType);
        }

        public void UpdateEveryTurn()
        {
            _combatSystem.UpdateEveryTurn();
            _mitigateDamage = 0;
            foreach (var effect in _effects)
            {
                effect.Execute(_container);
            }
            CleanEffects();
        }

        public void TakeDamage(int damage)
        {
            var currentDamage = Math.Max(0, damage - _mitigateDamage);
            if (_health - currentDamage <= 0) 
            {
                OnDead?.Invoke(); 
                _health = 0;
                _uiEvents.OnHealth(EntityType, _health);
                return;
            }

            _health -= currentDamage;
            _uiEvents.OnHealth(EntityType, _health);
        }

        public void Heal(int heal)
        {
            if (_health + heal >= _maxHealth) 
            {
                _health = _maxHealth;
                _uiEvents.OnHealth(EntityType, _health);
                return;
            }

            _health += heal;
            _uiEvents.OnHealth(EntityType, _health);
        }

        public void SetBlockDamage(int value)
        {
            _mitigateDamage = value;
        }

        public void AddEffect(CombatEffect effect)
        {
            _effects.Add(effect);
            _uiEvents.OnAddEffect(EntityType, effect.Index, effect.Duration);
        }

        public void RemoveEffect(Type effectType)
        {
            var effect = _effects.Find(effect => effect.GetType() == effectType);
            if (effect == null) return;

            RemoveEffect(effect);
        }

        public void RemoveEffect(CombatEffect currentEffect)
        {
            var effect = _effects.Find(effect => effect.Index == currentEffect.Index);
            if (effect == null) return;
            
            _effects.Remove(effect);
            _uiEvents.OnRemoveEffect(EntityType, effect.Index);
        }

        private void CleanEffects()
        {
            _effects.RemoveAll(effect =>
            {
                if (effect.IsFlaggedForUndo)
                {
                    _uiEvents.OnRemoveEffect(EntityType, effect.Index);
                    return true;
                }
                return false;
            });
        }

        private EntityComponent SetEnemy(IResolver container)
        {
            if (EntityType == EntityType.Enemy)
                return container.Resolve<EntityComponent>(nameof(EntityType.Player));
            else
                return container.Resolve<EntityComponent>(nameof(EntityType.Enemy));
        }

        private void Registers(IResolver container)
        {
            _container.RegisterInstance(this as IGetDamage);
            _container.RegisterInstance(this as IHealable);
            _container.RegisterInstance(this as IDamageBlocker);
            _container.RegisterInstance(this as IEffectManager);
            _container.RegisterSingleton(nameof(EntityType.Enemy), (c) => {
                return SetEnemy(container) as IGetDamage;
            });
            _container.RegisterSingleton(nameof(EntityType.Enemy), (c) => {
                return SetEnemy(container) as IEffectManager;
            });
        }
    }
}
