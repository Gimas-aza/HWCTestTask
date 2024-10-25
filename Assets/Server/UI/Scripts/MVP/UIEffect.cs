using TMPro;
using UnityEngine;

namespace Assets.MVP
{
    public class UIEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _duration;
        [SerializeField] private int _index;

        public int Index => _index;

        public void SetDuration(int duration)
        {
            if (_duration == null)
                Debug.LogError($"Duration is null. Effect: {name}");
            if (duration <= 0)
                gameObject.SetActive(false);
            _duration.text = duration.ToString();
        }
    }
}
