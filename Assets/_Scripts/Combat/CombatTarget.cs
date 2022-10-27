using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            // CombatTarget target = hit.transform.GetComponent<CombatTarget>();

            //if target dont have combattarget which is only belongs enemies so continue.
            // if (target == null) continue;

            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                GetComponent<Fighter>().Attack(target.gameObject);
            }

            SetCursor(CursorType.Combat);
            return true;
        }
    }
}