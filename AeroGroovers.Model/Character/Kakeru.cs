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
        public override string Image => "Resources/Graphics/Kakeru.png";

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
