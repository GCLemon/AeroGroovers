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
        public override string Image => "Resources/Graphics/Rintaro.png";

        /// <summary>
        /// キャラクターを更新する
        /// </summary>
        /// <param name="difference"> タイミングとの誤差 </param>
        public override void Update(int difference)
        {

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
