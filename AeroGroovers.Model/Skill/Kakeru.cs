namespace AeroGroovers.Model
{
    public partial class Kakeru
    {
        /// <summary>
        /// ゲージ減少防止スキル : ミュート
        /// </summary>
        private class Mute : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Guard;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint > 0;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Mute(Character character)
                : base(character)
            {
                SkillPoint = 15;
                SkillPoint_Max = 15;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                if (SkillState)
                {
                    Trigger();
                    base.Update(judge);
                    return true;
                }
                else
                {
                    base.Update(judge);
                    return false;
                }
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                --SkillPoint;

                base.Trigger();
            }
        }

        /// <summary>
        /// クリア妨害スキル : エコー
        /// </summary>
        private class Echo : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Attack;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint >= SkillPoint_Max;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Echo(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 30;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                // Hit以上でポイントを加算
                switch (judge)
                {
                    case Judge.JustShoot:
                    case Judge.Shoot: ++SkillPoint; break;
                    case Judge.Hit:
                    case Judge.Miss: SkillPoint = 0; break;
                }

                // スキル発動条件を満たした時の処理
                if (SkillState)
                {
                    Trigger();
                    SkillPoint -= SkillPoint_Max;
                }

                return base.Update(judge);
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                foreach (var player in Game.Player)
                    if (player != null && player != Character.Player)
                        player.Character.Damage(15_0000);

                base.Trigger();
            }
        }

        /// <summary>
        /// ステータスダウンスキル : ハウル
        /// </summary>
        private class Howl : Skill
        {
            /// <summary>
            /// スキルの種類
            /// </summary>
            public override SkillType SkillType =>
                SkillType.Edge;

            /// <summary>
            /// スキルの発動要件
            /// </summary>
            protected override bool SkillState =>
                SkillPoint == 0;

            /// <param name="character"> 紐づけるキャラクター </param>
            public Howl(Character character)
                : base(character)
            {
                SkillPoint = 12;
                SkillPoint_Max = 12;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                if (judge == Judge.Miss) --SkillPoint;

                // スキル発動条件を満たした時の処理
                if (SkillState)
                {
                    Trigger();
                    base.Update(judge);
                    return true;
                }
                else
                {
                    base.Update(judge);
                    return false;
                }
            }

            /// <summary>
            /// スキルが発動した時の動作
            /// </summary>
            protected override void Trigger()
            {
                ((Kakeru)Character).IncreseRate /= 3;
                ((Kakeru)Character).PointRate = new PointAllocation
                {
                    JustShoot = 0.63,
                    Shoot = 0.504,
                    Hit = 0.378,
                    Miss = 0,
                    Combo = 0.045,
                    ClearPoint = 0.225
                };
                base.Trigger();
            }
        }
    }
}
