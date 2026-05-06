using UnityEngine;
using SubmarineJourney.Core.DI;

namespace SubmarineJourney.Core {
    public abstract class BaseMonoBehaviour : MonoBehaviour {
        protected virtual void Awake() {
            DependencyInjector.Inject(this);
        }
    }
}
