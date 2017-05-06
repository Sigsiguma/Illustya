using UnityEngine;
using UniRx;
using utility.text;
using utility.stopwatch;

public class GameMananager : SingletonMonoBehaviour<GameMananager> {

    [SerializeField]
    private SpriteDigitNumber timeSprite_;

    [SerializeField]
    private SpriteDigitNumber scoreSprite_;

    public GameState state_ { get; private set; }

    public StopWatchRx timer_;

    public ReactiveProperty<int> score_;

    private new void Awake() {
        base.Awake();
        score_ = new ReactiveProperty<int>();
    }

    private void Start() {
        timer_ = new utility.stopwatch.StopWatchRx();

        timer_.CountDownStart(60, () => state_ = GameState.Result);
        state_ = GameState.Game;

        timer_.CurrentTime_.Subscribe(num => timeSprite_.inputDigit_.Value = num.ToString())
                           .AddTo(this);

        score_.Subscribe(num => scoreSprite_.inputDigit_.Value = num.ToString())
              .AddTo(this);

    }

    public enum GameState {
        Game,
        Result
    };
}
