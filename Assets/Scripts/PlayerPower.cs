using System.Collections;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] Collider m_collider;
    [SerializeField] LayerMask m_wallLayer;
    [SerializeField] LayerMask m_nothingLayer;
    string m_currentItem;

    public delegate void OnItemGetDelegate(string itemName);
    public static event OnItemGetDelegate OnItemGet;

    public delegate void OnItemUsedDelegate();
    public static event OnItemUsedDelegate OnItemUsed;

    public bool RecoverCollectable(string collectType)
    {
        if (m_currentItem == null)
        {
            m_currentItem = collectType;
            //OnItemGet.Invoke(m_currentItem);
            Debug.Log("call event OnItemGet");
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


        //OnItemUsed.Invoke();
    }

    IEnumerator InvisibilityBoost()
    {
        Debug.Log("Invisibility Activated");
        m_collider.excludeLayers = m_wallLayer;
        yield return new WaitForSeconds(3f);
        m_collider.excludeLayers = m_nothingLayer;

        //OnItemUsed.Invoke();

        yield return null;
    }

    //=== No Boost ===//
    private void NoBoost()
    {
        Debug.Log("No boost in slot");
    }
}
