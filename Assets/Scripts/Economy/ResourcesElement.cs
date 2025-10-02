using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    public class ResourcesElement : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _counterText;
        [SerializeField] Image _collectIcon;

        private float _showCollectionDuration = 0.5f;
        private float _showCollectionTimer = 0;
        private void Update()
        {
            _showCollectionTimer -= Time.deltaTime;

            if (_showCollectionTimer <= 0)
            {
                _collectIcon.gameObject.SetActive(false);
            }
        }

        public void UpdatePresentation(int newValue)
        {
            _counterText.text = newValue.ToString();
            _showCollectionTimer = _showCollectionDuration;
            _collectIcon.gameObject.SetActive(true);
        }
        public void ResetElements()
        {
            _counterText.text = 0.ToString();
            _collectIcon.gameObject.SetActive(false);
        }       
    }
}

