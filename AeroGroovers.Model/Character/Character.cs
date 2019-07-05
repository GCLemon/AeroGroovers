namespace AeroGroovers.Model
{
    /// <summary>
    /// キャラクターが共通して持つ情報
    /// </summary>
    public abstract class Character
    {
        /// <summary>
        /// Viewのゲージの数値を設定するメソッド
        /// </summary>
        /// <param name="value"> ゲージの値を 0~100 で指定 </param>
        public delegate void GaugeMethod(int value);

        /// <summary>
        /// Viewのゲージの数値を設定するメソッドを設定するための変数
        /// </summary>
        public GaugeMethod GaugeMethodSetter
        {
            set
            {
                SetGaugeValue = value;
                value(Player.ClearPoint / 1_0000);
            }
        }

        /// <summary>
        /// Viewのゲージの数値を設定するメソッド
        /// </summary>
        protected GaugeMethod SetGaugeValue;

        /// <summary>
        /// View側にエフェクトを再生するメソッド
        /// </summary>
        public delegate void EffectMethod(Judge judge);

        /// <summary>
        /// View側にエフェクトを再生するメソッド
        /// </summary>
        public EffectMethod AddEffet { protected get; set; }

        /// <summary>
        /// <para> キャラクターごとに設定する得点比率 </para>
        /// <para> 各々の変数に設定できるのは定数のみ </para>
        /// <para> JustShoot + Combo + ClearPoint == 1 となるように設定すること. </para>
        /// </summary>
        internal protected struct PointAllocation
        {
            /// <summary>
            /// <para> JustShootの得点比率 </para>
            /// <para> 標準 = 0.8 </para>
            /// </summary>
            public double JustShoot;

            /// <summary>
            /// <para> Shootの得点比率 </para>
            /// <para> 標準 = 0.56 </para>
            /// </summary>
            public double Shoot;

            /// <summary>
            /// <para> Hitの得点比率 </para>
            /// <para> 標準 = 0.32 </para>
            /// </summary>
            public double Hit;

            /// <summary>
            /// <para> Missの得点比率 </para>
            /// <para> 標準 = 0 </para>
            /// </summary>
            public double Miss;

            /// <summary>
            /// <para> コンボ数の得点比率 </para>
            /// <para> 標準 = 0.1 </para>
            /// </summary>
            public double Combo;

            /// <summary>
            /// <para> クリアポイントの得点比率 </para>
            /// <para> 標準 = 0.1 </para>
            /// </summary>
            public double ClearPoint;
        }

        /// <summary>
        /// キャラクターごとに設定する得点比率
        /// </summary>
        internal protected PointAllocation PointRate { get; protected set; }

        /// <summary>
        /// このキャラクターが紐づいているプレイヤー
        /// </summary>
        public Player Player { get; protected set; }

        /// <summary>
        /// このキャラクターが持っているスキル
        /// </summary>
        public Skill[] Skills { get; protected set; }

        /// <summary>
        /// キャラクター画像のパス
        /// </summary>
        public abstract string Image { get; }

        /// <summary>
        /// クリアゲージの上昇率
        /// </summary>
        protected int ClearPointAmplitude
        {
            get
            {
                switch (Player.Difficulty)
                {
                    case Difficulty.Novice: return 4;
                    case Difficulty.Medium: return 2;
                    case Difficulty.Expert: return 1;
                }
                return 0;
            }
                
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Player"> 紐づけるプレイヤー </param>
        public Character(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// キャラクターを更新する
        /// </summary>
        /// <param name="difference"> タイミングとの誤差 </param>
        public abstract void Update(int difference);

        /// <summary>
        /// このキャラクターにダメージを与える
        /// </summary>
        /// <param name="damage"> クリアポイント減少量 </param>
        public abstract void Damage(int damage);
    }
}
