using asd;

using System.Linq;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    /// <summary>
    /// コントローラー登録シーン
    /// </summary>
    public partial class EntryScene : Scene
    {
        /// <summary>
        /// スタートボタンを押したフレーム数
        /// </summary>
        private int StartHold;

        protected override void OnRegistered()
        {
            Layer2D back = new Layer2D();
            Layer2D text = new Layer2D();
            Layer2D window = new Layer2D();

            // 背景を設定する
            back.AddPostEffect(
                new Background(
                    new Vector3DF(0.0f, 0.4f, 1.0f),
                    new Vector3DF(0.4f, 0.7f, 1.0f)
                )
            );

            // テキストの中心座標をオブジェクトの中心にする
            Vector2DF center = new Vector2DF(0.5f, 0.5f);

            // 「ENTRY」
            AGText title = new AGText(72, 4, center);
            title.SetText("ENTRY");
            title.Position = new Vector2DF(640, 70);
            text.AddObject(title);

            // 「コントローラーを登録します。準備ができたらスタートボタンを長押ししてください。」
            AGText announce = new AGText(36, 4, center);
            announce.SetText("コントローラーを登録します。\n準備ができたらスタートボタンを長押ししてください。");
            announce.Position = new Vector2DF(640, 720);
            text.AddObject(announce);

            // ウィンドウを追加する
            for (int i = 1; i <= 4; ++i)
                window.AddObject(new EntryWindow(i) { NoisyValue = 0.025f });

            // レイヤーを追加する
            AddLayer(back);
            AddLayer(text);
            AddLayer(window);
        }

        protected override void OnStartUpdating()
        {
            base.OnStartUpdating();

            // BGMの再生
            BGM_ID = Sound.Play(BGM_Entry);
        }

        protected override void OnUpdated()
        {
            // プレイヤーを追加する
            void AddPlayer(int index)
            {
                Controller controller =
                    (index == -1) ?
                    new Controller() :
                    new Controller(index);

                Game.AddPlayer(controller);
            }

            // 前のシーンに戻る
            void GotoPrevScene(int index)
            {
                if (!Game.Player
                    .Where(p => p != null)
                    .Any(p => p.Controller.ControllerID == index))
                {
                    Sound.Play(SE_Cancel);
                    Engine.ChangeSceneWithTransition(new TitleScene(), new TransitionFade(1, 1));
                }
            }

            // キーボード操作
            if (Controller.KeyPush(Keys.Z)) AddPlayer(-1);
            if (Controller.KeyPush(Keys.X)) GotoPrevScene(-1);

            // ジョイスティック操作
            for (int i = 0; i < 16; ++i)
            {
                Joystick joystick = Engine.JoystickContainer.GetJoystickAt(i);

                if(Engine.JoystickContainer.GetIsPresentAt(i))
                {
                    if (Controller.JoyPush(joystick, 1)) AddPlayer(i);
                    if (Controller.JoyPush(joystick, 0)) GotoPrevScene(i);
                }
            }

            // スタートボタンが押されていたら次のシーンに移る
            bool IsStartHold = false;

            foreach(Player player in Game.Player.Where(p => p != null))
                IsStartHold |= ((Controller)player.Controller).GetHold(Button.Start);

            StartHold = IsStartHold ? StartHold + 1 : 0;

            if(StartHold == 40)
            {
                Sound.Play(SE_Decision);
                Engine.ChangeSceneWithTransition(new SelectScene(), new TransitionFade(1, 1));
            }
        }
    }
}
