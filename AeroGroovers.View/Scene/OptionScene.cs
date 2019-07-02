using asd;

using System.Collections.Generic;
using System.Linq;

namespace AeroGroovers.View
{
    /// <summary>
    /// プレイ設定シーン
    /// </summary>
    public partial class OptionScene : Scene
    {
        /// <summary>
        /// 追加したウィンドウオブジェクト
        /// </summary>
        List<OptionWidnow> Windows = new List<OptionWidnow>();

        /// <summary>
        /// シーンが変わっているか
        /// </summary>
        bool SceneChanging;

        protected override void OnRegistered()
        {
            Layer2D back = new Layer2D();
            Layer2D text = new Layer2D();
            Layer2D window = new Layer2D();

            // 背景の設定
            back.AddPostEffect(
                new Background(
                    new Vector3DF(0.4f, 1.0f, 0.0f),
                    new Vector3DF(0.7f, 1.0f, 0.4f)
                )
            );

            // テキストオブジェクトの生成・追加
            AGText title = new AGText(72, 4, new Vector2DF(0.5f, 0.5f));
            title.Position = new Vector2DF(640, 70);
            title.SetText("OPTION");

            text.AddObject(title);

            // ウィンドウの追加
            for (int i = 0; i < 4; ++i)
                if(Game.Player[i] != null)
                {
                    var w = new OptionWidnow(i + 1);
                    w.NoisyValue = 0.025f;
                    window.AddObject(w);
                    Windows.Add(w);
                }

            AddLayer(back);
            AddLayer(text);
            AddLayer(window);
        }

        protected override void OnStartUpdating()
        {
            base.OnStartUpdating();

            // BGMの再生
            BGM_ID = Sound.Play(BGM_Option);
        }

        protected override void OnUpdated()
        {
            // 設定が全て終わっているならば,ゲームシーンに移行する
            if (Windows.All(w => w.IsFinished) && !SceneChanging)
            {
                Sound.Stop(BGM_ID);
                Sound.Play(SE_BattleStart);
                Engine.ChangeSceneWithTransition(new GameScene(), new TransitionFade(1.276f, 2.996f));
                SceneChanging = true;
            }
        }

        protected override void OnTransitionBegin()
        {
            base.OnTransitionBegin();

            // シーンが変わっていることを明示する
            SceneChanging = true;
        }
    }
}
