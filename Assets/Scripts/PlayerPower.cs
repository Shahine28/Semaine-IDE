using System.Collections;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] Collider m_collider;

    [Header("Layer Mask")]
    [SerializeField] LayerMask m_laserLayer;
    [SerializeField] LayerMask m_nothingLayer;
    string m_currentItem;

    public delegate void OnItemGetDelegate(string itemName, string playerTag);
    public static event OnItemGetDelegate OnItemGet;

    public delegate void OnItemUsedDelegate(string playerTag);
    public static event OnItemUsedDelegate OnItemUsed;

    public bool RecoverCollectable(string collectType)
    {
        if (m_currentItem == null)
        {
            m_currentItem = collectType;
            OnItemGet.Invoke(m_currentItem, transform.tag);
            Debug.Log("Item Set " + m_currentItem);
            return true;
        }
        else
        {
            Debug.Log("No Item Set");
            return false;
        }
    }

    public void ItemUse()
    {
        if (m_currentItem != null)
        {
            switch (m_currentItem)
            {
                case "Dash":
                    DashBoost();
                    break;

                case "Invisibility":
                    StartCoroutine(InvisibilityBoost());
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
    private void DashBoost()
    {
        Debug.Log("Dash Activated");

        m_currentItem = null;
        OnItemUsed.Invoke(transform.tag);
    }

    IEnumerator InvisibilityBoost()
    {
        Debug.Log("Invisibility Activated");
        m_collider.excludeLayers = m_laserLayer;
        yield return new WaitForSeconds(3f);
        m_collider.excludeLayers = m_nothingLayer;

        m_currentItem = null;
        OnItemUsed.Invoke(transform.tag);

        yield return null;
    }

    //=== No Boost ===//
    private void NoBoost()
    {
        Debug.Log("No boost in slot");
    }
}
