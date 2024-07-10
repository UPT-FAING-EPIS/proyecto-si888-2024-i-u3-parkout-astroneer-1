using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Pregunta
{
    public string textoPregunta;
    public List<string> opciones;
    public int indiceRespuestaCorrecta;
}

public class LavaTrigger : MonoBehaviour
{
    [Tooltip("The point that the player will teleport back to when they touch the trigger.")]
    [SerializeField] private Transform checkpoint;

    [SerializeField] private GameObject canvasEncuesta; 
    [SerializeField] private TextMeshProUGUI textoPregunta; 

    [SerializeField] private Button[] botonesAlternativas; 
    [SerializeField] private TextMeshProUGUI textoVidas; 
    [SerializeField] private MonoBehaviour playerMovementScript; 

    private int vidas = 3;
    private List<Pregunta> preguntas;
    private Pregunta preguntaActual;

    private void Start()
    {
        canvasEncuesta.SetActive(false);
        textoVidas.text = "Vidas: " + vidas;

        preguntas = new List<Pregunta>
        {
             new Pregunta
        {
            textoPregunta = "¿Cuál es el planeta más cercano al sol?",
            opciones = new List<string> { "Venus", "Marte", "Mercurio", "Júpiter" },
            indiceRespuestaCorrecta = 2
        },
        new Pregunta
        {
            textoPregunta = "¿En qué año llegó el hombre a la luna?",
            opciones = new List<string> { "1965", "1969", "1972", "1975" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Qué significa NASA?",
            opciones = new List<string> { "National Aeronautics and Space Administration", "National Air and Space Agency", "National Aeronautics and Space Agency", "National Air and Space Administration" },
            indiceRespuestaCorrecta = 0
        },
        new Pregunta
        {
            textoPregunta = "¿Cuál fue la primera nave espacial en llevar astronautas a la luna?",
            opciones = new List<string> { "Apollo 10", "Apollo 11", "Apollo 12", "Apollo 13" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Cuántos planetas se pueden ver desde la Tierra sin un telescopio?",
            opciones = new List<string> { "Cinco", "Seis", "Siete", "Ocho" },
            indiceRespuestaCorrecta = 0
        },
        new Pregunta
        {
            textoPregunta = "¿En qué año fue la primera mujer al espacio?",
            opciones = new List<string> { "1961", "1963", "1965", "1967" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Qué planeta ha recibido más misiones?",
            opciones = new List<string> { "Venus", "Marte", "Júpiter", "Saturno" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Cuál fue la primera sonda espacial en visitar Urano?",
            opciones = new List<string> { "Voyager 1", "Voyager 2", "Pioneer 10", "Pioneer 11" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Cuál fue el primer animal en órbita?",
            opciones = new List<string> { "Mono", "Perro", "Gato", "Ratón" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Cuántas constelaciones hay?",
            opciones = new List<string> { "78", "80", "88", "90" },
            indiceRespuestaCorrecta = 2
        },
        new Pregunta
        {
            textoPregunta = "¿Cuál es el nombre del primer comandante femenina de la Estación Espacial Internacional?",
            opciones = new List<string> { "Sally Ride", "Peggy Whitson", "Valentina Tereshkova", "Mae Jemison" },
            indiceRespuestaCorrecta = 1
        },
        new Pregunta
        {
            textoPregunta = "¿Cuál fue el primer planeta descubierto usando un telescopio?",
            opciones = new List<string> { "Neptuno", "Urano", "Plutón", "Saturno" },
            indiceRespuestaCorrecta = 1
        }

        };

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent(out PlayerController controller);
            controller.TeleportToPosition(checkpoint.position);

            if (vidas < 1)
            {
                SceneManager.LoadScene("Died");
            }

            playerMovementScript.enabled = false;

            preguntaActual = preguntas[Random.Range(0, preguntas.Count)];

            canvasEncuesta.SetActive(true);
            textoPregunta.text = preguntaActual.textoPregunta;

            for (int i = 0; i < botonesAlternativas.Length; i++)
            {
                int indice = i;
                botonesAlternativas[i].GetComponentInChildren<TextMeshProUGUI>().text = preguntaActual.opciones[i];
                botonesAlternativas[i].onClick.RemoveAllListeners();
                botonesAlternativas[i].onClick.AddListener(() => VerificarRespuesta(indice));
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.TryGetComponent(out PlayerController controller);
            controller.TeleportToPosition(checkpoint.position);

            if (vidas < 1)
            {
                SceneManager.LoadScene("Died");
            }

            playerMovementScript.enabled = false;

            preguntaActual = preguntas[Random.Range(0, preguntas.Count)];

            canvasEncuesta.SetActive(true);
            textoPregunta.text = preguntaActual.textoPregunta;

            for (int i = 0; i < botonesAlternativas.Length; i++)
            {
                int indice = i;
                botonesAlternativas[i].GetComponentInChildren<TextMeshProUGUI>().text = preguntaActual.opciones[i];
                botonesAlternativas[i].onClick.RemoveAllListeners();
                botonesAlternativas[i].onClick.AddListener(() => VerificarRespuesta(indice));
            }
        }
    }



    private void VerificarRespuesta(int indiceSeleccionado)
    {
        if (indiceSeleccionado != preguntaActual.indiceRespuestaCorrecta)
        {
            vidas--;
            textoVidas.text = "Vidas: " + vidas;
        }
        playerMovementScript.enabled = true;
        canvasEncuesta.SetActive(false);
    }
}