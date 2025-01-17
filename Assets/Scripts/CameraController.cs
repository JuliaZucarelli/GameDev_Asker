using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;       // Referência ao personagem
    public Transform cameraTarget; // Referência ao ponto de foco da câmera
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento
    public Vector3 offset;         // Offset inicial da câmera
    public float rotationSpeed = 100f; // Velocidade de rotação da câmera

    private float currentAngle = 0f; // Ângulo atual da câmera

    void LateUpdate()
    {
        HandleCameraRotation();
        FollowPlayer();
    }

    void HandleCameraRotation()
    {
        // Captura o movimento horizontal do mouse
        float horizontalInput = Input.GetAxis("Mouse X");

        // Ajusta o ângulo atual com base na entrada do mouse
        currentAngle += horizontalInput * rotationSpeed * Time.deltaTime;

        // Calcula a nova posição da câmera ao redor do personagem
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 newPosition = cameraTarget.position + rotation * offset;

        // Atualiza a posição da câmera
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        // Garante que a câmera esteja sempre olhando para o personagem
        transform.LookAt(cameraTarget);
    }

    void FollowPlayer()
    {
        // Mantém a câmera suavemente alinhada ao personagem
        Vector3 desiredPosition = cameraTarget.position + Quaternion.Euler(0, currentAngle, 0) * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}