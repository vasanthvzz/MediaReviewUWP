using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IMediaDetailControl
    {
        Task UpdateMediaRating(float mediaRating);
    }
}