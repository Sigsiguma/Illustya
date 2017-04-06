using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ingame.camera {
    public class CameraController : MonoBehaviour {

        [SerializeField]
        private Transform target_;

        private Vector3 startDistance_;

        private void Start() {
            startDistance_ = transform.position - target_.position;

            this.LateUpdateAsObservable()
                .Where(_ => target_ != null)
                .Subscribe(_ => {
                    var pos = transform.position;
                    pos.x = target_.position.x + startDistance_.x;
                    transform.position = pos;
                });
        }
    }
}
