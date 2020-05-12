using System;
using System.Collections;
using UnityEngine.Networking;

namespace AI.Architecture.iRoot
{
    public class ObservableWWW
    {
        static IEnumerator GetWWW(string url, Action<string> onSuccess, Action<string> onError)
        {
            UnityWebRequest http = UnityWebRequest.Get(url);
            
            yield return http.SendWebRequest();
            if (http.isDone == true)
            {
                onSuccess(http.downloadHandler.text);
            }

            if (http.error != null)
            {
                onError(http.error);
            }
        }

        public static IObservable<string> Get(string url)
        {
            return AnonymsousObservable.Create<string>(observer =>
            {
                void OnSuccess(string value)
                {
                    try
                    {
                        observer.OnNext(value);
                        observer.OnCompleted();
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                    }
                }

                void OnError(string error)
                {
                    observer.OnError(new Exception(error));
                }

                var e = GetWWW(url, OnSuccess, OnError);

                GameLoopDispatcher.StartCoroutine1(e);
                return Disposable.Empty;
            });
        }
    }
}