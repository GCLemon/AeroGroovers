using asd;

namespace AeroGroovers.View
{
    public class OptionWidnow : Window
    {
        /// <summary>
        /// ウィンドウの状態
        /// </summary>
        private enum WindowState
        {
            /// <summary> 設定する項目を選んでいる </summary>
            Choosing,

            /// <summary> 難易度を設定している </summary>
            Set_Difficulty,

            /// <summary> ノーツ速度を設定している </summary>
            Set_HighSpeed,

            /// <summary> キャラクターを設定している </summary>
            Set_Character,

            /// <summary> 設定が終了している </summary>
            Finished
        }

        // メニューの項目
        private enum MenuItem
        {
            /// <summary> 難易度 </summary>
            Difficulty,

            /// <summary> ノーツ速度 </summary>
            HighSpeed,

            /// <summary> キャラクター </summary>
            Character,

            /// <summary> 設定終了 </summary>
            Finish
        }

        public OptionWidnow() : base(270, 450)
        {
        }
    }
}
