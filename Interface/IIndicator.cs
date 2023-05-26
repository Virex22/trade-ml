namespace App.Interface
{
    public interface IIndicator {}

    public interface IIndicator<T> : IIndicator
    {
        // params to calculate with ParameterVariation
        T Calculate(params object[] objects);
    }
}
