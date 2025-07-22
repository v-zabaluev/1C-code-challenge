using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.Gameplay
{
    using UnityEngine;
    using System;

    [RequireComponent(typeof(Collider2D))]
    public class ShapeDragHandler : MonoBehaviour
    {
        private bool _dragging;
        private Camera _camera;
        private int _activeTouchId = -1;

        public event Action OnBeginDragAction;
        public event Action OnEndDragAction;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            HandleMouseInput();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryBeginDrag(Input.mousePosition);
            }

            if (_dragging && Input.GetMouseButton(0))
            {
                Vector3 worldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
            }

            if (_dragging && Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }

        private void HandleTouchInput()
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 worldPos = _camera.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (_activeTouchId == -1)
                            TryBeginDrag(touch.position);

                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (_dragging && touch.fingerId == _activeTouchId)
                            transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);

                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (_dragging && touch.fingerId == _activeTouchId)
                        {
                            EndDrag();
                            _activeTouchId = -1;
                        }

                        break;
                }
            }
        }

        private void TryBeginDrag(Vector2 screenPosition)
        {
            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider == GetComponent<Collider2D>())
            {
                _dragging = true;
                _activeTouchId = Input.touchCount > 0 ? Input.GetTouch(0).fingerId : -1;
                OnBeginDragAction?.Invoke();
            }
        }

        private void EndDrag()
        {
            _dragging = false;
            OnEndDragAction?.Invoke();
        }
    }
}