using MediaReviewClassLibrary.Models;
using System.Threading.Tasks;

namespace MediaReviewUWP.View.Contract
{
    public interface IMediaPage : IView
    {
        Task UpdateMediaPage(MediaDetailBObj mediaDetailBObj);
    }
}