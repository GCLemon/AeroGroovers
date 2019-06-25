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
    }
}
