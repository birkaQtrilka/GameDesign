using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Direction")]
public class Direction : ScriptableObject
{
    [SerializeField] Vector2 _direction;
    public Vector2 Value => _direction;
}
