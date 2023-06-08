namespace GameForum1.DAL
{
    public class CommentAnswerManager
    {
        public static List<CommentAnswer> Answers { get; set; }
        public static async Task<List<CommentAnswer>> GetCommentAnswers()
        {
            if (Answers == null)
            {
                Answers = new List<CommentAnswer>();
            }


            return Answers;


            //List<CommentAnswer> answerList = new List<CommentAnswer>();


            ////TODO: API här 

            //return answerList;
        }

        public static async Task CreateCommentAnswer(CommentAnswer newAnswer)
        {
            Answers.Add(newAnswer);
        }
        public static async Task<CommentAnswer> GetOneCommentAnswer(int id)
        {
            return Answers[id];
        }

        public static async Task UpdateCommentAnswer(CommentAnswer existingAnswer)
        {
            if (Answers is null)
            {
                Answers = await GetCommentAnswers();
            }

            var answerToUpdate = Answers.Where(x => x.Id == existingAnswer.Id).FirstOrDefault();

            answerToUpdate.Content = existingAnswer.Content;
            answerToUpdate.Reported = existingAnswer.Reported;
            answerToUpdate.Score = existingAnswer.Score;

            Answers.RemoveAt(existingAnswer.Id);
            Answers.Add(answerToUpdate);
        }

        public static async Task DeleteCommentAnswer(int id)
        {
            if (Answers is null)
            {
                Answers = await GetCommentAnswers();
            }

            Answers.RemoveAt((int)id);
        }
    }
}
