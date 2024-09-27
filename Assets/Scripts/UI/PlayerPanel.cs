using PickUps;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerPanel:Window
    {
        [SerializeField] private Slider _moneySlider;
        private PlayerTag _player;
        private Camera _mainCamera;
        private float smoothSpeed = 5f;
        public void Init(IWindowManager windowManager, PlayerTag player, Camera mainCamera)
        {
            base.Init(windowManager);
            _player = player;
            _mainCamera = mainCamera;
        }

        private void LateUpdate()
        {
            if (_player.transform && _mainCamera)
            {
                
                float offsetPosY = _player.transform.position.y + 1.5f;
                Vector3 offsetPos = new Vector3(_player.transform.position.x, offsetPosY, _player.transform.position.z);
                Vector2 canvasPos;
                Vector2 screenPoint = _mainCamera.WorldToScreenPoint(offsetPos);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_manager.WindowRoot,screenPoint ,_mainCamera, out canvasPos);
                Canvas.ForceUpdateCanvases();
                Vector2 smoothPosition = Vector2.Lerp(_moneySlider.transform.localPosition, canvasPos, Time.deltaTime * smoothSpeed);
                _moneySlider.transform.localPosition = smoothPosition;
            }
            else
            {
                Debug.Log($"Component of type {typeof(PlayerPanel).Name} or component of type {typeof(Camera).Name} is null");
            }
        }
        
        public void UpdateSlider(int value, int maxMoneyCount)
        {
            float moneyNormalizedValue = (float)value / maxMoneyCount;
            _moneySlider.value = Mathf.Clamp(moneyNormalizedValue, 0.0F, 1.0F);
        }
    }
}