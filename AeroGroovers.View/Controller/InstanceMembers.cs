using asd;

using AeroGroovers.Model;

namespace AeroGroovers.View
{
    public partial class Controller : Model.Controller
    {
        private Joystick Joystick;

        /// <summary>
        /// コントローラーの種類
        /// </summary>
        public ControllerType ControllerType { get; private set; }

        /// <summary>
        /// キーボードを登録する
        /// </summary>
        public Controller() : base(-1)
        {
            ControllerType = ControllerType.Keyboard;
        }

        /// <summary>
        /// ジョイスティックを登録する
        /// </summary>
        public Controller(int joystick_index) : base(joystick_index)
        {
            Joystick = Engine.JoystickContainer.GetJoystickAt(joystick_index);

            switch(Joystick.JoystickType)
            {
                case JoystickType.XBOX360:
                    ControllerType = ControllerType.Xbox360;
                    break;
                case JoystickType.PS4:
                    ControllerType = ControllerType.PlayStation4;
                    break;
                case JoystickType.Other:
                    ControllerType = ControllerType.Other;
                    break;
            }
        }

        /// <summary>
        /// ボタンが押されたか
        /// </summary>
        public bool GetPush(Button button)=>
            GetButtonState(button, ButtonState.Push);

        /// <summary>
        /// ボタンが押されているか
        /// </summary>
        public bool GetHold(Button button) =>
            GetButtonState(button, ButtonState.Hold);

        /// <summary>
        /// ボタンが離されたか
        /// </summary>
        public bool GetRelease(Button button) =>
            GetButtonState(button, ButtonState.Release);

        /// <summary>
        /// ボタンが離されているか
        /// </summary>
        public bool GetFree(Button button) =>
            GetButtonState(button, ButtonState.Free);

        private bool GetButtonState(Button button, ButtonState state)
        {
            switch(ControllerType)
            {
                case ControllerType.Keyboard:
                    switch(button)
                    {
                        case Button.A:      return GetKeyState(Keys.Z)          == state;
                        case Button.B:      return GetKeyState(Keys.X)          == state;
                        case Button.X:      return GetKeyState(Keys.C)          == state;
                        case Button.Y:      return GetKeyState(Keys.V)          == state;
                        case Button.L:      return GetKeyState(Keys.LeftShift)  == state;
                        case Button.R:      return GetKeyState(Keys.RightShift) == state;
                        case Button.Up:     return GetKeyState(Keys.Up)         == state;
                        case Button.Down:   return GetKeyState(Keys.Down)       == state;
                        case Button.Left:   return GetKeyState(Keys.Left)       == state;
                        case Button.Right:  return GetKeyState(Keys.Right)      == state;
                        case Button.Start:  return GetKeyState(Keys.A)          == state;
                        case Button.Select: return GetKeyState(Keys.S)          == state;
                    }
                    return false;
                case ControllerType.Xbox360:
                    switch (button)
                    {
                        case Button.A:      return GetJoyState(Joystick, 1)  == state;
                        case Button.B:      return GetJoyState(Joystick, 0)  == state;
                        case Button.X:      return GetJoyState(Joystick, 3)  == state;
                        case Button.Y:      return GetJoyState(Joystick, 2)  == state;
                        case Button.L:      return GetJoyState(Joystick, 4)  == state;
                        case Button.R:      return GetJoyState(Joystick, 5)  == state;
                        case Button.Up:     return GetJoyState(Joystick, 16) == state;
                        case Button.Down:   return GetJoyState(Joystick, 18) == state;
                        case Button.Left:   return GetJoyState(Joystick, 19) == state;
                        case Button.Right:  return GetJoyState(Joystick, 17) == state;
                        case Button.Start:  return GetJoyState(Joystick, 12) == state
                                                || GetJoyState(Joystick, 13) == state;
                        case Button.Select: return GetJoyState(Joystick, 8)  == state
                                                || GetJoyState(Joystick, 9)  == state;
                    }
                    return false;
                case ControllerType.PlayStation4:
                    switch (button)
                    {
                        case Button.A:      return GetJoyState(Joystick, 1)  == state;
                        case Button.B:      return GetJoyState(Joystick, 0)  == state;
                        case Button.X:      return GetJoyState(Joystick, 3)  == state;
                        case Button.Y:      return GetJoyState(Joystick, 2)  == state;
                        case Button.L:      return GetJoyState(Joystick, 4)  == state;
                        case Button.R:      return GetJoyState(Joystick, 5)  == state;
                        case Button.Up:     return GetJoyState(Joystick, 16) == state;
                        case Button.Down:   return GetJoyState(Joystick, 18) == state;
                        case Button.Left:   return GetJoyState(Joystick, 19) == state;
                        case Button.Right:  return GetJoyState(Joystick, 17) == state;
                        case Button.Start:  return GetJoyState(Joystick, 12) == state
                                                || GetJoyState(Joystick, 13) == state;
                        case Button.Select: return GetJoyState(Joystick, 8)  == state
                                                || GetJoyState(Joystick, 9)  == state;
                    }
                    return false;
                case ControllerType.Other:
                    switch (button)
                    {
                        case Button.A:      return GetJoyState(Joystick, 1)  == state;
                        case Button.B:      return GetJoyState(Joystick, 0)  == state;
                        case Button.X:      return GetJoyState(Joystick, 3)  == state;
                        case Button.Y:      return GetJoyState(Joystick, 2)  == state;
                        case Button.L:      return GetJoyState(Joystick, 4)  == state;
                        case Button.R:      return GetJoyState(Joystick, 5)  == state;
                        case Button.Up:     return GetJoyState(Joystick, 16) == state;
                        case Button.Down:   return GetJoyState(Joystick, 18) == state;
                        case Button.Left:   return GetJoyState(Joystick, 19) == state;
                        case Button.Right:  return GetJoyState(Joystick, 17) == state;
                        case Button.Start:  return GetJoyState(Joystick, 12) == state
                                                || GetJoyState(Joystick, 13) == state;
                        case Button.Select: return GetJoyState(Joystick, 8)  == state
                                                || GetJoyState(Joystick, 9)  == state;
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}
