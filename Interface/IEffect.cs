using App.Core;

namespace App.Interface
{
    public interface IEffect<T>
    {
        public T UseEffect(T initialValue, DecisionMaker decisionMaker);
    }
}
