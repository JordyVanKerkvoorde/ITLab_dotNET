using System;

namespace ITLab29.Models.Domain
{
    public class Feedback
    {

        public int FeedbackId { get; set; }
        public int Score { get; set; }

        public User User { get; set; }
        public string Description { get; set; }

        public Feedback(int score, string description, User user)
        {
            Score = score;
            User = user;
            Description = description;
        }

        public Feedback()
        {
        }

    }
}
