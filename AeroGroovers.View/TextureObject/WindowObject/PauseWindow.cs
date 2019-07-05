using asd;

using System.Linq;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// ポーズ中に表示するウィンドウ
        /// </summary>
        private class PauseWindow : Window
        {
            /// <summary>
            /// メニューの項目
            /// </summary>
            private enum MenuItem
            {
                /// <summary> 再開する </summary>
                Resume,

                /// <summary> 最初からやり直す </summary>
                Retry,

                /// <summary> 曲選択に戻る </summary>
                TuneSelect
            }

            /// <summary>
            /// 選択中の項目
            /// </summary>
            MenuItem CurrentItem = MenuItem.Resume;

            /// <summary>
            /// ポーズを解除するメソッドを格納するためのデリゲート
            /// </summary>
            public delegate void ResumeMethod();

            /// <summary>
            /// ポーズを解除するメソッド
            /// </summary>
            public ResumeMethod Resume { private get; set; }

            /// <summary>
            /// テキストオブジェクトの中心座標
            /// </summary>
            private static Vector2DF center = new Vector2DF(0.5f, 0.5f);

            /// <summary>
            /// 「Pause」
            /// </summary>
            private AGText pause = new AGText(54, 0, center);

            /// <summary>
            /// 「Resume」
            /// </summary>
            private AGText resume = new AGText(36, 0, center);

            /// <summary>
            /// 「Retry」
            /// </summary>
            private AGText retry = new AGText(36, 0, center);

            /// <summary>
            /// 「Tune Select」
            /// </summary>
            private AGText select = new AGText(36, 0, center);

            public PauseWindow() : base(360, 300)
            {
                NoisyValue = 0.025f;
                Position = new Vector2DF(440, 280);
                Color = new Color(127, 127, 127);

                pause.SetText("PAUSE");
                resume.SetText("Resume");
                retry.SetText("Retry");
                select.SetText("Tune Select");

                pause.Position = new Vector2DF(180, 50);
                resume.Position = new Vector2DF(180, 130);
                retry.Position = new Vector2DF(180, 180);
                select.Position = new Vector2DF(180, 230);

                AddObjects
                (
                    pause,
                    resume,
                    retry,
                    select
                );
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();

                void SetTextColor(AGText text, MenuItem item)
                {
                    text.Color =
                        CurrentItem == item
                        ? new Color(255, 255, 255)
                        : new Color(63, 63, 63);
                }

                foreach (var player in Scene.Game.Player.Where(p => p != null))
                {
                    // コントローラーを取得
                    Controller controller = (Controller)player.Controller;

                    // Aボタンが押された場合
                    if (controller.GetPush(Button.A))
                    {
                        switch (CurrentItem)
                        {
                            case MenuItem.Resume:

                                // ポーズを解除する
                                Resume();
                                break;

                            case MenuItem.Retry:

                                // 新しくゲームシーンを作成し,そのシーンに移る
                                Sound.Stop(BGM_ID);
                                Sound.Play(SE_BattleStart);
                                Engine.ChangeSceneWithTransition(new GameScene(), new TransitionFade(1.276f, 2.996f));
                                break;

                            case MenuItem.TuneSelect:

                                // 曲選択に戻る
                                Sound.Stop(BGM_ID);
                                Sound.Play(SE_Cancel);
                                Engine.ChangeSceneWithTransition(new SelectScene(), new TransitionFade(1, 1));
                                break;
                        }
                    }

                    // Bボタンが押された場合
                    if (controller.GetPush(Button.B))
                    {
                        Resume();
                    }

                    // 上ボタンが押された場合
                    if (controller.GetPush(Button.Up))
                    {
                        int index = ((int)CurrentItem - 1) % 3;
                        if (index < 0) index += 3;
                        CurrentItem = (MenuItem)index;
                    }

                    // 下ボタンが押された場合
                    if (controller.GetPush(Button.Down))
                    {
                        int index = ((int)CurrentItem + 1) % 3;
                        if (index < 0) index += 3;
                        CurrentItem = (MenuItem)index;
                    }

                    // テキストの色を変更する
                    SetTextColor(resume, MenuItem.Resume);
                    SetTextColor(retry, MenuItem.Retry);
                    SetTextColor(select, MenuItem.TuneSelect);
                }
            }
        }
    }
}