using static System.Math;

namespace AeroGroovers.Model
{
    /// <summary>
    /// 三嶋 奏音 のキャラクター情報
    /// </summary>
    public partial class Kanon : Character
    {
        /// <summary>
        /// キャラクター画像のパス
        /// </summary>
        public override string Image => "Resources/Graphics/Kanon.png";

        public Kanon(Player Player) : base(Player)
        {
            // 得点比率の設定
            PointRate = new PointAllocation
            {
                JustShoot = 0.8,
                Shoot = 0.56,
                Hit = 0.32,
                Miss = 0,
                Combo = 0.1,
                ClearPoint = 0.1
            };

            // スキルの登録
            Skills = new Skill[]
            {
                new Amanomurakumo(this),
                new Yata(this),
                new Yasakani(this)
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
            Skills[2].Update(judge);

            int point = 0;

            switch (judge)
            {
                case Judge.JustShoot: point = 1_0000; break;
                case Judge.Shoot: point = 5000; break;
                case Judge.Hit: point = 2500; break;
                case Judge.Miss:
                    point = Skills[1].Update(judge) ? 0 : -7_0000;
                    break;
            }

            Player.Update(judge, point * ClearPointAmplitude);

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
