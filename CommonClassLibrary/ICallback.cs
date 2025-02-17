using System;

namespace CommonClassLibrary
{
    public interface ICallback<R>
    {
        void OnSuccess(ZResponse<R> response);

        void OnFailure(Exception exception);
    }
}