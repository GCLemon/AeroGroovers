using asd;

using System.Collections.Generic;

using AeroGroovers.Model;

using static System.Math;

namespace AeroGroovers.View
{
    public partial class EntryScene
    {
        /// <summary>
        /// コントローラー登録シーンで用いるウィンドウ
        /// </summary>
        private class EntryWindow : Window
        {
            /// <summary>
            /// ウィンドウの状態
            /// </summary>
            private enum WindowState
            {
                /// <summary> ウィンドウを開く指示を受けた </summary>
                Opened,

                /// <summary> ウィンドウが開いている </summary>
                Opening,

                /// <summary> ウィンドウを閉じる指示を受けた </summary>
                Closed,

                /// <summary> ウィンドウが閉じている </summary>
                Closing
            }

            /// <summary>
            /// ウィンドウの今の状態
            /// </summary>
            private WindowState CurrentState;

            /// <summary>
            /// 紐づいているプレイヤーの番号
            /// </summary>
            private int PlayerNumber;

            /// <summary>
            /// フレームカウンタ
            /// </summary>
            private int FrameCounter;

            /// <summary>
            /// 戻るボタンが長押しされたフレーム数
            /// </summary>
            private int B_Hold;

            /// <summary>
            /// テキストオブジェクトの中心座標
            /// </summary>
            private static readonly Vector2DF center = new Vector2DF(0.5f, 0.5f);

            //////////////////////////////////////////////////
            //
            //   WindowStateがClosingの時に表示するテキスト
            //

            private AGText press_a = new AGText(24, 0, center) { Position = new Vector2DF(135, 40) };

            //////////////////////////////////////////////////
            //
            //   WindowStateがOpeningの時に表示するテキスト
            //

            private AGText playr_l = new AGText(36, 0, center)
            {
                Position = new Vector2DF(135, 40),
                IsDrawn = false
            };
            private AGText style_l = new AGText(24, 0, center)
            {
                Position = new Vector2DF(135, 100),
                IsDrawn = false
            };
            private AGText style_v = new AGText(36, 0, center)
            {
                Position = new Vector2DF(135, 140),
                IsDrawn = false
            };
            private AGText check_l = new AGText(24, 0, center)
            {
                Position = new Vector2DF(135, 200),
                IsDrawn = false
            };
            private AGText press_b = new AGText(24, 0, center)
            {
                Position = new Vector2DF(135, 420),
                IsDrawn = false
            };

            //////////////////////////////////////////////////
            //
            //   コントローラーの動作確認をする時に表示するテキスト
            //

            private Dictionary<Button, AGText> check = new Dictionary<Button, AGText>
        {
            { Button.A, new AGText(24, 0, center)
            {
                Position = new Vector2DF(54, 240),
                IsDrawn = false
            }},
            { Button.B, new AGText(24, 0, center)
            {
                Position = new Vector2DF(108, 240),
                IsDrawn = false
            }},
            { Button.X, new AGText(24, 0, center)
            {
                Position = new Vector2DF(162, 240),
                IsDrawn = false
            }},
            { Button.Y, new AGText(24, 0, center)
            {
                Position = new Vector2DF(216, 240),
                IsDrawn = false
            }},
            { Button.L, new AGText(24, 0, center)
            {
                Position = new Vector2DF(90, 270),
                IsDrawn = false
            }},
            { Button.R, new AGText(24, 0, center)
            {
                Position = new Vector2DF(180, 270),
                IsDrawn = false
            }},
            { Button.Up,     new AGText(24, 0, center)
            {
                Position = new Vector2DF(162, 300),
                IsDrawn = false
            }},
            { Button.Down,   new AGText(24, 0, center)
            {
                Position = new Vector2DF(108, 300),
                IsDrawn = false
            }},
            { Button.Left, new AGText(24, 0, center)
            {
                Position = new Vector2DF(54, 300),
                IsDrawn = false
            }},
            { Button.Right,  new AGText(24, 0, center)
            {
                Position = new Vector2DF(216, 300),
                IsDrawn = false
            }},
            { Button.Start,  new AGText(24, 0, center)
            {
                Position = new Vector2DF(90, 330),
                IsDrawn = false
            }},
            { Button.Select, new AGText(24, 0, center)
            {
                Position = new Vector2DF(180, 330),
                IsDrawn = false
            }}
        };

            public EntryWindow(int player_number) : base(270, 100)
            {
                // 番号を設定する
                PlayerNumber = player_number;

                // ウィンドウの今の状態
                CurrentState = WindowState.Closing;

                // 引数に指定されたプレイヤー番号に応じてウィンドウの色を変える
                switch (player_number)
                {
                    case 1: Color = new Color(87, 209, 235); break;
                    case 2: Color = new Color(245, 0, 94); break;
                    case 3: Color = new Color(151, 225, 36); break;
                    case 4: Color = new Color(224, 213, 97); break;
                }

                // ウィンドウの位置を変更する
                Position = new Vector2DF(310 * player_number - 266, 175);

                // ウィンドウに表示するテキストを変更する
                press_a.SetText("決定ボタンで参加");
                playr_l.SetText("Player " + PlayerNumber);
                style_l.SetText("< Play Style >");
                style_v.SetText("");
                check_l.SetText("< Button Check >");
                press_b.SetText("キャンセルボタン\n長押しで登録解除");

                check[Button.A].SetText("A");
                check[Button.B].SetText("B");
                check[Button.X].SetText("X");
                check[Button.Y].SetText("Y");
                check[Button.L].SetText("L");
                check[Button.R].SetText("R");
                check[Button.Up].SetText("↑");
                check[Button.Down].SetText("↓");
                check[Button.Left].SetText("←");
                check[Button.Right].SetText("→");
                check[Button.Start].SetText("Start");
                check[Button.Select].SetText("Select");

                // オブジェクトを追加する
                AddObjects(press_a, playr_l, style_l, style_v, check_l, press_b);
                foreach (var pair in check) AddObject(pair.Value);
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();

                switch (CurrentState)
                {
                    case WindowState.Opened: OnOpened(); break;
                    case WindowState.Opening: OnOpening(); break;
                    case WindowState.Closed: OnClosed(); break;
                    case WindowState.Closing: OnClosing(); break;
                }
            }

            /// <summary>
            /// ウィンドウを開く指示を受けた時の処理
            /// </summary>
            private void OnOpened()
            {
                // ウィンドウの高さを変更する
                float v = FrameCounter;
                float h = (float)(470 - 370 * Pow(v / 10.0, 2));
                WindowSize = new Vector2DF(270, h);

                if (FrameCounter == 0)
                {
                    // 状態を変更し,メソッド内の処理を終える
                    NoisyValue = 0.025f;

                    playr_l.IsDrawn =
                    style_l.IsDrawn =
                    style_v.IsDrawn =
                    check_l.IsDrawn =
                    press_b.IsDrawn = true;

                    foreach (var pair in check) pair.Value.IsDrawn = true;


                    Player player = Game.Player[PlayerNumber - 1];
                    style_v.SetText(((Controller)player.Controller).ControllerType.ToString());

                    CurrentState = WindowState.Opening;
                    return;
                }

                --FrameCounter;
            }

            /// <summary>
            /// ウィンドウが開いている時の処理
            /// </summary>
            private void OnOpening()
            {
                // プレイヤーの取得
                Player player = Game.Player[PlayerNumber - 1];

                // プレイやーがnullになった時
                // すなわちプレイヤーが解除された時
                if (player == null)
                {
                    // フレームカウンタを設定し,状態を変更する
                    FrameCounter = 10;

                    playr_l.IsDrawn =
                    style_l.IsDrawn =
                    style_v.IsDrawn =
                    check_l.IsDrawn =
                    press_b.IsDrawn = false;

                    foreach (var pair in check) pair.Value.IsDrawn = false;

                    NoisyValue = 1;

                    Sound.Play(SE_Close);
                    CurrentState = WindowState.Closed;
                }

                else
                {
                    // コントローラーの動作確認のためのテキストの設定
                    foreach (var pair in check)
                    {
                        pair.Value.Color =
                            ((Controller)player.Controller).GetHold(pair.Key)
                            ? new Color(255, 255, 255) : new Color(63, 63, 63);
                    }

                    // プレイヤーの登録解除
                    B_Hold =
                        ((Controller)player.Controller).GetHold(Button.B)
                        ? B_Hold + 1 : 0;

                    if (B_Hold == 40)
                        Game.DeletePlayer(PlayerNumber);
                }
            }

            /// <summary>
            /// ウィンドウを閉じる指示を受けた時の処理
            /// </summary>
            private void OnClosed()
            {
                // ウィンドウの高さを変更する
                float v = FrameCounter;
                float h = (float)(100 + 370 * Pow(v / 10.0, 2));
                WindowSize = new Vector2DF(270, h);

                if (FrameCounter == 0)
                {
                    // 状態を変更し,メソッド内の処理を終える
                    NoisyValue = 0.025f;
                    press_a.IsDrawn = true;
                    CurrentState = WindowState.Closing;
                    return;
                }

                --FrameCounter;
            }

            /// <summary>
            /// ウィンドウが閉じている時の処理
            /// </summary>
            private void OnClosing()
            {
                press_a.Color = new Color(255, 255, 255, (int)(255 * Pow(Sin(FrameCounter / 40.0), 2)));

                // プレイヤーの取得
                Player player = Game.Player[PlayerNumber - 1];

                // プレイやーがnullではなくなった時,
                // すなわちプレイヤーが登録された時
                if (player != null)
                {
                    // 状態を変更する
                    NoisyValue = 1;
                    press_a.IsDrawn = false;
                    FrameCounter = 10;

                    Sound.Play(SE_Open);
                    CurrentState = WindowState.Opened;
                }

                ++FrameCounter;
            }
        }
    }
}