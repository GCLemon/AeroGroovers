using static System.Math;

namespace AeroGroovers.Model
{
    /// <summary>
    /// 文字 翔 のキャラクター情報
    /// </summary>
    public partial class Kakeru : Character
    {
        /// <summary>
        /// キャラクター画像のパス
        /// </summary>
        public override string Image => "Graphics/Kakeru.png";

        /// <summary>
        /// ゲージが増加する割合
        /// </summary>
        private int IncreseRate;

        public Kakeru(Player Player) : base(Player)
        {
            PointRate = new PointAllocation
            {
                JustShoot = 0.7,
                Shoot = 0.56,
                Hit = 0.42,
                Miss = 0,
                Combo = 0.05,
                ClearPoint = 0.25
            };

            // スキルの登録
            Skills = new Skill[]
            {
                new Mute(this),
                new Echo(this),
                new Howl(this)
            };

            IncreseRate = 3000;
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
                Abs(difference) <= 142 ? Judge.Hit : Judge.Miss;
            Skills[1].Update(judge);
            Skills[2].Update(judge);

            switch (judge)
            {
                case Judge.JustShoot: Player.Update(judge, IncreseRate * 4 * ClearPointAmplitude); break;
                case Judge.Shoot: Player.Update(judge, IncreseRate * 3 * ClearPointAmplitude); break;
                case Judge.Hit: Player.Update(judge, IncreseRate * 2 * ClearPointAmplitude); break;
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
            if (!Skills[0].Update(null))
            {
                Player.ClearPoint -= damage;
                SetGaugeValue(Player.Score / 1_0000);
            }
        }
    }
}
