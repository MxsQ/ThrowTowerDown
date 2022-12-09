using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    [SerializeField] public BrickColor color;

    public TowerLayer Host { get; set; }

    public void Eliminate()
    {
        Host.Remove(gameObject);
        Destroy(gameObject);
    }
}

public enum BrickColor
{
    Defaualt,
    Red,
    Blue,
    Yellow,
}
