
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/AnimationNames")]
public class AnimationNamesConfig : ScriptableObject
{
    [Tooltip("Lista de todos los nombres de animaciones disponibles")]
    public List<string> animationNames = new List<string>();
}