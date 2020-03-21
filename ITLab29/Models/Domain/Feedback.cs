using System;

namespace ITLab29.Models.Domain
{
    public class Feedback
    {
        private int _score;

        public int FeedbackId { get; set; }
        public int Score {
            get { return _score; }
            set {
                if (value > 5 || value < 0)
                    throw new ArgumentException("Score has to be between 0 and 5");
                _score = value;
            }
        }

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
