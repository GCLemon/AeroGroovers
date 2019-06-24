namespace AeroGroovers.Model
{
    /// <summary>
    /// 譜面が持つ情報
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 曲名
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///サブタイトル
        ///</summary>
        public string Subtitle { get; private set; }

        /// <summary>
        /// 音源のファイルパス
        /// </summary>
        public string SoundPath { get; private set; }

        /// <summary>
        /// ジャケットのファイルパス
        /// </summary>
        public string JacketPath { get; private set; }

        /// <summary>
        /// 曲のテンポ [bpm]
        /// </summary>
        public float  Tempo { get; private set; }

        /// <summary>
        /// 曲が始まる前の空白部分 [ms]
        /// </summary>
        public int    Ofset { get; private set; }

        
    }
}
