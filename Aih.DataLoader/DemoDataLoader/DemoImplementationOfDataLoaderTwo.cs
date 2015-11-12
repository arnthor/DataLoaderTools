using System;
using Aih.DataLoader.Tools;

namespace DemoDataLoader
{
    class DemoImplementationOfDataLoaderTwo : BaseDataLoader
    {


        public override void CleanUp()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderTwo Cleanup");
        }

        public override void Initialize()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderTwo Initialize");
        }

        public override void LoadData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderTwo LoadData");
        }

        public override void SaveData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderTwo SaveData");
        }

        public override void TransformData()
        {
            Console.WriteLine("DemoImplementationOfDataLoaderTwo TransformData");
        }
    }
}
