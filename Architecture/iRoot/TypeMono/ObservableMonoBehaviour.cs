namespace AI.Architecture.iRoot
{
    public class ObservableMonoBehaviour : TypedMonoBehaviour
    {
        Subject<Unit> awake;
        public override void Awake()
        {
            awake.OnNext(Unit.Default);
        }

        public IObservable<Unit> AwakeAsObservable()
        {
            return awake ?? (awake = new Subject<Unit>());
        }
    }
}