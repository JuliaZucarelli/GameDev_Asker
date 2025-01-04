using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Image barraDeVida;
    public float vidaAtual = 100f;
    public float vidaMaxima = 100f;

    public void ReduzirVida(float dano)
    {
        Debug.Log($"Vida antes: {vidaAtual}");
        vidaAtual -= dano;
        Debug.Log($"Vida depois: {vidaAtual}");
        if (vidaAtual < 0) vidaAtual = 0;
        AtualizarBarra();
    }

    private void AtualizarBarra()
    {
        barraDeVida.fillAmount = vidaAtual / vidaMaxima;
    }
}
