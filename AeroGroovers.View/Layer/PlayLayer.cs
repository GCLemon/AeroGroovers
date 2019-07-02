using asd;

using System.Collections.Generic;

using AeroGroovers.Model;

namespace AeroGroovers.View
{

    public partial class GameScene
    {
        private class PlayLayer : Layer2D
        {
            /// <summary>
            /// レイヤーに紐づいているプレイヤー
            /// </summary>
            private Player Player;

            /// <summary>
            /// 流す譜面
            /// </summary>
            private List<NoteInfo> Score;

            /// <summary>
            /// ノーツのインデックス
            /// </summary>
            private int NoteIndex;

            /// <summary>
            /// 前のノーツを記憶
            /// </summary>
            private Note[] PrevNote = new Note[5];

            public PlayLayer(int i)
            {
                Player = Game.Player[i];
                Score = Game.Score.Notes[Player.Difficulty];

                // レーンの背景の配置
                AddObject(new TextureObject2D
                {
                    Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/Lane_Back.png"),
                    Position = new Vector2DF(310 * i + 40, 120),
                    DrawingPriority = 0
                });

                // コンボテキストの配置
                AddObject(new ComboText(i + 1) { DrawingPriority = 10 });

                // レーンの境目の配置
                AddObject(new TextureObject2D
                {
                    Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/Lane_Border.png"),
                    Position = new Vector2DF(310 * i + 40, 120),
                    DrawingPriority = 2
                });

                // カメラの追加
                CameraObject2D camera = new CameraObject2D();
                camera.Src = new RectI(310 * i + 40, 120, 270, 450);
                camera.Dst = new RectI(310 * i + 40, 120, 270, 450);
                AddObject(camera);

                n = i;
            }

            int n;

            protected override void OnAdded()
            {
                // レーンエフェクトの追加
                for (int j = 1; j <= 5; ++j)
                {
                    var effect = new LaneEffect((Controller)Player.Controller, j);
                    ((GameScene)Scene).EffectLayer.AddObject(effect);
                    effect.Position += new Vector2DF(310 * n, 0);
                }
            }

            protected override void OnUpdated()
            {
                // ノートオブジェクトの配置
                while (NoteIndex < Score.Count && Score[NoteIndex].Timing < Note.Time + 10000)
                {
                    int i = Score[NoteIndex].Number - 1;
                    Note note = new Note(Player, Score[NoteIndex], PrevNote[i]);
                    AddObject(note);
                    PrevNote[i] = note;

                    ++NoteIndex;
                }
            }
        }
    }
}
