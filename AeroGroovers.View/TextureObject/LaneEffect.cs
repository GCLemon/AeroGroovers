using asd;

using System;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class GameScene
    {
        /// <summary>
        /// レーンエフェクト
        /// </summary>
        private class LaneEffect : TextureObject2D
        {
            Controller Controller;
            int LaneNumber;

            int Alpha;
            int Count;

            bool IsButtonPressed
            {
                get
                {
                    if (Controller.ControllerType == ControllerType.Keyboard)
                    {
                        // キーボードの場合
                        switch (LaneNumber)
                        {
                            case 1: return Controller.GetHold(Button.Y);
                            case 2: return Controller.GetHold(Button.X);
                            case 3: return Controller.GetHold(Button.B);
                            case 4: return Controller.GetHold(Button.A);
                            case 5: return Controller.GetHold(Button.L);
                        }
                        return false;
                    }
                    else
                    {
                        // その他の場合
                        switch (LaneNumber)
                        {
                            case 1: return Controller.GetHold(Button.A);
                            case 2: return Controller.GetHold(Button.X);
                            case 3: return Controller.GetHold(Button.B);
                            case 4: return Controller.GetHold(Button.Y);
                            case 5:
                                return Controller.GetHold(Button.Left)
                                    || Controller.GetHold(Button.Right)
                                    || Controller.GetHold(Button.Up)
                                    || Controller.GetHold(Button.Down);
                        }
                        return false;
                    }
                }
            }

            bool IsButtonPressed_prev;

            public LaneEffect(Controller controller, int lane_number)
            {
                Controller = controller;
                LaneNumber = lane_number;

                switch (lane_number)
                {
                    case 1: Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/LaneEffect_R.png"); break;
                    case 2: Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/LaneEffect_Y.png"); break;
                    case 3: Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/LaneEffect_G.png"); break;
                    case 4: Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/LaneEffect_B.png"); break;
                    case 5: Texture = Engine.Graphics.CreateTexture2D("Resources/Graphics/LaneEffect_O.png"); break;
                }

                CenterPosition = new Vector2DF(0, Texture.Size.Y);

                switch (lane_number)
                {
                    case 1: Position = new Vector2DF(259, 519); break;
                    case 2: Position = new Vector2DF(208, 519); break;
                    case 3: Position = new Vector2DF(157, 519); break;
                    case 4: Position = new Vector2DF(106, 519); break;
                    case 5: Position = new Vector2DF(42, 519); break;
                }
            }

            protected override void OnUpdate()
            {
                if (IsButtonPressed) Alpha = 255;
                else
                {
                    if (IsButtonPressed_prev) Count = 0;
                    Alpha = (int)((-Math.Pow(Count - 8, 3)) / 512 * 255);
                }

                Color = new Color(255, 255, 255, Alpha);

                if (Count < 8) ++Count;

                IsButtonPressed_prev = IsButtonPressed;
            }
        }
    }
}