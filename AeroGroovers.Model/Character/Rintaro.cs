using static System.Math;

namespace AeroGroovers.Model
{
    /// <summary>
    /// 高杉 凛太朗 のキャラクター情報
    /// </summary>
    public partial class Rintaro : Character
    {
        /// <summary>
        /// キャラクター画像のパス
        /// </summary>
        public override string Image => "Graphics/Rintaro.png";

        /// <summary>
        /// <para> ゲージに加えるポイント </para>
        /// <para> 最大コンボ数ごとに上昇する </para>
        /// </summary>
        internal int GaugeAdd;

        public Rintaro(Player Player) : base(Player)
        {
            // 得点比率の設定
            PointRate = new PointAllocation
            {
                 JustShoot = 0.5,
                     Shoot = 0.4,
                       Hit = 0.15,
                      Miss = 0,
                     Combo = 0.4,
                ClearPoint = 0.1
            };

            // スキルの登録
            Skills = new Skill[]
            {
                new Eclipse(this),
                new Prominence(this),
                new Flare(this)
            };

            GaugeAdd = 5000;
        }

        /// <summary>
        /// キャラクターを更新する
        /// </summary>
        /// <param name="difference"> タイミングとの誤差 </param>
        public override void Update(int difference)
        {
            Judge judge =
                Abs(difference) <= 32 ? Judge.JustShoot :
                Abs(difference) <= 87 ? Judge.Shoot :
                Abs(difference) <= 166 ? Judge.Hit : Judge.Miss;

            if (Skills[0].Update(judge)) judge = Judge.Hit;

            Skills[1].Update(judge);
            Skills[2].Update(judge);

            switch (judge)
            {
                case Judge.JustShoot: Player.Update(judge, GaugeAdd * ClearPointAmplitude); break;
                case Judge.Shoot: Player.Update(judge, GaugeAdd / 2 * ClearPointAmplitude); break;
                case Judge.Hit: Player.Update(judge, GaugeAdd / 4 * ClearPointAmplitude); break;
                case Judge.Miss: Player.Update(judge, -10_0000); break;
            }

            AddEffet(judge);

            SetGaugeValue(Player.ClearPoint / 1_0000);
        }

        /// <summary>
        /// このキャラクターにダメージを与える
        /// </summary>
        /// <param name="damage"> クリアポイント減少量 </param>
        public override void Damage(int damage)
        {
            Player.ClearPoint -= damage;
            SetGaugeValue(Player.Score / 1_0000);
        }
    }
}
