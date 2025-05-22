using UnityEngine;
using UnityEngine.UI;

public class CuentaAtras : MonoBehaviour
{
    public float totalTime = 10f;
    public TMPro.TMP_Text timerText;

    private float timeLeft;

    public bool iniciar = false;

    void Start()
    {
        timeLeft = (int)PlayerPrefs.GetFloat("tiempo");
    }

    void Update()
    {
        if (iniciar)
        {
            timeLeft -= Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = timeLeft.ToString("0"); // Mostrar el tiempo como entero
            }

            if (timeLeft <= 0)
            {
                // Acción a realizar cuando se acaba el tiempo
                Debug.Log("¡Tiempo agotado!");
                // Puedes detener la cuenta atrás, activar un evento, etc.

                this.GetComponent<UIManagerJuego>().titulo.text="TIEMPO AGOTADO. FIN";
                GameObject.Find("AudioSourceReloj").GetComponent<AudioSource>().Stop();
                iniciar=false;
                
            }
        }
    }
}