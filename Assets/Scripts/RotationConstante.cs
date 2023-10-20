using UnityEngine;

public class RotationConstante : MonoBehaviour
{
    public Transform player;  // R�f�rence au transform du joueur
    [SerializeField] private float rotationX;
    public float rotationSpeed = 5.0f;  // Vitesse de rotation de la cam�ra
    [SerializeField] private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = transform.localPosition;
    }
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x + cameraPosition.x, cameraPosition.y, player.position.z + cameraPosition.z);
            // Obtenir la rotation actuelle de la cam�ra
            Quaternion currentRotation = transform.rotation;

            // Obtenir la rotation actuelle du joueur
            Quaternion targetRotation = player.rotation;

            // Conserver l'angle de rotation de 90 degr�s sur l'axe X
            targetRotation.eulerAngles = new Vector3(rotationX, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Interpoler en douceur vers la nouvelle rotation
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
