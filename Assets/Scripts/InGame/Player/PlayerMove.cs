using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

namespace ingame.player {
    public class PlayerMove : MonoBehaviour {

        private const float moveSpeed_ = 0.25f;
        private const float laneSize_ = 1.5f;
        private LanePos lane_;
        private Tween tween_;

        private float moveSpeedX_ = 0.5f;

        private void Awake() {
            lane_ = LanePos.Middle;
            tween_ = null;
        }

        private void Start() {

            StartCoroutine(PlayerMoveX());

            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.DownArrow))
                .Where(_ => lane_ != LanePos.Down)
                .Where(_ => tween_ == null)
                .Subscribe(_ => {
                    tween_ = transform.DOMoveY(-laneSize_, moveSpeed_).SetEase(Ease.Linear).SetRelative();
                    tween_.OnComplete(() => tween_ = null);
                    lane_ = (LanePos)((int)lane_ - 1);
                });

            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.UpArrow))
                .Where(_ => lane_ != LanePos.Up)
                .Where(_ => tween_ == null)
                .Subscribe(_ => {
                    tween_ = transform.DOMoveY(laneSize_, moveSpeed_).SetEase(Ease.Linear).SetRelative();
                    tween_.OnComplete(() => tween_ = null);
                    lane_ = (LanePos)((int)lane_ + 1);
                });
        }

        private IEnumerator PlayerMoveX() {
            while (true) {
                var move = transform.DOMoveX(1f, moveSpeedX_).SetEase(Ease.Linear).SetRelative();
                yield return move.WaitForCompletion();
            }
        }
    }
}