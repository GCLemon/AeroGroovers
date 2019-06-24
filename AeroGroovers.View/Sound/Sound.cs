using asd;

namespace AeroGroovers.View
{
    /// <summary>
    /// 音関係の操作を行う静的クラス
    /// </summary>
    public static class Sound
    {
        /// <summary>
        /// BGMに用いる音源を作成する
        /// </summary>
        public static SoundSource CreateBGM(string path) =>
            Engine.Sound.CreateSoundSource(path, false);

        /// <summary>
        /// ループするBGMを作成する
        /// </summary>
        public static SoundSource CreateLoopingBGM(string path, float start = -1, float end = -1)
        {
            // 音を作成する
            var sound = Engine.Sound.CreateSoundSource(path, false);

            // ループモードをオンにする
            sound.IsLoopingMode = true;

            // ループ開始・終了地点を設定
            if (start < 0) sound.LoopStartingPoint = 0;
            else sound.LoopStartingPoint = start;
            if (end < 0) sound.LoopEndPoint = sound.Length;
            else sound.LoopStartingPoint = end;

            // 作成した音源を返す
            return sound;
        }

        /// <summary>
        /// 効果音に用いる音源を作成する
        /// </summary>
        public static SoundSource CreateSE(string path) =>
            Engine.Sound.CreateSoundSource(path, true);


        /// <summary>
        /// 音源を再生する
        /// </summary>
        public static int  Play(SoundSource source) => Engine.Sound.Play(source);

        /// <summary>
        /// 音源を停止する
        /// </summary>
        public static void Stop(int id) => Engine.Sound.Stop(id);

        /// <summary>
        /// 音源を一時停止する
        /// </summary>
        public static void Pause(int id) => Engine.Sound.Pause(id);

        /// <summary>
        /// 音源を一時停止した位置から再生する
        /// </summary>
        public static void Resume(int id) => Engine.Sound.Resume(id);


        /// <summary>
        /// タイトルシーンで再生するBGM
        /// </summary>
        public static SoundSource BGM_Title  = CreateLoopingBGM("Resources/Sounds/BGM_Title.ogg", 18.223f);

        /// <summary>
        /// コントローラー登録シーンで再生するBGM
        /// </summary>
        public static SoundSource BGM_Entry  = CreateLoopingBGM("Resources/Sounds/BGM_Entry.ogg");

        /// <summary>
        /// ゲーム設定シーンで再生するBGM
        /// </summary>
        public static SoundSource BGM_Option = CreateLoopingBGM("Resources/Sounds/BGM_Option.ogg");

        /// <summary>
        /// リザルトシーンで再生するBGM
        /// </summary>
        public static SoundSource BGM_Result = CreateLoopingBGM("Resources/Sounds/BGM_Result.ogg");


        /// <summary>
        /// ゲーム開始時に再生する効果音
        /// </summary>
        public static SoundSource SE_Start       = CreateSE("Resources/Sounds/SE_Start.ogg");

        /// <summary>
        /// 前のシーンに戻るときに再生する効果音
        /// </summary>
        public static SoundSource SE_Cancel      = CreateSE("Resources/Sounds/SE_Cancel.ogg");

        /// <summary>
        /// ウィンドウを開くときに再生する効果音
        /// </summary>
        public static SoundSource SE_Open        = CreateSE("Resources/Sounds/SE_Open.ogg");

        /// <summary>
        /// ウィンドウを閉じるときに再生する効果音
        /// </summary>
        public static SoundSource SE_Close       = CreateSE("Resources/Sounds/SE_Close.ogg");

        /// <summary>
        /// 決定したときに再生する効果音
        /// </summary>
        public static SoundSource SE_Decision    = CreateSE("Resources/Sounds/SE_Decision.ogg");

        /// <summary>
        /// 選択項目を移動するときに再生する効果音
        /// </summary>
        public static SoundSource SE_Select      = CreateSE("Resources/Sounds/SE_Select.ogg");

        /// <summary>
        /// プレイ開始時に再生する効果音
        /// </summary>
        public static SoundSource SE_BattleStart = CreateSE("Resources/Sounds/SE_BattleStart.ogg");

        /// <summary>
        /// ノーツが反応したときに再生する効果音
        /// </summary>
        public static SoundSource SE_Hit         = CreateSE("Resources/Sounds/SE_Hit.ogg");
    }
}
