namespace AeroGroovers.View
{
    /// <summary>
    /// ボタンの種類
    /// </summary>
    public enum Button
    {
        /// <summary> Aボタン </summary>
        A,

        /// <summary> Bボタン </summary>
        B,

        /// <summary> Xボタン </summary>
        X,

        /// <summary> Yボタン </summary>
        Y,

        /// <summary> Lボタン </summary>
        L,

        /// <summary> Rボタン </summary>
        R,

        /// <summary> 上ボタン </summary>
        Up,

        /// <summary> 下ボタン </summary>
        Down,

        /// <summary> 左ボタン </summary>
        Left,

        /// <summary> 右ボタン </summary>
        Right,

        /// <summary> スタートボタン </summary>
        Start,

        /// <summary> セレクトボタン </summary>
        Select
    }

    /// <summary>
    /// コントローラーの種類
    /// </summary>
    public enum ControllerType
    {
        /// <summary> キーボード </summary>
        Keyboard,

        /// <summary> PlayStation4 </summary>
        PlayStation4,

        /// <summary> Xbox360 </summary>
        Xbox360,

        /// <summary> その他のコントローラー </summary>
        Other
    }
}
