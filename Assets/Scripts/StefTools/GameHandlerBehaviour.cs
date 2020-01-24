using UnityEngine;

namespace StefTools
{
    public class GameHandlerBehaviour : MonoBehaviour
    {
        public GameObject workerPrefab;
        // Start is called before the first frame update
        void Start()
        {
            TaskSystem taskSystem = new TaskSystem();
            var obj = Instantiate(workerPrefab);
            EntityTaskAIBehaviour workerTaskAI = obj.GetComponent<EntityTaskAIBehaviour>();
            WorkerBehaviour wb = obj.GetComponent<WorkerBehaviour>();
            
    //        workerTaskAI.Setup(wb);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
