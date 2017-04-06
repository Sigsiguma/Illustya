using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.stage {
    public class BackGroundRepeat : MonoBehaviour {

        private Transform mainCamera_;

        private const float spriteHalfSize_ = 17.86f;

        private void Awake() {
            mainCamera_ = GameObject.FindWithTag("MainCamera").transform;
        }

        private void Start() {
            this.OnBecameInvisibleAsObservable()
                .Where(_ => mainCamera_ != null)
                .Subscribe(_ => {
                    var pos = transform.position;
                    pos.x = mainCamera_.position.x + spriteHalfSize_;
                    transform.position = pos;
                });
        }

    }
}
