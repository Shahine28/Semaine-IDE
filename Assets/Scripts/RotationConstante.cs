using UnityEngine;

public class RotationConstante : MonoBehaviour
{
    public Transform player;  // Référence au transform du joueur
    public float rotationSpeed = 5.0f;  // Vitesse de rotation de la caméra
    public float cameraHeight;

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, cameraHeight, player.position.z);
            // Obtenir la rotation actuelle de la caméra
            Quaternion currentRotation = transform.rotation;

            // Obtenir la rotation actuelle du joueur
            Quaternion targetRotation = player.rotation;

            // Conserver l'angle de rotation de 90 degrés sur l'axe X
            targetRotation.eulerAngles = new Vector3(90.0f, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);

            // Interpoler en douceur vers la nouvelle rotation
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
