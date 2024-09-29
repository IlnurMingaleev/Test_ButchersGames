using Runner.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI
{
    public class PlayerPanel : Window
    {
        [SerializeField] private Slider _moneySlider;
        private readonly float smoothSpeed = 5f;
        private Camera _mainCamera;
        private PlayerTag _player;

        private void LateUpdate()
        {
            if (_player.transform && _mainCamera)
            {
                var offsetPosY = _player.transform.position.y + 1.5f;
                var offsetPos = new Vector3(_player.transform.position.x, offsetPosY, _player.transform.position.z);
                Vector2 canvasPos;
                Vector2 screenPoint = _mainCamera.WorldToScreenPoint(offsetPos);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_manager.WindowRoot, screenPoint, _mainCamera,
                    out canvasPos);
                Canvas.ForceUpdateCanvases();
                var smoothPosition = Vector2.Lerp(_moneySlider.transform.localPosition, canvasPos,
                    Time.deltaTime * smoothSpeed);
                _moneySlider.transform.localPosition = smoothPosition;
            }
            else
            {
                Debug.Log(
                    $"Component of type {typeof(PlayerPanel).Name} or component of type {typeof(Camera).Name} is null");
            }
        }

        public void Init(IWindowManager windowManager, PlayerTag player, Camera mainCamera)
        {
            base.Init(windowManager);
            _player = player;
            _mainCamera = mainCamera;
        }

        public void UpdateSlider(int value, int maxMoneyCount)
        {
            var moneyNormalizedValue = (float) value / maxMoneyCount;
            _moneySlider.value = Mathf.Clamp(moneyNormalizedValue, 0.0F, 1.0F);
        }
    }
}