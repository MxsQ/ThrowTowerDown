using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower
{
    [System.Serializable]
    public class TowerShaper
    {
        [Header("outlook")]
        /* deside base shape of the part of tower */
        public Shape shape = Shape.Circle;
        public int length = 20; //1.it's length of size when is square or triangle; 2. it's girth when is circle
        public int hight = 1; // number of layers
        public int increateLength;
        public float startRotateAngle = 0;
        public float increseAngle = 0;

        [Header("center position")]
        /* decide center point of per layer */
        public float centerZOffset = 0;
        public float centerIncreaseAngle = 0;

        public readonly float brickWidth = 1;
    }

    public class TowerLayer
    {
        public List<GameObject> bricks = new List<GameObject>();

        public void Add(GameObject brick)
        {
            bricks.Add(brick);
        }

        public void Remove(GameObject brick)
        {
            bricks.Remove(brick);
        }
    }
}




public enum Shape
{
    Circle,
    Square,
    Triangle,
}
