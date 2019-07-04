using asd;

using System;
using System.Linq;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    /// <summary>
    /// リザルト表示シーン
    /// </summary>
    public partial class ResultScene : Scene
    {
        protected override void OnRegistered()
        {
            Layer2D back = new Layer2D();
            Layer2D text = new Layer2D();
            Layer2D window = new Layer2D();

            // 背景を設定する
            back.AddPostEffect(
                new Background(
                    new Vector3DF(1.0f, 1.0f, 0.0f),
                    new Vector3DF(1.0f, 1.0f, 0.6f)
                )
            );

            // テキストの中心座標をオブジェクトの中心にする
            Vector2DF center = new Vector2DF(0.5f, 0.5f);

            // 「RESULT」
            AGText title = new AGText(72, 4, center);
            title.SetText("RESULT");
            title.Position = new Vector2DF(640, 70);
            text.AddObject(title);

            // ウィンドウを追加する
            for (int i = 1; i <= 4; ++i)
                if (Game.Player[i - 1] != null)
                    window.AddObject(new ResultWindow(i) { NoisyValue = 0.025f });

            // レイヤーを追加する
            AddLayer(back);
            AddLayer(text);
            AddLayer(window);
        }

        protected override void OnStartUpdating()
        {
            base.OnStartUpdating();

            // BGMの再生
            BGM_ID = Sound.Play(BGM_Result);
        }

        protected override void OnUpdated()
        {
            var players = Game.Player.Where(p => p != null);

            foreach (Player player in players)
            {
                Controller controller = (Controller)player.Controller;

                foreach (Button button in Enum.GetValues(typeof(Button)))
                    if (controller.GetPush(button))
                    {
                        Sound.Stop(BGM_ID);
                        Sound.Play(SE_Start);

                        Engine.ChangeSceneWithTransition(new SelectScene(), new TransitionFade(1, 1));
                    }
            }
        }
    }
}
