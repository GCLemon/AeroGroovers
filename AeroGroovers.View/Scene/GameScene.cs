using asd;

using System.Linq;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    /// <summary>
    /// 実際にプレイを行うシーン
    /// </summary>
    public partial class GameScene : Scene
    {
        /// <summary>
        /// ゲームの状態
        /// </summary>
        private enum GameState
        {
            /// <summary> 初期化している </summary>
            Initialize,

            /// <summary> 準備している </summary>
            Ready,

            /// <summary> プレイ中 </summary>
            Play,

            /// <summary> ポーズ中 </summary>
            Pause,

            /// <summary> プレイ終了 </summary>
            End
        }

        /// <summary>
        /// BGMを鳴らしたか
        /// </summary>
        private bool MusicStarted;

        // 音がなっているか
        bool SoundPlaying
        {
            get
            {
                // ノーツのタイマーが音源の長さを超えた時,false
                float length = BGM.Length * 1000 + 3000;
                return Note.Time <= length;
            }
        }

        /// <summary>
        /// ゲームの現在の状態
        /// </summary>
        private GameState CurrentState;

        /// <summary>
        /// エフェクトを描画するレイヤー
        /// </summary>
        public Layer2D EffectLayer { get; private set; } = new Layer2D();

        /// <summary>
        /// ポーズ画面用のレイヤー
        /// </summary>
        public Layer2D PauseLayer = new Layer2D
        {
            DrawingPriority = 10,
            IsDrawn = false,
            IsUpdated = false
        };

        /// <summary>
        /// ゲームシーンで流すBGM
        /// </summary>
        SoundSource BGM;

        /// <summary>
        /// ReadyGoエフェクト
        /// </summary>
        ReadyGoEffect ReadyGo = new ReadyGoEffect();

        public GameScene()
        {
            Layer2D back = new Layer2D();

            back.AddPostEffect(
                new Background(
                    new Vector3DF(1.0f, 1.0f, 0.0f),
                    new Vector3DF(1.0f, 1.0f, 0.6f)
                )
            );

            AddLayer(back);

            Skill.Game = Game;

            for (int i = 0; i < 4; ++i)
            {
                if (Game.Player[i] != null)
                {
                    // プレイヤー情報を初期化
                    var d = Game.Player[i].Difficulty;
                    var c = Game.Score.Notes[d].Count;
                    Game.Player[i].Initialize(c);

                    AddLayer(new PlayLayer(i));
                    AddLayer(new UILayer(i));
                }
            }

            // あらかじめポーズのためにレイヤーを用意
            PauseLayer.AddObject(new PauseWindow { Resume = Resume });

            PauseLayer.AddObject(new GeometryObject2D
            {
                Shape = new RectangleShape { DrawingArea = new RectF(0, 0, 1280, 800) },
                Color = new Color(0, 0, 0, 127)
            });

            EffectLayer.DrawingPriority = 9;
            AddLayer(EffectLayer);
            AddLayer(PauseLayer);

            // ゲームを初期化
            Reset();
        }

        protected override void OnUpdated()
        {
            switch (CurrentState)
            {
                case GameState.Ready:

                    // エフェクトが再生され終わったらゲームを始める
                    if (!ReadyGo.IsAlive)
                    {
                        StartGame();
                        CurrentState = GameState.Play;
                    }
                    break;

                case GameState.Play:
                    // ノートタイマーの値が0以上の時
                    // 音が鳴らされていなければ
                    // 音を鳴らす
                    if (Note.Time >= 0 && !MusicStarted) StartMusic();

                    // ゲーム中にセレクトが押された時
                    // ポーズする
                    foreach (Player p in Game.Player.Where(p => p != null))
                    {
                        var c = (Controller)p.Controller;
                        if (c.GetPush(Button.Select)) Pause();
                    }

                    // 音が再生されなくなったら
                    // ゲームを終了する
                    if (!SoundPlaying)
                    {
                        TerminateGame();
                         CurrentState = GameState.End;
                    }
                    break;
            }
        }

        protected override void OnStartUpdating() { }

        protected override void OnTransitionFinished()
        {
            CurrentState = GameState.Ready;

            // ReadyGoエフェクトの追加
            EffectLayer.AddObject(ReadyGo);
        }

        /// <summary>
        /// ゲームをリセットする
        /// </summary>
        void Reset()
        {
            // ノーツのタイマーを初期化
            Note.Stopwatch.Stop();
            Note.Stopwatch.Reset();
        }

        /// <summary>
        /// ゲームを開始する
        /// </summary>
        void StartGame()
        {
            // コントローラーからの入力を受け付ける
            Controller.AcceptInput = true;

            // 音を準備する
            string path = Game.Score.SoundPath;
            BGM = Sound.CreateBGM(path);
            BGM.IsLoopingMode = false;

            // ノーツタイマーを動かす
            Note.Stopwatch.Start();
        }

        /// <summary>
        /// 音を鳴らす
        /// </summary>
        void StartMusic()
        {
            // BGMのスタート
            BGM_ID = Sound.Play(BGM);

            MusicStarted = true;
        }

        /// <summary>
        /// ゲームを終える
        /// </summary>
        void TerminateGame()
        {
            Engine.ChangeSceneWithTransition(new ResultScene(), new TransitionFade(1, 1));
            

            // 音の再生を止める
            Sound.Stop(BGM_ID);
        }

        /// <summary>
        /// ポーズ
        /// </summary>
        void Pause()
        {
            // 音の再生を止める
            Sound.Pause(BGM_ID);

            // ポーズレイヤーの更新をし,それ以外は更新を止める
            foreach (Layer2D l in Layers) l.IsUpdated = false;
            PauseLayer.IsDrawn = true;
            PauseLayer.IsUpdated = true;

            // タイマーを止める
            Note.Stopwatch.Stop();

            // ポーズ状態にする
            CurrentState = GameState.Pause;
        }

        /// <summary>
        /// ポーズ解除
        /// </summary>
        void Resume()
        {
            // 音を再生する
            Sound.Resume(BGM_ID);

            // ポーズレイヤーの更新を止め,それ以外は再開する
            foreach (Layer2D l in Layers) l.IsUpdated = true;
            PauseLayer.IsDrawn = false;
            PauseLayer.IsUpdated = false;

            // ノーツタイマーを動かす
            Note.Stopwatch.Start();

            // ポーズ状態を解除する
            CurrentState = GameState.Play;
        }
    }
}
