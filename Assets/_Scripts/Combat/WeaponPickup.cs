using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                PickUp(GetComponent<Fighter>());
            }
        }

        private void PickUp(Fighter fighter)
        {
            fighter.GetComponent<Fighter>().EquipWeapon(weapon);
            StartCoroutine(HideForSecond(respawnTime));
        }

        IEnumerator HideForSecond(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

        private void ShowPickUp(bool shouldShow)
        {
            GetComponent<SphereCollider>().enabled = shouldShow;
            transform.GetChild(0).gameObject.SetActive(shouldShow);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        private void HidePickUps()
        {
            gameObject.SetActive(false);
        }

        public CursorType GetCursorType()
        {
            return CursorType.PickUp;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PickUp(callingController.GetComponent<Fighter>());
            }

            return true;
        }
    }
}