using UnityEngine;

public class RotationConstante : MonoBehaviour
{

    void Update()
    {
        // Ajuster la rotation de la cam�ra � la rotation d�sir�e
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0, 0f)) ;
    }
}
