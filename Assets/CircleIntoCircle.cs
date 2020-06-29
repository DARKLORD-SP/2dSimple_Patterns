using UnityEngine;
using UnityEditor;

namespace Trignometry.Circle
{

    /// <summary>
    /// CircleIntoCircle Description
    /// </summary>
    public class CircleIntoCircle : MonoBehaviour
    {
        [Range(1.0f, 20.0f)]
        public float radius = 10;
        [Range(2, 100)]
        public int numberOfCirclesToPlot = 10;
        [Range(1, 10)]
        public int iteration = 2;


        public void OnDrawGizmos()
        {
            //Drawing Main Cirlce
            Vector3 mainCirclePos = Vector3.zero;
            Handles.color = Color.red;
            Handles.DrawWireDisc(mainCirclePos, -Vector3.forward, radius);

            float theta = 360 / numberOfCirclesToPlot;

            int currentIteration = 1;
            while (currentIteration < iteration)
            {
                Vector3[] allCircles = new Vector3[numberOfCirclesToPlot];

                for (int i = 1; i <= numberOfCirclesToPlot; i++)
                {
                    float nextTheta = ((i * theta) * Mathf.PI) / 180.0f;

                    Vector3 tempCircle = CreateCirlce(mainCirclePos, radius, nextTheta);

                    allCircles[i - 1] = tempCircle;

                    Handles.color = Color.blue;
                    Handles.DrawWireDisc(tempCircle, Vector3.forward, radius * (iteration * 0.1f));
                }

                for (int i = 0; i < allCircles.Length; i++) 
                {
                    for(int j = 1; j <= numberOfCirclesToPlot; j++)
                    {
                        float nextTheta = ((j * theta) * Mathf.PI) / 180.0f;

                        Vector3 tempCircle = CreateCirlce(allCircles[i], radius * (iteration * 0.1f), nextTheta);

                        Handles.color = Color.green;
                        Handles.DrawWireDisc(tempCircle, -Vector3.forward, radius * (iteration * 0.01f));
                    }
                }

                currentIteration += 1;
            }
        }

        Vector3 CreateCirlce(Vector3 referencePos, float _radius, float angleInRadians)
        {
            return new Vector3(referencePos.x + (_radius * Mathf.Cos(angleInRadians)),
                    referencePos.y + (_radius * Mathf.Sin(angleInRadians)), 0);
        }
    }

}