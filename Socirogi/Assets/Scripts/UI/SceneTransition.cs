using System.Collections;
using UnityEngine;

namespace UI
{
    public abstract class SceneTransition : MonoBehaviour
    {

        // Transition that plays upon switching to another scene (fade in)
        public abstract IEnumerator animateTransitionIn();
        
        // finish the transition after the scene has been switched (fade out)
        public abstract IEnumerator animateTransitionOut();


    }
}
