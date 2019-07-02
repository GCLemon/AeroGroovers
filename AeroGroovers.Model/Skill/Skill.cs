namespace AeroGroovers.Model
{
    /// <summary>
    /// スキルが共通して持つ情報
    /// </summary>
    public abstract class Skill
    {
        /// <summary>
        /// プレイヤーを取得するためのゲームのモデル
        /// </summary>
        public static Game Game { protected get; set; }

        /// <summary>
        /// View側にエフェクトを追加するメソッド
        /// </summary>
        public delegate void EffectMethod();

        /// <summary>
        /// Viewのゲージの数値を設定するメソッド
        /// </summary>
        /// <param name="value"> ゲージの値を 0~100 で指定 </param>
        public delegate void GaugeMethod(int value);

        /// <summary>
        /// View側にエフェクトを追加するメソッド
        /// </summary>
        public EffectMethod AddEffect { protected get; set; }

        /// <summary>
        /// Viewのゲージの数値を設定するメソッドを設定するための変数
        /// </summary>
        public GaugeMethod GaugeMethodSetter
        {
            set
            {
                SetGaugeValue = value;
                value(SkillPoint * 100 / SkillPoint_Max);
            }
        }

        /// <summary>
        /// Viewのゲージの数値を設定するメソッド
        /// </summary>
        protected GaugeMethod SetGaugeValue;

        /// <summary>
        /// スキルポイント
        /// </summary>
        protected int SkillPoint;

        /// <summary>
        /// スキルポイントの最大値,
        /// SkillPoint == SkillPoint_Max のとき,ゲージの値は100
        /// </summary>
        protected int SkillPoint_Max;

        /// <summary>
        /// このスキルを保有するキャラクター
        /// </summary>
        protected Character Character;

        /// <summary>
        /// スキルの種類
        /// </summary>
        public abstract SkillType SkillType { get; }

        /// <summary>
        /// スキルの発動要件
        /// </summary>
        protected abstract bool SkillState { get; }

        public Skill(Character character) => Character = character;

        /// <summary>
        /// スキルを更新する
        /// </summary>
        /// <param name="judge"> 判定 </param>
        public virtual bool Update(Judge? judge)
        {
            SetGaugeValue(SkillPoint * 100 / SkillPoint_Max);
            return true;
        }

        /// <summary>
        /// スキルが発動した時の動作
        /// </summary>
        protected virtual void Trigger() => AddEffect();
    }
}
