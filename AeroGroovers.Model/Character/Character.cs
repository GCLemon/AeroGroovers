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
                value(Player.ClearPoint / 1000);
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
        protected int ClearPointAmplitude;

        /// <summary>
        /// キャラクターを初期化する
        /// </summary>
        /// <param name="Player"> 紐づけるプレイヤー </param>
        public virtual void Initialize(Player Player)
        {
            this.Player = Player;

            switch(Player.Difficulty)
            {
                case Difficulty.Novice: ClearPointAmplitude = 4; break;
                case Difficulty.Medium: ClearPointAmplitude = 2; break;
                case Difficulty.Expert: ClearPointAmplitude = 1; break;
            }
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
