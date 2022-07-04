using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform player;
        private void LateUpdate()
        {
            transform.position = player.position;//SmoothDamp lazim
        }
    }
}