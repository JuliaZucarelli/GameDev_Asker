using UnityEngine;

public class Coin : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Algo colidiu com a moeda: {other.gameObject.name} com a tag {other.tag}");
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponentInParent<PlayerMovement>(); // Procura no objeto pai
            if (playerMovement == null)
            {
                playerMovement = other.GetComponentInChildren<PlayerMovement>(); // Procura nos filhos
            }

            if (playerMovement != null)
            {
                Debug.Log("PlayerMovement encontrado. Ativando pulo duplo.");
                playerMovement.EnableDoubleJump();
            }
            else
            {
                Debug.Log("PlayerMovement não encontrado na hierarquia do objeto Player.");
            }

            Destroy(gameObject);
        }
    }
}