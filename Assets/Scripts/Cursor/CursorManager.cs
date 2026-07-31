using Assets.Scripts.Transition;
using UnityEngine;

namespace Assets.Scripts.Cursor
{
    public class CursorManager : MonoBehaviour
    {
        private Vector3 MouseWorldPos => 
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private bool _canClick;

        private void Update()
        {
            _canClick = GetMouseOverCollider() != null;

            if(_canClick && Input.GetMouseButtonDown(0))
            {
                // 处理点击事件
                var collider = GetMouseOverCollider();
                if (collider != null)
                {
                    OnClick(collider.gameObject);
                }
            }
        }

        private void OnClick(GameObject obj)
        {
            switch (obj.tag)
            {
                case "Teleport":
                    // 处理传送点击
                    var teleport = obj.GetComponent<Teleport>();
                    teleport.OnTeleport();
                    break;
            }
        }

        // 获取鼠标点击位置的碰撞体
        private Collider2D GetMouseOverCollider()
        {
            return Physics2D.OverlapPoint(MouseWorldPos);
        }
    }
}
