using System;
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
        public List<Brick> bricks = new List<Brick>();
        public bool frezz = false;

        public void Add(GameObject brick)
        {
            bricks.Add(brick.GetComponent<Brick>());
        }

        public void Remove(GameObject brick)
        {
            bricks.Remove(brick.GetComponent<Brick>());
        }

        public void Pin()
        {
            foreach (Brick b in bricks)
            {
                b.Pin();
            }
            frezz = true;
        }

        public void Loosen()
        {
            //  Debug.Log("lose");
            foreach (Brick b in bricks)
            {
                b.Loosen();
            }
            frezz = false;
        }

        public bool isCollapse()
        {
            return bricks.Count == 0;
        }

        public int UpdateBrickState()
        {
            List<Brick> tmpBricks = new List<Brick>();
            foreach (Brick b in bricks)
            {
                if (b.AwayFrom())
                {
                    tmpBricks.Add(b);
                }
            }


            //  bricks.RemoveAll((brick) => { return brick.AwayFrom(); });

            foreach (Brick b in tmpBricks)
            {
                bricks.Remove(b);
            }

            return tmpBricks.Count;
        }
    }
}




public enum Shape
{
    Circle,
    Square,
    Triangle,
}
