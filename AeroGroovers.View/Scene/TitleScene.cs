using asd;

using System;

namespace AeroGroovers.View
{
    /// <summary>
    /// タイトルシーン
    /// </summary>
    public class TitleScene : Scene
    {
        /// <summary>
        /// 背景に使うレイヤー
        /// </summary>
        private Layer2D BackLayer = new Layer2D();

        /// <summary>
        /// テキストを表示するレイヤー
        /// </summary>
        private Layer2D TextLayer = new Layer2D();

        protected override void OnRegistered()
        {
            // 背景を設定する
            BackLayer.AddPostEffect(
                new Background(
                    new Vector3DF(0.0f, 0.4f, 1.0f),
                    new Vector3DF(0.4f, 0.7f, 1.0f)
                )
            );

            // テキストの中心座標をオブジェクトの中心にする
            Vector2DF center = new Vector2DF(0.5f, 0.5f);

            // 「Aero Groovers」
            AGText title = new AGText(120, 4, center);
            title.SetText("Aero Groovers");
            title.Position = new Vector2DF(640, 150);
            TextLayer.AddObject(title);

            // 「Press Any Button.」
            AGText announce = new AGText(72, 4, center);
            announce.SetText("Press Any Button.");
            announce.Position = new Vector2DF(640, 600);
            TextLayer.AddObject(announce);

            // レイヤーを追加する
            AddLayer(BackLayer);
            AddLayer(TextLayer);
        }

        protected override void OnStartUpdating()
        {
            base.OnStartUpdating();

            // BGMの再生
            BGM_ID = Sound.Play(Sound.BGM_Title);
        }

        protected override void OnUpdated()
        {
            // キーが押されたらシーンチェンジ
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                if (Controller.KeyPush(key)) { }
            // Engine.ChangeSceneWithTransition(new Entry(), new TransitionFade(1, 1));

            // ボタンが押されたらシーンチェンジ
            for (int i = 0; i < 16; ++i)
                if (Controller.IsPresent(i))
                {
                    Joystick joystick = Controller.GetJoystick(i);
                    for (int j = 0; j < joystick.ButtonsCount; ++j)
                        if (Controller.JoyPush(joystick, j)) { }
                    // Engine.ChangeSceneWithTransition(new Entry(), new TransitionFade(1, 1));
                }
        }

        protected override void OnTransitionBegin()
        {
            base.OnTransitionBegin();

            // SEの再生
            Sound.Play(Sound.SE_Start);
        }
    }
}
