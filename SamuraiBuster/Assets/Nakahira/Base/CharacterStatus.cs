using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // Animationから関数として操作するためにプロパティにしています
    public int damage { get; set; }
    public int hitPoint { get; set; }
}
