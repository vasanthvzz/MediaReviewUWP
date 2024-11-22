using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public abstract class UseCaseBase<R>
    {

        public ICallback<R> PresenterCallback { get; set; }

        public abstract void Action();

        public void Execute()
        {
            Task.Run(() =>
            {
                Action();
            });
        }

        public UseCaseBase(ICallback<R> callback)
        {
            PresenterCallback = callback;
        }
    }
}
