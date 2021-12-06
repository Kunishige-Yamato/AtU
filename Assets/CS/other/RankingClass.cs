namespace scoreInfo
{
    public class PersonalScore
    {
        public string achieve, name;
        public int s_easy, s_normal, s_hard, s_crazy;
        public int e_normal, e_gambling;

        public PersonalScore(string achieve, string name, int s_easy, int s_normal, int s_hard, int s_crazy, int e_normal, int e_gambling)
        {
            this.achieve = achieve;
            this.name = name;
            this.s_easy = s_easy;
            this.s_normal = s_normal;
            this.s_hard = s_hard;
            this.s_crazy = s_crazy;
            this.e_normal = e_normal;
            this.e_gambling = e_gambling;
        }
    }

    public class RankingScore
    {
        public string achieve, name;
        public int score;

        public RankingScore(string achieve, string name, int score)
        {
            this.achieve = achieve;
            this.name = name;
            this.score = score;
        }
    }
}
