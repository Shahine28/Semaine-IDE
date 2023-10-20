using UnityEngine;

public class RotationConstante : MonoBehaviour
{

    void Update()
    {
        // Ajuster la rotation de la caméra à la rotation désirée
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0, 0f)) ;
    }
}
