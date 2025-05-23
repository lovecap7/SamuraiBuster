using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameObjectStore")]
public class GameObjectStore : ScriptableObject
{
    public List<GameObject> objects;
}
