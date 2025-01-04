using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidade normal
    public float sprintMultiplier = 2f; // Multiplicador de velocidade ao correr
    public float jumpForce = 5f; // Força do pulo
    private bool isGrounded; // Verifica se está no chão
    private bool canDoubleJump; // Verifica se pode realizar o segundo pulo
    private bool doubleJumpUnlocked = false; // Indica se o duplo pulo foi adquirido
    private Rigidbody rb;
    private bool isSprinting = false; // Verifica se o sprint está ativo

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Movimento horizontal e vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Verificar se o Shift está pressionado
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (!isSprinting) // Apenas logar quando o sprint começar
            {
                isSprinting = true;
                Debug.Log("Botão Shift: Ativado. \n Velocidade aumentada: " + (speed * sprintMultiplier) + ".");
            }
            currentSpeed *= sprintMultiplier;
        }
        else if (isSprinting)
        {
            isSprinting = false;
            Debug.Log("Botão Shift: Desativado. \n Velocidade normal: " + speed + ".");
        }

        // Calcular direção de movimento
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical).normalized * currentSpeed * Time.deltaTime;

        // Aplicar movimento com Rigidbody
        rb.MovePosition(rb.position + transform.TransformDirection(movement));
    }

    void Update()
    {
        // Pulo e Duplo Pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true; // Após o primeiro pulo, permite o duplo pulo
            }
            else if (doubleJumpUnlocked && canDoubleJump)
            {
                Jump();
                canDoubleJump = false; // Após o segundo pulo, desativa o duplo pulo
            }
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reseta a velocidade vertical antes de aplicar o pulo
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Aplica a força do pulo
    }

    public void EnableDoubleJump()
    {
        doubleJumpUnlocked = true; 
        Debug.Log("Duplo pulo adquirido!");
        StartCoroutine(DisableDoubleJumpAfterTime(10f)); // Habilita o duplo pulo por 10 segundos
    }

    private IEnumerator DisableDoubleJumpAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        doubleJumpUnlocked = false;
        Debug.Log("Duplo pulo desativado!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}