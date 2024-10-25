using System.Collections.Generic;
using Assets.EntryPoint;
using Assets.Entity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.DI;

namespace Assets.MVP
{
    public class View : MonoBehaviour, IInitializer
    {
        [SerializeField] private Button _restartLevel;
        [SerializeField] private List<Image> _abilitiesRecharge;

        private List<UIEffect> _effectsPlayer;
        private List<UIEffect> _effectsEnemy;
        private UIEntity _uiEntityPlayer;
        private UIEntity _uiEntityEnemy;
        private DIContainer _container;
        private UIEvents _uiEvents;

        public void Init(IResolver container)
        {
            _uiEvents = new UIEvents();
            _container = new DIContainer();
            _uiEntityPlayer = container.Resolve<EntityComponent>("Player").UIEntity;
            _uiEntityEnemy = container.Resolve<EntityComponent>("Enemy").UIEntity;
            var presenter = container.Resolve<Presenter>();

            InitUI();

            _container.RegisterInstance(_uiEvents);
            presenter.RegisterEventsForView(_container);
        }

        private void InitUI()
        {
            _effectsPlayer = _uiEntityPlayer.EffectsUI;
            _effectsEnemy = _uiEntityEnemy.EffectsUI;

            _restartLevel.onClick.AddListener(_uiEvents.OnRestartLevel);
            _uiEvents.AbilitiesRecharge += AbilityRecharge;

            _uiEvents.Health += Health;

            _uiEvents.AddEffect += AddEffect;
            _uiEvents.EffectDuration += SetEffectDuration;
            _uiEvents.RemoveEffect += RemoveEffect;
        }

        private void AbilityRecharge(int index, int recharge, int maxRecharge)
        {
            _abilitiesRecharge[index].fillAmount = recharge / (float)maxRecharge;
        }

        private void Health(EntityType entity, int health) 
        {
            if (entity == EntityType.Player)
                _uiEntityPlayer.ChangeHealthUI(health);
            else
                _uiEntityEnemy.ChangeHealthUI(health);
        }

        private void AddEffect(EntityType entity, int effect, int duration)
        {
            if (entity == EntityType.Player)
            {
                _effectsPlayer.Find(e => e.Index == effect).gameObject.SetActive(true);
                _effectsPlayer.Find(e => e.Index == effect).SetDuration(duration);
            }
            else
            {
                _effectsEnemy.Find(e => e.Index == effect).gameObject.SetActive(true);
                _effectsEnemy.Find(e => e.Index == effect).SetDuration(duration);
            }
        }

        private void RemoveEffect(EntityType entity, int effect)
        {
            if (entity == EntityType.Player)
            {
                _effectsPlayer.Find(e => e.Index == effect).gameObject.SetActive(false);
                _effectsPlayer.Find(e => e.Index == effect).SetDuration(0);
            }
            else
            {
                _effectsEnemy.Find(e => e.Index == effect).gameObject.SetActive(false);
                _effectsEnemy.Find(e => e.Index == effect).SetDuration(0);
            }
        }

        private void SetEffectDuration(EntityType entity, int effectIndex, int duration)
        {
            if (entity == EntityType.Player)
                _effectsPlayer.Find(e => e.Index == effectIndex).SetDuration(duration);
            else
                _effectsEnemy.Find(e => e.Index == effectIndex).SetDuration(duration);
        }
    }
}
