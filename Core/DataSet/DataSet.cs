namespace App.Core.DataSet
{
    public class DataSet : AbstractDataSet
    {
        public override DateTimeOffset GetCurrentTime() => DateTimeOffset.Now;

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }
    }
}
