using asd;

namespace AeroGroovers.View
{
    /// <summary>
    /// 曲選択シーン
    /// </summary>
    public partial class SelectScene : Scene
    {
        protected override void OnRegistered()
        {
            // 譜面を読み込む
            Game.LoadScore();

            Layer2D back = new Layer2D();

            // 背景の設定
            back.AddPostEffect(
                new Background(
                    new Vector3DF(0.4f, 1.0f, 0.0f),
                    new Vector3DF(0.7f, 1.0f, 0.4f)
                )
            );

            AddLayer(back);
            AddLayer(new ImageLayer());
            AddLayer(new TextLayer());
        }

        protected override void OnStartUpdating()
        {
            base.OnStartUpdating();

            // 音を再生
            SoundSource sound = Sound.CreateBGM(Game.Score.SoundPath);
            sound.IsLoopingMode = true;
            BGM_ID = Sound.Play(sound);
        }
    }
}
