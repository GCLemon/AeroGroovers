namespace AeroGroovers.Model
{
    public partial class Kanon
    {
        /// <summary>
        /// クリア妨害スキル : 天叢雲剣
        /// </summary>
        private class Amanomurakumo : Skill
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
            public Amanomurakumo(Character character)
                : base(character)
            {
                SkillPoint = 0;
                SkillPoint_Max = 20;
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
                    case Judge.Shoot:
                    case Judge.Hit: ++SkillPoint; break;
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
                        player.Character.Damage(5_0000);

                base.Trigger();
            }
        }

        /// <summary>
        /// ゲージ減少防止スキル : 八咫鏡
        /// </summary>
        private class Yata : Skill
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
            public Yata(Character character)
                : base(character)
            {
                SkillPoint = 10;
                SkillPoint_Max = 10;
            }

            /// <summary>
            /// スキルを更新する
            /// </summary>
            /// <param name="judge"> 判定 </param>
            public override bool Update(Judge? judge)
            {
                if (judge == Judge.Miss && SkillState)
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
        /// クリア妨害スキル : 八尺瓊勾玉
        /// </summary>
        private class Yasakani : Skill
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
            public Yasakani(Character character)
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
                        player.Character.Damage(10_0000);

                base.Trigger();
            }
        }
    }
}
