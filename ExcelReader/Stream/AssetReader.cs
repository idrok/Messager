using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace GameInnvoation.ExcelReader
{
    public class AssetReader : IReadable
    {
        public void Reader<T>(IConfigable ini, Action<T> callback)
        {
            AddressableResLoadTask<T>(ini, asset =>
            {
                if (callback != null) callback(asset);
            });

        }

        async void AddressableResLoadTask<T>(IConfigable ini, Action<T> callback)
        {
            // todo 
            // name reader from ini
            var handle = Addressables.LoadAssetAsync<T>("innvoation");
            await handle.Task;
            if (handle.IsDone == true)
            {
                var asset = handle.Result;
                if (callback != null) callback(asset);
            }
        }
    }
}