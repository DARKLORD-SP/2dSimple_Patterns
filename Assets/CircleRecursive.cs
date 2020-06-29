using UnityEngine;
using UnityEditor;
using UnityEngine.XR;

namespace Trignometry.Circle
{

    /// <summary>
    /// CircleRecursive Description
    /// </summary>
    public class CircleRecursive : MonoBehaviour    
    {

        [Range(1f, 20f)]
        public float radius = 10f;

        [Range(2, 20)]
        public int numberOfCircles = 5;

        [Range(2, 100)]
        public int iteration = 2;

        private int currentIteration = 1;

        public void OnDrawGizmos()
        {

            Vector3 mainCircle = Vector3.zero;
            Handles.color = Color.red;
            Handles.DrawWireDisc(mainCircle, -Vector3.forward, radius);

            for (int i = 1; i <= numberOfCircles; i++) 
            {
                Vector3 iteratedCircle = mainCircle;
                Vector3 previouslyIteratedCircle = iteratedCircle;
                float theta = (i * (360 / numberOfCircles) * Mathf.PI) / 180.0f;
                float _radius = 0;
                do
                {
                    _radius = (iteratedCircle != mainCircle) ? _radius * ((currentIteration) * 0.1f) : radius;
                    
                    previouslyIteratedCircle = iteratedCircle;
                    iteratedCircle = CreateCircle(iteratedCircle, theta, _radius);

                    Handles.color = Color.Lerp(Color.blue, Color.red, Time.deltaTime / 0.2f);
                    Handles.DrawWireDisc(iteratedCircle, -Vector3.forward, _radius );

                    currentIteration++;
                }
                while (currentIteration != iteration);

                for (int j = 1; j < numberOfCircles ; j++) {
                    iteratedCircle = CreateCircle(previouslyIteratedCircle, theta, _radius);

                    Handles.color = Color.Lerp(Color.blue, Color.yellow, Time.deltaTime / 0.2f);
                    Handles.DrawWireDisc(iteratedCircle, -Vector3.forward, _radius * (( currentIteration) * 0.1f));
                }


                currentIteration = 1;
            }

        }

        Vector3 CreateCircle(Vector3 referenceCircle, float angleInRaidians, float radius) 
        {
            return new Vector3(referenceCircle.x + (radius * Mathf.Cos(angleInRaidians)), referenceCircle.y + (radius * Mathf.Sin(angleInRaidians)), 0);
        }

    }

}