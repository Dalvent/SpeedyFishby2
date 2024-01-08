using System.Collections;

namespace Code.Commands
{
    public interface ITimeCoroutineCommand
    {
        IEnumerator Start(float time);
    }
    
    public interface IResetbleCoroutineCommand : ITimeCoroutineCommand
    {
        ITimeCoroutineCommand CreateGoBackCoroutine();
    }
}