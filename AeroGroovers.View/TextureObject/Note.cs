using asd;

using System.Diagnostics;
using System.Linq;

using AeroGroovers.Model;

using static System.Math;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        private class Note : TextureObject2D
        {
            /// <summary>
            /// ノーツの移動を管理するタイマー
            /// </summary>
            public static Stopwatch Stopwatch { get; private set; } = new Stopwatch();

            /// <summary>
            /// 経過時間をミリ秒で取得
            /// </summary>
            public static long Time => Stopwatch.ElapsedMilliseconds - 1000;

            /// <summary>
            /// このオブジェクトに紐づいている情報
            /// </summary>
            private NoteInfo NoteInfo;

            /// <summary>
            /// 紐づいているプレイヤー
            /// </summary>
            private Player Player;

            /// <summary>
            /// 紐づいているプレイヤーのコントローラー
            /// </summary>
            private Controller Controller;

            /// <summary>
            /// 紐づいているプレイヤーのキャラクター
            /// </summary>
            private Character Character;

            /// <summary>
            /// 直前のノーツに関する情報
            /// </summary>
            private Note PrevNote;

            /// <summary>
            /// ノーツの描画部分
            /// </summary>
            private static RectF[] NoteSRC = {
            new RectF(0,  0,  49, 12),
            new RectF(0, 12,  49, 12),
            new RectF(0, 24,  49, 12),
            new RectF(0, 36,  49, 12),
            new RectF(0, 48,  62, 12)
        };

            /// <summary>
            /// 押されるべきボタン
            /// </summary>
            private Button[] Button;

            /// <param name="player"> ノーツに紐づけるプレイヤー </param>
            /// <param name="info"> ノーツの情報 </param>
            /// <param name="prev"> 直前のノーツに関する情報 </param>
            public Note(Player player, NoteInfo info, Note prev)
            {
                Player = player;
                Controller = (Controller)Player.Controller;
                Character = Player.Character;
                NoteInfo = info;
                PrevNote = prev;

                Texture = Engine
                    .Graphics
                    .CreateTexture2D("Resources/Graphics/Notes.png");
                Src = NoteSRC[info.Number - 1];
                CenterPosition = Src.Size * new Vector2DF(0, 0.5f);
                Position = new Vector2DF(310 * player.PlayerNumber - ((info.Number == 5) ? 268 : 51 * info.Number), 0);

                DrawingPriority = 3;

                if (Controller.ControllerType == ControllerType.Keyboard)
                    switch (info.Number)
                    {
                        case 1: Button = new Button[] { Model.Button.Y }; break;
                        case 2: Button = new Button[] { Model.Button.X }; break;
                        case 3: Button = new Button[] { Model.Button.B }; break;
                        case 4: Button = new Button[] { Model.Button.A }; break;
                        case 5: Button = new Button[] { Model.Button.L }; break;
                    }
                else
                    switch (info.Number)
                    {
                        case 1: Button = new Button[] { Model.Button.A }; break;
                        case 2: Button = new Button[] { Model.Button.X }; break;
                        case 3: Button = new Button[] { Model.Button.B }; break;
                        case 4: Button = new Button[] { Model.Button.Y }; break;
                        case 5:
                            Button = new Button[] { Model.Button.Up,
                                                    Model.Button.Down,
                                                    Model.Button.Left,
                                                    Model.Button.Right }; break;
                    }
            }

            protected override void OnUpdate()
            {
                void AddEffect(Judge judge)
                {
                    Layer2D layer = ((GameScene)Layer.Scene).EffectLayer;
                    Vector2DF p = new Vector2DF(Position.X + Src.Size.X * 0.5f, 520);
                    layer.AddObject(new HitEffect(judge, p));
                }

                if (Time < -500)
                {
                    double v = Pow(Stopwatch.ElapsedMilliseconds, 2);
                    Color = new Color(255, 255, 255, (int)(v * 0.00102));
                }
                else
                {
                    Color = new Color(255, 255, 255);
                }

                // ノーツの移動
                int difference = (int)(Time - NoteInfo.Timing);
                Position = new Vector2DF(Position.X, 520 + difference * 0.05f * Player.HighSpeed);

                // 直前のノーツがnullか,Disposeメソッドがすでに呼び出された場合
                if (PrevNote == null || !PrevNote.IsAlive)
                {
                    // 推れるべきボタンがどれか一つ押されていた場合
                    bool ButtonPressed =
                        Button.Any(b => Controller.GetPush(b));

                    if (difference > 166 || (difference >= -166 && ButtonPressed))
                    {
                        // キャラクターを更新する
                        Character.AddEffet = AddEffect;
                        Character.Update(difference);

                        Dispose();
                    }
                }
            }
        }
    }
}