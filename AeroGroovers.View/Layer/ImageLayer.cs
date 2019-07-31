using asd;

using System.Linq;

using AeroGroovers.Model;

using static System.Math;

namespace AeroGroovers.View
{
    public partial class SelectScene
    {
        /// <summary>
        /// ジャケットが動いている方向
        /// </summary>
        private enum Direction { Left, Right, Stay }

        /// <summary>
        /// ジャケットを描画するためのレイヤー
        /// </summary>
        private class ImageLayer : Layer2D
        {
            // フレームカウンター
            private int FrameCounter;

            public Direction Direction { get; private set; } = Direction.Stay;

            // ジャケットオブジェクト
            TextureObject2D[] JacketObjects = new TextureObject2D[7];

            protected override void OnAdded()
            {
                // ジャケットオブジェクトの設定
                for (int i = 0; i < 7; ++i)
                {
                    JacketObjects[i] = new TextureObject2D();
                    Texture2D jacket = Engine.Graphics.CreateTexture2D(Game.Scores[i].JacketPath);
                    JacketObjects[i].Texture = jacket;
                    JacketObjects[i].CenterPosition = jacket.Size.To2DF() / 2;
                    float pos_x = (i != 3) ? 640 + (72 + 248 * Abs(i - 3)) * Sign(i - 3) : 640;
                    JacketObjects[i].Scale = (i - 3 != 0) ? new Vector2DF(0.6f, 0.6f) : new Vector2DF(1, 1);
                    JacketObjects[i].Position = new Vector2DF(pos_x, 300);
                    AddObject(JacketObjects[i]);
                }
            }

            protected override void OnUpdated()
            {
                switch(Direction)
                {
                    case Direction.Left:
                    case Direction.Right: OnMoving(); break;
                    case Direction.Stay: OnStaying(); break;
                }
            }

            /// <summary>
            /// ジャケット画像が動いている時の処理
            /// </summary>
            private void OnMoving()
            {
                float v = 1 - (float)Pow(1 - FrameCounter / 12.0, 3);
                int n = 0;
                switch (Direction)
                {
                    case Direction.Left: n = 2; break;
                    case Direction.Right: n = 4; break;
                }

                // ジャケットの位置の設定
                for (int i = 0; i < 7; ++i)
                {
                    float pos_r = (i != 3) ? 640 + (72 + 248 * Abs(i - 3)) * Sign(i - 3) : 640;
                    float pos_l = (i != n) ? 640 + (72 + 248 * Abs(i - n)) * Sign(i - n) : 640;
                    JacketObjects[i].Position = new Vector2DF(pos_r * (1 - v) + pos_l * v, 300);
                }

                // 大きさの設定
                JacketObjects[3].Scale = new Vector2DF(1 - v + 0.6f * v, 1 - v + 0.6f * v);
                JacketObjects[n].Scale = new Vector2DF(0.6f * (1 - v) + v, 0.6f * (1 - v) + v);

                if (FrameCounter == 12) ResetState();

                else ++FrameCounter;
            }

            /// <summary>
            /// 状態を元に戻す
            /// </summary>
            void ResetState()
            {
                for (int i = 0; i < 7; ++i)
                {
                    Texture2D jacket = Engine.Graphics.CreateTexture2D(Game.Scores[i].JacketPath);
                    JacketObjects[i].Texture = jacket;
                    float pos_x = (i != 3) ? 640 + (72 + 248 * Abs(i - 3)) * Sign(i - 3) : 640;
                    JacketObjects[i].Scale = (i - 3 != 0) ? new Vector2DF(0.6f, 0.6f) : new Vector2DF(1, 1);
                    JacketObjects[i].Position = new Vector2DF(pos_x, 300);
                }

                FrameCounter = 0;
                Direction = Direction.Stay;

                // ここで音を再生
                SoundSource sound = Sound.CreateBGM(Game.Score.SoundPath);
                sound.IsLoopingMode = true;
                BGM_ID = Sound.Play(sound);

                // 入力を受け付ける
                Controller.AcceptInput = true;
            }


            /// <summary>
            /// ジャケット画像が止まっている時の処理
            /// </summary>
            private void OnStaying()
            {
                foreach(Player player in Game.Player.Where(p => p != null))
                {
                    // 画像を左に動かす
                    if(((Controller)player.Controller).GetHold(Button.Left))
                    {
                        Sound.Play(SE_Select);

                        Sound.Stop(BGM_ID);
                        Game.DecrementScoreIndex();
                        Direction = Direction.Left;

                        Controller.AcceptInput = false;
                    }

                    // 画像を右に動かす
                    if (((Controller)player.Controller).GetHold(Button.Right))
                    {
                        Sound.Play(SE_Select);

                        Sound.Stop(BGM_ID);
                        Game.IncrementScoreIndex();
                        Direction = Direction.Right;

                        Controller.AcceptInput = false;
                    }

                    // 次のシーンへ
                    if (((Controller)player.Controller).GetPush(Button.A))
                    {
                        Sound.Stop(BGM_ID);
                        Sound.Play(SE_Decision);
                        Engine.ChangeSceneWithTransition(new OptionScene(), new TransitionFade(1, 1));
                    }

                    // 前のシーンに戻る
                    if (((Controller)player.Controller).GetPush(Button.B))
                    {
                        Sound.Stop(BGM_ID);
                        Sound.Play(SE_Cancel);
                        Engine.ChangeSceneWithTransition(new EntryScene(), new TransitionFade(1, 1));
                    }
                }
            }
        }
    }
}
