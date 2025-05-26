using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;


public class UIManagerJuego : MonoBehaviour
{   

    private int gemasEncontradas =0;
    public GameObject bCrear;
    public int planosHencontrados;
    public int planosVencontrados;

    
    public TMPro.TMP_Text vertical;
    public TMPro.TMP_Text horizontal;

    public TMPro.TMP_Text titulo;
    // Start is called before the first frame update
    private bool crearPulsado =false;

    void Start()
    {
        
        Debug.Log(PlayerPrefs.GetFloat("tiempo"));
        Debug.Log(PlayerPrefs.GetFloat("vertical"));
        Debug.Log(PlayerPrefs.GetFloat("horizontal"));
        Debug.Log(PlayerPrefs.GetFloat("oclusion"));
        if (PlayerPrefs.GetFloat("oclusion")==1.0f)
        {
            Camera.main.AddComponent<AROcclusionManager>();
            Camera.main.GetComponent<AROcclusionManager>().requestedEnvironmentDepthMode = UnityEngine.XR.ARSubsystems.EnvironmentDepthMode.Best;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscenarioPulsado()
    {
        bCrear.SetActive(false);
        crearPulsado=true;
        this.GetComponent<PlaneCounterPablo>().InstantiateCubesOnDetectedPlanes((int)PlayerPrefs.GetFloat("vertical"),(int)PlayerPrefs.GetFloat("horizontal"));
        vertical.gameObject.SetActive(false);
        horizontal.gameObject.SetActive(false);
        titulo.text="Gemas encontradas=\n 0/"+(PlayerPrefs.GetFloat("vertical")+PlayerPrefs.GetFloat("horizontal")).ToString();
        this.GetComponent<CuentaAtras>().totalTime=(int)PlayerPrefs.GetFloat("tiempo");
        this.GetComponent<CuentaAtras>().iniciar = true;
        GameObject.Find("AudioSourceReloj").GetComponent<AudioSource>().Play();
    }

    public void SalirPulsado()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    public void planoDectado(int vert, int hori)
    {
        if(!crearPulsado)
        {        
            vertical.text = "VERTICALES: "+vert+"/"+PlayerPrefs.GetFloat("vertical");
            horizontal.text = "HORIZONTAL:"+hori+"/"+PlayerPrefs.GetFloat("horizontal");
            if (vert>=(int)PlayerPrefs.GetFloat("vertical") && hori>=(int)PlayerPrefs.GetFloat("horizontal"))
            {
                bCrear.SetActive(true);
            }
            else
            {
                bCrear.SetActive(false);
            }
        }
    }

    public void gemaEncontrada()
    {
        GameObject.Find("AudioSource").GetComponent<AudioSource>().Play();
        gemasEncontradas++;
        if (gemasEncontradas==((int)PlayerPrefs.GetFloat("vertical")+(int)PlayerPrefs.GetFloat("horizontal")))
        {
            GameObject.Find("AudioSourceReloj").GetComponent<AudioSource>().Stop();
            titulo.text="HAS GANADO. FIN";
            this.GetComponent<CuentaAtras>().iniciar = false;
            
        }
        else
        {
            titulo.text="Gemas encontradas=\n "+gemasEncontradas+"/"+(PlayerPrefs.GetFloat("vertical")+PlayerPrefs.GetFloat("horizontal")).ToString();
        }
    }

    
}
