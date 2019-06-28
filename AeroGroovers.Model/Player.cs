namespace AeroGroovers.Model
{
    /// <summary>
    /// プレイヤーが持つ情報
    /// </summary>
    public class Player
    {
        //////////////////////////////////////////////////
        //
        //    インスタンスが作られるときに設定する項目
        //    外部クラスから設定不可能
        //

        /// <summary>
        /// プレイヤー番号
        /// </summary>
        public int PlayerNumber;

        /// <summary>
        /// 登録されているコントローラー
        /// </summary>
        public Controller Controller { get; private set; }

        //////////////////////////////////////////////////
        //
        //    OptionSceneで設定する項目
        //    外部クラスから設定可能
        //

        /// <summary>
        /// プレイするときの難易度
        /// </summary>
        public Difficulty Difficulty;

        /// <summary>
        /// ノートの速度
        /// </summary>
        public float HighSpeed;

        /// <summary>
        /// 登録されているキャラクター
        /// </summary>
        public Character Character;

        //////////////////////////////////////////////////
        //
        //    GameSceneで更新する項目
        //    外部クラスから設定不可能
        //

        /// <summary>
        /// JustShootの数
        /// </summary>
        public int JustShoot { get; private set; }

        /// <summary>
        /// Shootの数
        /// </summary>
        public int Shoot { get; private set; }

        /// <summary>
        /// Hitの数
        /// </summary>
        public int Hit { get; private set; }

        /// <summary>
        /// Missの数
        /// </summary>
        public int Miss { get; private set; }

        /// <summary>
        /// コンボ数
        /// </summary>
        public int Combo { get; private set; }

        /// <summary>
        /// 最大コンボ数
        /// </summary>
        public int BestCombo { get; private set; }

        /// <summary>
        /// 天井コンボ数
        /// </summary>
        public int MaxCombo { get; private set; }

        /// <summary>
        /// クリアゲージの値
        /// </summary>
        public int ClearGuage { get; private set; }

        /// <summary>
        /// 点数
        /// </summary>
        public int Score { get; private set; }

        /// <param name="controller"> コントローラー </param>
        public Player(Controller controller)
        {
            // プレイヤーとコントローラーの紐づけ
            Controller = controller;
        }
    }
}
