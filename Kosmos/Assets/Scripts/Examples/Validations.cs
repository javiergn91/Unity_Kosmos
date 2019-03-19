using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Validations : MonoBehaviour
{
	void Start ()
    {
        Debug.Log("Rut 17742947-3 es: " + Kosmos.Validation.Validate.isChileanRut("17742947-3"));
        Debug.Log("Rut 17.742.947-3 es: " + Kosmos.Validation.Validate.isChileanRut("17.742.947-3"));
        Debug.Log("Rut 177429473 es: " + Kosmos.Validation.Validate.isChileanRut("177429473"));
        Debug.Log("Rut 17.748.947-3 es: " + Kosmos.Validation.Validate.isChileanRut("17.748.947-3"));
    }
}
