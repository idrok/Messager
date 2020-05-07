﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AI.Architecture.iRoot;
using UnityEngine;

public class TestArchitecture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CompositeDisposable coposite = new CompositeDisposable();
        coposite.GetEnumerator();

        Collection();

        for (int i = 0; i < 1000; i++)
        {
            var rate = Inder(ProbabilityValue);
            if (rate == 6)
            {
                Debug.LogFormat("rate:" + rate);
            }
        }
        
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
    void Update()
    {
    }

    // HashSet<byte> set = new HashSet<byte>();
    // SortedSet<byte> sort = new SortedSet<byte>(new );


    float[] ProbabilityValue = new float[7] {0.05f, 0.1f, 0.1f, 0.2f, 0.25f, 0.3f, 1f};
    //string[] Probability = new string[7] {0.05f, 0.1f, 0.1f, 0.2f, 0.25f, 0.3f, 1f};

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