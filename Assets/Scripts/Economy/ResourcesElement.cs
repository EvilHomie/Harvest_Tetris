using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    public class ResourcesElement : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _counterText;
        [SerializeField] Image _collectIcon;
        [SerializeField] Image _spendIcon;

        private float _showCollectionDuration = 0.5f;
        private float _showCollectionTimer = 0;

        private float _showSpendDuration = 0.5f;
        private float _showSpendTimer = 0;
        private void Update()
        {
            _showCollectionTimer -= Time.deltaTime;
            _showSpendTimer -= Time.deltaTime;

            if (_showCollectionTimer <= 0)
            {
                _collectIcon.gameObject.SetActive(false);
            }

            if (_showSpendTimer <= 0)
            {
                _spendIcon.gameObject.SetActive(false);
            }
        }

        public void UpdatePresentation(int newValue, bool isAdded)
        {
            _counterText.text = newValue.ToString();

            if (isAdded)
            {
                _showCollectionTimer = _showCollectionDuration;
                _collectIcon.gameObject.SetActive(true);
            }
            else
            {
                _showSpendTimer = _showSpendDuration;
                _spendIcon.gameObject.SetActive(true);
            }
        }
        public void ResetElements()
        {
            _counterText.text = 0.ToString();
            _collectIcon.gameObject.SetActive(false);
            _spendIcon.gameObject.SetActive(false);
        }
    }
}

