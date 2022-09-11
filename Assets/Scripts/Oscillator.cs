using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField]  float period = 2f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycles = Time.time / period; // Creciendo a medida que pasa el tiempo
        const float tau = Mathf.PI * 2; // Valor constante de 6.283
        float rawSineWave = Mathf.Sin(cycles * tau); // Variando entre -1 y 1

        movementFactor = (rawSineWave + 1f) / 2f; // Calculado para que vaya de 0 a 1
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
