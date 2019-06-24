using asd;

namespace AeroGroovers.View
{
    /// <summary>
    /// 入力情報を取得するオブジェクトの型
    /// </summary>
    public partial class Controller
    {
        /// <summary>
        /// 入力を受け付けるか
        /// </summary>
        public static bool AcceptInput { private get; set; } = true;

        /// <summary>
        /// キーボードのキーが押されたか
        /// </summary>
        public static bool KeyPush(Keys keys) =>
            AcceptInput && Engine.Keyboard.GetKeyState(keys) == ButtonState.Push;

        /// <summary>
        /// キーボードのキーが押されているか
        /// </summary>
        public static bool KeyHold(Keys keys) =>
            AcceptInput && Engine.Keyboard.GetKeyState(keys) == ButtonState.Hold;

        /// <summary>
        /// キーボードのキーが離されたか
        /// </summary>
        public static bool KeyRelease(Keys keys) =>
            AcceptInput && Engine.Keyboard.GetKeyState(keys) == ButtonState.Release;

        /// <summary>
        /// キーボードのキーが離されているか
        /// </summary>
        public static bool KeyFree(Keys keys) =>
            !AcceptInput || Engine.Keyboard.GetKeyState(keys) == ButtonState.Free;

        /// <summary>
        /// キーボードの押下情報を取得
        /// </summary>
        public static ButtonState GetKeyState(Keys keys) =>
            AcceptInput ? Engine.Keyboard.GetKeyState(keys) : ButtonState.Free;

        /// <summary>
        /// ジョイスティックが接続されているか
        /// </summary>
        public static bool IsPresent(int i) =>
            Engine.JoystickContainer.GetIsPresentAt(i);

        /// <summary>
        /// ジョイスティックを取得する
        /// </summary>
        public static Joystick GetJoystick(int i) =>
            Engine.JoystickContainer.GetJoystickAt(i);

        /// <summary>
        /// ジョイスティックのボタンが押されたか
        /// </summary>
        public static bool JoyPush(Joystick joystick, int buttons) =>
            AcceptInput && joystick.GetButtonState(buttons) == ButtonState.Push;

        /// <summary>
        /// ジョイスティックのボタンが押されているか
        /// </summary>
        public static bool JoyHold(Joystick joystick, int buttons) =>
            AcceptInput && joystick.GetButtonState(buttons) == ButtonState.Hold;

        /// <summary>
        /// ジョイスティックのボタンが押離された
        /// </summary>
        public static bool JoyRelease(Joystick joystick, int buttons) =>
            AcceptInput && joystick.GetButtonState(buttons) == ButtonState.Release;

        /// <summary>
        /// ジョイスティックのボタンが離されているか
        /// </summary>
        public static bool JoyFree(Joystick joystick, int buttons) =>
            !AcceptInput || joystick.GetButtonState(buttons) == ButtonState.Free;

        /// <summary>
        /// キーボードの押下情報を取得
        /// </summary>
        public static ButtonState GetJoyState(Joystick joystick, int buttons) =>
            AcceptInput ? joystick.GetButtonState(buttons) : ButtonState.Free;
    }
}
