using App.Core;

namespace App.Interface
{
    public interface IEffect
    {
        public decimal UseEffect(decimal initialValue, DecisionMaker decisionMaker);
    }
}
