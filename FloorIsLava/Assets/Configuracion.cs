using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuracion : MonoBehaviour
{

    public GameObject CanvasPrincipal;
    public GameObject CanvasConfiguracion;
    public GameObject CanvasOperaciones;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Funci�n para cambiar el tama�o de la ventana
    public void CambiarTama�oVentana()
    {
        // Cambiar la resoluci�n a 800x600 en modo ventana

        Screen.fullScreen = false;
        Screen.SetResolution(1100, 850, false);
    }

    public void IrAConfiguracion()
    {
        CanvasPrincipal.SetActive(false);
        CanvasConfiguracion.SetActive(true);
    }

    public void IrAOpciones()
    {
        CanvasPrincipal.SetActive(false);
        CanvasOperaciones.SetActive(true);
    }

    public void IrAInicio()
    {
        CanvasPrincipal.SetActive(true);
        CanvasConfiguracion.SetActive(false);
        CanvasOperaciones.SetActive(false);

    }
    public void PantallaCompleta()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true);

    }
}