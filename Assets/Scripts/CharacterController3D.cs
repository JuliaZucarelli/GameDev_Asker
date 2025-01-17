using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    public float moveSpeed = 5f;        // Velocidade de movimento
    public float jumpForce = 5f;       // Força do pulo
    public Transform cameraTransform;  // Referência à câmera
    private Rigidbody rb;              // Referência ao Rigidbody
    private bool isGrounded;           // Verifica se o personagem está no chão

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtém o Rigidbody do personagem
    }

    void Update()
    {
        MoveCharacter();
        RotateCharacterToCamera();

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void MoveCharacter()
    {
        // Obtém entrada de teclado para movimento
        float horizontal = Input.GetAxis("Horizontal"); // Movimento no eixo X
        float vertical = Input.GetAxis("Vertical");     // Movimento no eixo Z

        // Calcula a direção do movimento com base na rotação da câmera
        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0f; // Garante que o movimento ocorra apenas no plano XZ

        // Move o personagem
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    void RotateCharacterToCamera()
    {
        // Calcula a direção para onde o personagem deve olhar, com base na câmera
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0f; // Evita que o personagem incline para cima/baixo

        // Faz a rotação suave em direção à câmera
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica se o personagem está no chão
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Quando o personagem sai do chão
        isGrounded = false;
    }
}