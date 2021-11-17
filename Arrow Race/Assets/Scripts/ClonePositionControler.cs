using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePositionControler : MonoBehaviour
{
    
            
string clonepath = "Player/ClonePlaces/";    
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
            
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find(clonepath+ "Place0").transform, false));
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find(clonepath+ "Place1").transform, false));
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find(clonepath+ "Place2").transform, false));
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find(clonepath+ "Place3").transform, false));
            cloneBehaviour.Add(new CloneBehaviour(GameObject.Find(clonepath+ "Place4").transform, false));
        }
        private void Update()
        {
            

        }

}
