using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.EntryPoint
{
    public interface IInitializeble
    {
        public UniTask Initialize(CancellationToken cancellationToken);
    }
}