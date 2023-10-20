using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlotBehavior : MonoBehaviour
{
    [SerializeField] GameObject m_slot;
    [SerializeField] GameObject m_dashIcon;
    [SerializeField] GameObject m_invisibilityIcon;

    private void OnEnable()
    {
        PlayerPower.OnItemGet += ItemGet;
        PlayerPower.OnItemUsed += ItemUsed;
    }
    private void OnDisable()
    {
        PlayerPower.OnItemGet -= ItemGet;
        PlayerPower.OnItemUsed -= ItemUsed;
    }

    private void Start()
    {
        m_slot.SetActive(true);

        m_dashIcon.SetActive(false);
        m_invisibilityIcon.SetActive(false);
    }

    private void ItemGet(string item, string playerTag)
    {
        if (transform.CompareTag(playerTag))
        {
            switch (item)
            {
                case "Dash":
                    m_dashIcon.SetActive(true);
                    break;

                case "Invisibility":
                    m_invisibilityIcon.SetActive(true);
                    break;

                default:
                    Debug.LogError("Wrongly Specified ItemType");
                    break;
            }
        }
    }

    private void ItemUsed(string playerTag)
    {
        if (transform.CompareTag(playerTag))
        {
            m_dashIcon.SetActive(false);
            m_invisibilityIcon.SetActive(false);
        }
    }
}
