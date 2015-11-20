using System;
using Aih.DataLoader.Tools;

namespace DemoDataLoader
{
    public class DemoImplementationOfDataLoaderOne : BaseDataLoader
    {


        public override void CleanUp()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderOne Cleanup");
        }

        public override string Initialize()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderOne Initialize");
            return Guid.NewGuid().ToString();
        }

        public override void LoadData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderOne LoadData");
        }

        public override void SaveData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderOne SaveData");
        }

        public override void TransformData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderOne TransformData");
        }

    }
}
