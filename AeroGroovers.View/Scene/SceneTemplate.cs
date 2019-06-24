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
        protected static int BGM_ID;

        /// <summary>
        /// ゲーム全体の情報を持つオブジェクト
        /// </summary>
        protected static Game Game = new Game();

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
