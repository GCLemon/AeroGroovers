namespace AeroGroovers.Model
{
    /// <summary>
    /// ボタンの種類
    /// </summary>
    public enum Button
    {
        /// <summary> Aボタン </summary>
        A,

        /// <summary> Bボタン </summary>
        B,

        /// <summary> Xボタン </summary>
        X,

        /// <summary> Yボタン </summary>
        Y,

        /// <summary> Lボタン </summary>
        L,

        /// <summary> Rボタン </summary>
        R,

        /// <summary> 上ボタン </summary>
        Up,

        /// <summary> 下ボタン </summary>
        Down,

        /// <summary> 左ボタン </summary>
        Left,

        /// <summary> 右ボタン </summary>
        Right,

        /// <summary> スタートボタン </summary>
        Start,

        /// <summary> セレクトボタン </summary>
        Select
    }

    /// <summary>
    /// コントローラーの種類
    /// </summary>
    public enum ControllerType
    {
        /// <summary> キーボード </summary>
        Keyboard,

        /// <summary> PlayStation4 </summary>
        PlayStation4,

        /// <summary> Xbox360 </summary>
        Xbox360,

        /// <summary> その他のコントローラー </summary>
        Other
    }

    /// <summary>
    /// 難易度
    /// </summary>
    public enum Difficulty
    {
        /// <summary> 難易度 : 簡単 </summary>
        Novice,

        /// <summary> 難易度 : 普通 </summary>
        Medium,

        /// <summary> 難易度 : 難しい </summary>
        Expert
    }

    /// <summary>
    /// 判定
    /// </summary>
    public enum Judge
    {
        /// <summary> 誤差が42ms以下の場合はJustShoot </summary>
        JustShoot,

        /// <summary> 誤差が92ms以下の場合はShoot </summary>
        Shoot,

        /// <summary> 誤差が166ms以下の場合はHit </summary>
        Hit,

        /// <summary> 誤差が+166msを超えた場合はMiss </summary>
        Miss
    }

    /// <summary>
    /// ランク
    /// </summary>
    public enum Rank
    {
        F, E, D, C, B, A, S, SS, SSS
    }

    /// <summary>
    /// クリア判定
    /// </summary>
    public enum ClearJudge
    {
        /// <summary> ノルマクリア失敗 </summary>
        Failure,

        /// <summary> ノルマクリア成功 </summary>
        Success,

        /// <summary> Missが0回でフルコンボ </summary>
        FullCombo,

        /// <summary> Hit以下が0回でオールシュート </summary>
        AllShoot,

        /// <summary> Shoot以下が0回でアルティメットシュート </summary>
        UltimateShoot,
    }

    /// <summary>
    /// スキルの種類
    /// </summary>
    public enum SkillType
    {
        /// <summary> クリア妨害スキル </summary>
        Attack,

        /// <summary> ゲージ減少防止スキル </summary>
        Guard,

        /// <summary> ゲージ回復スキル </summary>
        Heal,

        /// <summary> ステータスアップスキル </summary>
        Boost,

        /// <summary> 判定易化スキル </summary>
        Support,

        /// <summary> ステータスダウンスキル </summary>
        Edge,

        /// <summary> 自滅スキル </summary>
        Damage
    }
}
