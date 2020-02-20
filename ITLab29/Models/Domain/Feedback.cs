using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Models.Domain
{
    public class Feedback
    {
        private int _score;

        public int FeedbackId { get; set; }
        public int Score
        {
            get { return _score; }
            set { 
                if(value > 5 || value < 0)
                    throw new ArgumentException("Score has to be between 0 and 5");
                _score = value;
            }
        }

        public User User { get; set; }
        public string Description { get; set; }

        public Feedback(int feedbackId, int score, User user, string description)
        {
            FeedbackId = feedbackId;
            Score = score;
            User = user;
            Description = description;
        }


    }
}
