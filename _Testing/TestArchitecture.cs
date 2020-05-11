using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AI.Architecture.iRoot;
using UniRx;
using UnityEngine;
using Observable = AI.Architecture.iRoot.Observable;
using CompositeDisposable = AI.Architecture.iRoot.CompositeDisposable;
using ObservableWWW = AI.Architecture.iRoot.ObservableWWW;
using Random = UnityEngine.Random;

public class TestArchitecture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CompositeDisposable coposite = new CompositeDisposable();
        coposite.GetEnumerator();

        Collection();

        // for (int i = 0; i < 1000; i++)
        // {
        //     var rate = Inder(ProbabilityValue);
        //     if (rate == 6)
        //     {
        //         Debug.LogFormat("rate:" + rate);
        //     }
        // }
        
        Observable1();
    }

    void Observable1()
    {
        var one = Observable.Range(3, 3);
        one.Subscribe(Method());

        AI.Architecture.iRoot.IObserver<int> Method()
        {
            return AnonymsousObserver.Create<int>(v => Debug.LogFormat($"v:{v}"), err => { }, () => { });
        }
        
        Observable.Range(1, 5).Select(v => v > 3).Subscribe(v => Debug.LogFormat($"v:{v} v > 3 value:{v > 3}"));
        
        // List<byte> ls = new List<byte>();
        // ls.Add(3);
        // ls.Add(5);
        // ls.Add(7);
        // ls.Add(9);
        // ls.Add(11);
        // Predicate<byte> prd = b => b > 10;
        // var result = prd(20);
    }

    void Subject()
    {
        Subject<byte> subject = new Subject<byte>();
        
        AsyncSubject<byte> async = new AsyncSubject<byte>();
        
        BehaviorSubject<byte> behaviorSubject = new BehaviorSubject<byte>(1);
    }

    void Collection()
    {
        // HashSet<byte> hashSet = new HashSet<byte>() { 1, 1, 3, 3, 4, 5, 2, 2};
        // hashSet.Add(1);
        // hashSet.Add(6);
        // hashSet.ToList().ForEach(b => Debug.LogFormat("hashset:" + b));
        //
        // SortedSet<byte> sortedSet = new SortedSet<byte>() { 1, 1, 3, 3, 4, 5, 2, 2};
        // sortedSet.Add(1);
        // sortedSet.Add(6);
        // sortedSet.ToList().ForEach(b => Debug.LogFormat("sortedset:" + b));

        // Collection<byte> collection = new Collection<byte>() {1, 1, 3, 3, 4, 5, 2, 2};
        // collection.Add(1);
        // collection.Add(6);
        // collection.ToList().ForEach(b => Debug.LogFormat("collection:" + b));

        // SortedList<byte, byte> sortedList = new SortedList<byte, byte>(); // sorted by key
        // sortedList.Add(1, 1);
        // sortedList.Add(3, 3);
        // sortedList.Add(5, 1);
        // sortedList.Add(2, 2);
        // sortedList.Add(4, 4);
        // sortedList.Add(6, 2);
        // sortedList.ToList().ForEach(kv => Debug.LogFormat("k:" + kv.Key + " v:" + kv.Value));

        // SortedDictionary<byte, byte> sortedDictionary = new SortedDictionary<byte, byte>();
        // sortedDictionary.Add(1, 1);
        // sortedDictionary.Add(3, 3);
        // sortedDictionary.Add(5, 1);
        // sortedDictionary.Add(2, 2);
        // sortedDictionary.Add(4, 4);
        // sortedDictionary.Add(6, 2);
        // sortedDictionary.ToList().ForEach(kv => Debug.LogFormat("k:" + kv.Key + " v:" + kv.Value));

        // LinkedList<byte> linkedList = new LinkedList<byte>();
        // linkedList.AddLast(1);
        // linkedList.AddLast(1);
        // linkedList.AddLast(3);
        // linkedList.AddLast(4);
        // linkedList.AddLast(5);
        // linkedList.ToList().ForEach(b => Debug.LogFormat("linkedlist:" + b));
    }

    // Update is called once per frame
    private string fileName = "temp_xlsx.xlsx";

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // var path = Application.dataPath;
            // path = Path.Combine(path, fileName);
            //
            //
            // Debug.LogFormat("path:" + path);


            var http = ObservableWWW.Get("http://www.baidu.com");

            void OnNext(string value) => Debug.Log($"Success:{value}");
            void OnError(Exception error) => Debug.LogException(error);
            void OnComplete() => Debug.Log("Complete");

            http.Subscribe(OnNext, OnError, OnComplete);
        }
    }

    // HashSet<byte> set = new HashSet<byte>();
    // SortedSet<byte> sort = new SortedSet<byte>(new );


    float[] ProbabilityValue = new float[7] {0.05f, 0.1f, 0.1f, 0.2f, 0.25f, 0.3f, 1f};
    //string[] Probability = new string[7] {0.05f, 0.1f, 0.1f, 0.2f, 0.25f, 0.3f, 1f};

    /// <summary>
    /// 经典转盘概率算法，
    /// 准确度 ★★★★★
    /// 难度  ★
    /// </summary>
    /// <param name="ProbabilityValue"></param>
    /// <returns></returns>
    private int Inder(float[] ProbabilityValue)
    {
        float total = 0;
        //首先计算出概率的总值，用来计算随机范围
        for (int i = 0; i < ProbabilityValue.Length; i++)
        {
            total += ProbabilityValue[i];
        }

        float Nob = Random.Range(0, total);
        for (int i = 0; i < ProbabilityValue.Length; i++)
        {
            if (Nob <= ProbabilityValue[i])
            {
                return i;
            }
            else
            {
                Nob -= ProbabilityValue[i];
            }
        }

        return ProbabilityValue.Length - 1;
    }
}