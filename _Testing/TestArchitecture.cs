using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AI.Architecture.iRoot.Disposable;
using UnityEngine;

public class TestArchitecture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CompositeDisposable coposite = new CompositeDisposable();
        coposite.GetEnumerator();
        
        Collection();
        
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
    
}
