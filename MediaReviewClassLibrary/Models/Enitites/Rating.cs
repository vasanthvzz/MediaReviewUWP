using SQLite;
using System;

namespace MediaReviewClassLibrary.Models.Enitites
{
    [Table("rating")]
    public class Rating
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("media_id")]
        public long MediaId { get; set; }

        
        private short _score;
        [Column("score")]
        public short Score
        {
            get { return _score; }  
            set
            {
                if (value <= 5 && value >= -1)  
                {
                    _score = value;
                }
                else
                {
                    throw new Exception("Invalid rating score");
                }
            }
        }


        public Rating(long userId, long mediaId)
        {
            UserId = userId;
            MediaId = mediaId;
            Score = -1;
        }

        public Rating(long userId, long mediaId, short score)
        {
            UserId = userId;
            MediaId = mediaId;
            Score = score;
        }

        public Rating() { }

        public override bool Equals(object obj)
        {
            if (obj is Rating rating)
            {
                return this.UserId == rating.UserId && this.MediaId == rating.MediaId && Score == rating.Score;
            }
            return false;
        }

    }
}
