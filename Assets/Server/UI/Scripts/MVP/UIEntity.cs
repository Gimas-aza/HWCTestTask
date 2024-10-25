using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.MVP
{
    public class UIEntity : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthUI;

        public List<UIEffect> EffectsUI;

        public void ChangeHealthUI(int health)
        {
            _healthUI.text = "Жизни: " + health.ToString();
        }
    }
}