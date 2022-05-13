using UnityEngine;

namespace _Scripts.Units
{
    public class CharactersRendererSort : MonoBehaviour
    {
        private Components.Components components;

        private int sortingOrderBase = 100;
        private int offset = 0;

        private float _timer;
        private float _timerMax = .3f;

        private MeshRenderer _renderer;

        private void Awake()
        {
            components = GetComponentInParent<Components.Components>();
            _renderer = GetComponent<MeshRenderer>();
        }

        private void LateUpdate()
        {
            if (components.Handler.IsDeath())
            {
                _renderer.sortingOrder = 20;
                Destroy(this);
            }

            if (components.isMoving)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    _timer = _timerMax;
                    _renderer.sortingOrder = (int) (sortingOrderBase - transform.position.y - offset);
                }
            }
        }
    }
}