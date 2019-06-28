using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
        /// レベル
        /// </summary>
        public Dictionary<Difficulty, int> Level { get; private set; }

        /// <summary>
        /// 曲のテンポ [bpm]
        /// </summary>
        public double Tempo { get; private set; }

        /// <summary>
        /// 曲が始まる前の空白部分 [ms]
        /// </summary>
        public int    Ofset { get; private set; }

        /// <summary>
        /// ノーツの情報格納する.
        /// </summary>
        public Dictionary<Difficulty, List<NoteParameters>> Notes { get; internal set; }

        /// <summary>
        /// tomlファイルを読み込むストリーム
        /// </summary>
        private StreamReader reader;

        /// <param name="toml_path"> .tomlファイルのパス </param>
        public Score(string toml_path)
        {
            Level = new Dictionary<Difficulty, int>();
            Notes = new Dictionary<Difficulty, List<NoteParameters>>();

            // ローカルメソッドを定義
            void SetInfo(string line, string key, string regex_s, string regex_e)
            {
                // 文字列の除去
                line = Regex.Replace(line, key + regex_s, "");
                line = Regex.Replace(line, regex_e, "");

                // サブタイトルの設定
                switch (key)
                {
                    case "title": Title = line; break;
                    case "subtitle": Subtitle = line; break;
                    case "sound": SoundPath = line; break;
                    case "jacket": JacketPath = line; break;
                    case "level":
                        var lines = line.Replace("\"", "").Replace(" ", "").Split(',');
                        Level[Difficulty.Novice] = int.Parse(lines[0]);
                        Level[Difficulty.Medium] = int.Parse(lines[1]);
                        Level[Difficulty.Expert] = int.Parse(lines[2]);
                        break;
                    case "tempo": Tempo = double.Parse(line); break;
                    case "ofset": Ofset = int.Parse(line); break;

                }
            }

            // 譜面を作成する
            void MakeScore(Difficulty difficulty, string initial_line)
            {
                // 文字列型の譜面
                string line = Regex.Replace(initial_line, ".*\"\"\"", "");
                string score_line = "";
                List<string> score_str = new List<string>();

                void SetScoreStr(char c)
                {
                    // 文字がカンマであればscore_strに追加し,score_lineを初期化
                    if (c == ',')
                    {
                        score_str.Add(score_line);
                        score_line = "";
                    }

                    // 数字の0から5,または中括弧であれば,score_lineに文字を追加
                    else if (c == 40 || c == 41 || 48 <= c && c <= 53)
                    {
                        score_line += c;
                    }
                }

                // まず,ファイルを読み込んで文字列型の譜面を取得する
                while (true)
                {
                    // 譜面の終端に達した場合
                    if(Regex.IsMatch(line, ".*\"\"\".*"))
                    {
                        // クォーテーションを除去
                        line = Regex.Replace(initial_line, "\"\"\".*", "");

                        // 譜面の設定をして
                        for (int i = 0; i < line.Length; ++i) SetScoreStr(line[i]);

                        // break
                        break;
                    }

                    // 譜面の設定
                    for (int i = 0; i < line.Length; ++i) SetScoreStr(line[i]);

                    // 最後にlineを更新
                    line = reader.ReadLine();
                }

                int beat = 0;
                int measure = 4;
                double tempo = Tempo;
                int ofset = Ofset;

                List<NoteParameters> score = new List<NoteParameters>();

                // 1小節のノーツの数を求める
                int Length(string l)
                {
                    int length = 0;
                    int scope = 0;

                    foreach (char c in l)
                    {
                        if (c == '(') ++scope;
                        if (c == ')') --scope;

                        if (scope == 0) ++length;
                    }

                    return length;
                }

                // 次にscore_strの情報を譜面に変更する
                foreach (string l in score_str)
                {
                    double length = Length(l);
                    double timing = 0;
                    int scope = 0;

                    foreach(char c in l)
                    {
                             if (c == 40) ++scope;
                        else if (c == 41) --scope;
                        else
                        {
                            // ノーツの情報を追加
                            NoteParameters note = new NoteParameters();
                            note.Number = c - 48;
                            note.Timing = (long)(60000 / tempo               // 1ビートあたりの時間(ms/b)
                                        * (timing / length * measure + beat) // ビート数
                                        + ofset);

                            // ノーツのインデックスが0でないときは加える
                            if (note.Number != 0) score.Add(note);
                        }
                    }

                    beat += measure;
                }

                Notes.Add(difficulty, score);
            }

            // インスタンスを格納
            reader = new StreamReader(toml_path);

            // 読み取った行それぞれについて処理
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();

                // 正規表現
                string str = "\\s*=\\s*\".*\"\\s*";
                string str_s = "\\s*=\\s*\"";
                string str_e = "\"\\s*";
                string dec = "\\s*=\\s*\\d+\\s*";
                string dec_s = "\\s*=\\s*";
                string dec_e = "\\s*";
                string ary = "\\s*=\\s*\\[(\\s*\\d+\\s*,?\\s*)*\\]\\s*";
                string ary_s = "\\s*=\\s*\\[";
                string ary_e = "\\]\\s*";
                string mullstr = "\\s*=\\s*\"\"\".*(\"\"\")?\\s*";

                // サブタイトルの設定
                if (Regex.IsMatch(line, "subtitle" + str))
                    SetInfo(line, "subtitle", str_s, str_e);

                // タイトルの設定
                else if (Regex.IsMatch(line, "title" + str))
                    SetInfo(line, "title", str_s, str_e);

                // 音源ファイルのパスの設定
                else if (Regex.IsMatch(line, "sound" + str))
                    SetInfo(line, "sound", str_s, str_e);

                // ジャケット画像ファイルのパスの設定
                else if (Regex.IsMatch(line, "jacket" + str))
                    SetInfo(line, "jacket", str_s, str_e);

                // レベルの設定
                else if (Regex.IsMatch(line, "level" + ary))
                    SetInfo(line, "level", ary_s, ary_e);

                // 初期のテンポの設定
                else if (Regex.IsMatch(line, "tempo" + dec))
                    SetInfo(line, "tempo", dec_s, dec_e);

                // オフセットの設定
                else if (Regex.IsMatch(line, "ofset" + dec))
                    SetInfo(line, "ofset", dec_s, dec_e);

                // 「Novice」譜面の設定
                else if (Regex.IsMatch(line, "novice" + mullstr))
                    MakeScore(Difficulty.Novice, line);

                // 「Medium」譜面の設定
                else if (Regex.IsMatch(line, "medium" + mullstr))
                    MakeScore(Difficulty.Medium, line);

                // 「Expert」譜面の設定
                else if (Regex.IsMatch(line, "expert" + mullstr))
                    MakeScore(Difficulty.Expert, line);
            }
        }
    }
}
