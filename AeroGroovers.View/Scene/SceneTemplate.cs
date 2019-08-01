using asd;
using AeroGroovers.Model;

namespace AeroGroovers.View
{
    /// <summary>
    /// このゲームで使用するシーンクラスの親クラス
    /// </summary>
    public abstract class Scene : asd.Scene
    {
        /// <summary>
        /// 鳴らされているBGMのID(Sound.Play()の戻り値を格納する)
        /// </summary>
        public static int BGM_ID;

        /// <summary>
        /// ゲーム全体の情報を持つオブジェクト
        /// </summary>
        public static Game Game { get; protected set; } = new Game();

        /// <summary>
        /// タイトルシーンで再生するBGM
        /// </summary>
        public SoundSource BGM_Title;

        /// <summary>
        /// コントローラー登録シーンで再生するBGM
        /// </summary>
        public SoundSource BGM_Entry;

        /// <summary>
        /// ゲーム設定シーンで再生するBGM
        /// </summary>
        public SoundSource BGM_Option;

        /// <summary>
        /// リザルトシーンで再生するBGM
        /// </summary>
        public SoundSource BGM_Result;


        /// <summary>
        /// ゲーム開始時に再生する効果音
        /// </summary>
        public SoundSource SE_Start;

        /// <summary>
        /// 前のシーンに戻るときに再生する効果音
        /// </summary>
        public SoundSource SE_Cancel;

        /// <summary>
        /// ウィンドウを開くときに再生する効果音
        /// </summary>
        public SoundSource SE_Open;

        /// <summary>
        /// ウィンドウを閉じるときに再生する効果音
        /// </summary>
        public SoundSource SE_Close;

        /// <summary>
        /// 決定したときに再生する効果音
        /// </summary>
        public SoundSource SE_Decision;

        /// <summary>
        /// 選択項目を移動するときに再生する効果音
        /// </summary>
        public SoundSource SE_Select;

        /// <summary>
        /// プレイ開始時に再生する効果音
        /// </summary>
        public SoundSource SE_BattleStart;

        /// <summary>
        /// ノーツが反応したときに再生する効果音
        /// </summary>
        public SoundSource SE_Hit;

        public Scene()
        {
            // BGMの設定
            BGM_Title = Sound.CreateBGM("Sounds/BGM_Title.ogg");
            BGM_Title.IsLoopingMode = true;
            BGM_Title.LoopStartingPoint = 18.223f;
            BGM_Title.LoopEndPoint = BGM_Title.Length;

            BGM_Entry = Sound.CreateBGM("Sounds/BGM_Entry.ogg");
            BGM_Entry.IsLoopingMode = true;
            BGM_Entry.LoopStartingPoint = 0;
            BGM_Entry.LoopEndPoint = BGM_Title.Length;

            BGM_Option = Sound.CreateBGM("Sounds/BGM_Option.ogg");
            BGM_Option.IsLoopingMode = true;
            BGM_Option.LoopStartingPoint = 0;
            BGM_Option.LoopEndPoint = BGM_Title.Length;

            BGM_Result = Sound.CreateBGM("Sounds/BGM_Result.ogg");
            BGM_Result.IsLoopingMode = true;
            BGM_Result.LoopStartingPoint = 0;
            BGM_Result.LoopEndPoint = BGM_Title.Length;

            // 効果音の設定
            SE_Start = Sound.CreateSE("Sounds/SE_Start.ogg");
            SE_Cancel = Sound.CreateSE("Sounds/SE_Cancel.ogg");
            SE_Open = Sound.CreateSE("Sounds/SE_Open.ogg");
            SE_Close = Sound.CreateSE("Sounds/SE_Close.ogg");
            SE_Decision = Sound.CreateSE("Sounds/SE_Decision.ogg");
            SE_Select = Sound.CreateSE("Sounds/SE_Select.ogg");
            SE_BattleStart = Sound.CreateSE("Sounds/SE_BattleStart.ogg");
            SE_Hit = Sound.CreateSE("Sounds/SE_Hit.ogg");
        }

        protected override void OnStartUpdating()
        {
            // 入力を受け付ける
            Controller.AcceptInput = true;
        }

        protected override void OnTransitionBegin()
        {
            // 入力を受け付けない
            Controller.AcceptInput = false;

            // BGMの再生を止める
            Sound.Stop(BGM_ID);
        }
    }
}
