using static System.Math;

namespace AeroGroovers.Model
{
    /// <summary>
    /// 九重 りむ のキャラクター情報
    /// </summary>
    public partial class Rimu : Character
    {
        /// <summary>
        /// キャラクター画像のパス
        /// </summary>
        public override string Image => "Resources/Graphics/Rimu.png";

        public Rimu(Player Player) : base(Player)
        {
            PointRate = new PointAllocation
            {
                 JustShoot = 0.7,
                     Shoot = 0.175,
                       Hit = 0.14,
                      Miss = 0,
                     Combo = 0.1,
                ClearPoint = 0.2,
            };

            // スキルの登録
            Skills = new Skill[]
            {
                new Amaterasu(this),
                new Tsukuyomi(this),
                new Susanoo(this)
            };
        }

        /// <summary>
        /// キャラクターを更新する
        /// </summary>
        /// <param name="difference"> タイミングとの誤差 </param>
        public override void Update(int difference)
        {
            Judge judge =
                Abs(difference) <= 42 ? Judge.JustShoot :
                Abs(difference) <= 92 ? Judge.Shoot :
                Abs(difference) <= 166 ? Judge.Hit : Judge.Miss;

            Skills[0].Update(judge);
            Skills[1].Update(judge);
            Skills[2].Update(judge);

            switch (judge)
            {
                case Judge.JustShoot: Player.Update(judge, 8000 * ClearPointAmplitude); break;
                case Judge.Shoot: Player.Update(judge, 1000 * ClearPointAmplitude); break;
                case Judge.Hit: Player.Update(judge, 500 * ClearPointAmplitude); break;
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
        }
    }
}
