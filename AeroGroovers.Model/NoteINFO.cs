namespace AeroGroovers.Model
{
    /// <summary>
    /// ノーツが持つ情報 ノーツオブジェクトに埋め込んで使う
    /// </summary>
    public struct NoteInfo
    {
        /// <summary>
        /// ノートの番号(左から1,2,3,4,5)
        /// </summary>
        public int Number { get; internal set; }

        /// <summary>
        /// ボタンを押すタイミングをms単位で設定
        /// </summary>
        public long Timing { get; internal set; }
    }
}
