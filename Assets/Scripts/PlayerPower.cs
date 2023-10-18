using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    string m_currentItem;

    public delegate void OnItemGetDelegate(string itemName);
    public static event OnItemGetDelegate OnItemGet;

    public bool RecoverCollectable(string collectType)
    {
        if (m_currentItem == null)
        {
            m_currentItem = collectType;
            OnItemGet.Invoke(m_currentItem);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ItemUse(string m_currentItem)
    {
        if (m_currentItem != null)
        {
            switch (m_currentItem)
            {
                case "Jump":
                    JumpBoost();
                    break;

                case "Speed":
                    SpeedBoost();
                    break;

                case "Invisibility":
                    InvisibilityBoost();
                    break;

                default:
                    Debug.LogError("Wrongly Specified ItemType");
                    break;
            }
        }
        else
        {
            NoBoost();
        }
    }

    //=== Boost Action ===//
    private void JumpBoost()
    {
        Debug.Log("Jump Activated");
    }

    private void SpeedBoost()
    {
        Debug.Log("Speed Activated");
    }

    private void InvisibilityBoost()
    {
        Debug.Log("Invisibility Activated");
    }

    //=== No Boost ===//
    private void NoBoost()
    {
        Debug.Log("No boost in slot");
    }
}
