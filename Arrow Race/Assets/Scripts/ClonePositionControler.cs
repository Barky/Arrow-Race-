using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePositionControler : MonoBehaviour
{
        
    public List <CloneBehaviour> cloneBehaviour = new List<CloneBehaviour>();
 


     public class CloneBehaviour
        {
            public Transform Cube;
            public bool IsFull;

            public CloneBehaviour (Transform cube, bool isFull) //cube transform olacak
            {
                Cube = cube;
                IsFull = isFull;
            }
        }

        private void Start()
        {
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find("Player/ClonePlaces/Cube").transform, false));
        }
        private void Update()
        {
            
            Debug.Log(cloneBehaviour[0].Cube.transform.position);
        }
}
