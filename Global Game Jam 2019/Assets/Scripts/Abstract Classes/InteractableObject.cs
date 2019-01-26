using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AbstractClasses
{
    public abstract class InteractableObject : MonoBehaviour
    {
        /// <summary>
        /// Este método es el que se llama al interactuar con el objeto. Cada objeto lo implementará de una forma
        /// </summary>
        abstract public void Interact();
    }
}
