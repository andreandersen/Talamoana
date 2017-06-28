using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Talamoana.Domain.Core.Stash;

namespace Talamoana.PublichStashFetcher
{
    public class PublicStashMonitor : IObservable<Stash>
    {
        // Time between requests to GGG's API
        const int ThrottleWait = 500;

        public IObservable<Stash> Stashes;

        private StashFetcher _stashFetcher;
        private List<IObserver<Stash>> _observers;

        public PublicStashMonitor(string changeId = null)
        {
            _observers = new List<IObserver<Stash>>();
            _stashFetcher = new StashFetcher();

            if (changeId != null)
                _stashFetcher.NextChangeId = changeId;
        }

        public IDisposable Subscribe(IObserver<Stash> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public async Task Monitor(bool fetchLatestChangeId, CancellationToken cancellationToken)
        {
            if (fetchLatestChangeId)
            {
                await _stashFetcher.InitializeWithLastChangeId();
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                var throttleStopwatch = Stopwatch.StartNew();
                var stashes = await _stashFetcher.GetNextAsync();

                try
                {
                    stashes.Stashes.ForEach(stash => 
                        _observers.ForEach(observer => 
                            observer?.OnNext(stash)));
                }
                catch (Exception e)
                {
                    _observers.ForEach(o => o.OnError(e));
                }

                if (throttleStopwatch.ElapsedMilliseconds < ThrottleWait)
                    await Task.Delay((int)(ThrottleWait - throttleStopwatch.ElapsedMilliseconds));
            }

            _observers.ForEach(o => o?.OnCompleted());
            _observers.Clear();
        }


        private class Unsubscriber : IDisposable
        {
            private List<IObserver<Stash>> _observers;
            private IObserver<Stash> _observer;

            public Unsubscriber(List<IObserver<Stash>> observers, IObserver<Stash> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (!(_observer == null)) _observers.Remove(_observer);
            }
        }
    }
}
