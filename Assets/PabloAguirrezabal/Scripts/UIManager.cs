using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class UIManager : MonoBehaviour
{

    public TMPro.TMP_Text tiempo;
    public TMPro.TMP_Text vertical;
    public TMPro.TMP_Text horizontal;
    // Start is called before the first frame update
    public Slider sliderTiempo;
    public Slider sliderVertical;
    public Slider sliderHorizontal;

    public Toggle tOclusion;

    void Start()
    {
        if (sliderTiempo != null)
        {

            sliderTiempo.onValueChanged.AddListener(tiempoChanged);
        }
        if (sliderVertical != null)
        {

            sliderVertical.onValueChanged.AddListener(verticalChanged);
        }
        if (sliderHorizontal != null)
        {

            sliderHorizontal.onValueChanged.AddListener(horizontalChanged);
        }
    }

    // Este es el método que se suscribirá al evento del Slider.
    // Debe aceptar un parámetro de tipo float.
    void tiempoChanged(float valor)
    {
        tiempo.text="TIEMPO BUSQUEDA: "+valor.ToString();
        PlayerPrefs.SetFloat("tiempo",valor);
    }

    public void verticalChanged(float valor)
    {
        vertical.text="GEMAS EN VERTICAL: "+valor.ToString();
        PlayerPrefs.SetFloat("vertical",valor);
    }

    public void horizontalChanged(float valor)
    {
        horizontal.text="GEMAS EN HORIZONTAL: "+valor.ToString();
        PlayerPrefs.SetFloat("horizontal",valor);
    }

    public void ComenzarPulsado()
    {
        PlayerPrefs.SetFloat("oclusion",tOclusion.isOn  ? 1.0f : 0.0f);



        // UnityEngine.XR.ARFoundation.ARSession currentSession = FindObjectOfType<UnityEngine.XR.ARFoundation.ARSession>();
        // DontDestroyOnLoad(currentSession.gameObject);
        // Unity.XR.CoreUtils.XROrigin oldOrigin = FindObjectOfType<Unity.XR.CoreUtils.XROrigin>();
        // DontDestroyOnLoad(oldOrigin.gameObject);

        // XRGeneralSettings instance = XRGeneralSettings.Instance;
        // if (instance != null)
        // {
        //     instance.Manager.DeinitializeLoader();
        //     instance.Manager.StopSubsystems();
        // }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }


}
