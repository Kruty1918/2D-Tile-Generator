using Mediators;
using UnityEngine;
using Utilities;

namespace EasyTransition
{

    public class DemoLoadScene : MonoBehaviour
    {
        public TransitionSettings transition;
        public float startDelay;

        public void LoadScene(string _sceneName)
        {
            Debug.Log(_sceneName);
            SM.Instance<TransitionManager>().Transition(_sceneName, transition, startDelay);
        }
    }
}