using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePositionControler : MonoBehaviour
{
            
    string clonepath = "Player/ClonePlaces/";    
    public List <CloneBehaviour> cloneBehaviour = new List<CloneBehaviour>();
    private int calc = 0;


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
        }
        private void Update()
        {
            ElementAdd();
        }


        void ElementAdd(){
            int tempno = 0;

        for(int i = 0; i < cloneBehaviour.Count ; i++ )
            {
                if (cloneBehaviour[i].IsFull == true)
                {
                    tempno++;
                }
            }
            if (tempno == (cloneBehaviour.Count)){
                Vector3 newpos = cloneBehaviour[cloneBehaviour.Count - 3].Cube.transform.position - new Vector3(0, 0, 1f);
                Transform newplace = Instantiate(GameObject.Find(clonepath+ "Place0").transform, newpos, Quaternion.identity);
                newplace.parent = transform;
                cloneBehaviour.Add(new CloneBehaviour(newplace, false));
            }
        }
}
