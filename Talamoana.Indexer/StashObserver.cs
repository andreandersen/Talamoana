using System;
using System.Linq;
using System.Threading.Tasks;
using Talamoana.Domain.Core.Stash;

namespace Talamoana.Indexer
{
    internal class StashObserver : IObserver<Stash>
    {
        private IDisposable _unsubsciber;

        private void OnNextImpl(Stash stash)
        {
            var lookFor = new[] { "Exalted Orb", "Mirror of Kalandra", "Headhunter", "Skyforth", "Taste of Hate", "Fated Connections",
                    "House of Mirrors", "The Doctor", "The Fiend", "Mawr Blaidd",  "The Perandus Manor", "Sin's Rebirth", "Dying Sun",
                    "United in Dream", "Atziri's Disfavour", "Atziri's Acuity", "Tukahoma's Fortress", "Eyes of the Greatwolf"};

            var matches =
                from item in stash.Items
                where lookFor.Contains(item.Name) ||
                      lookFor.Contains(item.TypeLine)
                select item;

            foreach (var match in matches.ToList())
            {
                string price = null;

                if (!string.IsNullOrWhiteSpace(match.Note) && match.Note.Contains('~'))
                    price = match.Note;
                else if (!string.IsNullOrWhiteSpace(stash.StashName) && stash.StashName.Contains('~'))
                    price = stash.StashName;

                if (price != null)
                {
                    lock (Console.Out)
                    {
                        PrintItemToConsole(match, price, stash.LastCharacterName, stash.AccountName);
                        Console.Beep(3300, 30);
                    }
                }
            }
        }

        public virtual void OnNext(Stash stash)
        {
            Task.Run(() => OnNextImpl(stash));
        }

        const string ind = "   >  ";
        private void PrintItemToConsole(StashItem item, string price, string lastCharacter, string accountName)
        {
            var previousColor = Console.ForegroundColor;

            if (Console.CursorLeft != 0)
            {
                Console.WriteLine();
                Console.CursorLeft = 0;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{(item.Name + " " + item.TypeLine).Trim()}");
            Console.ForegroundColor = previousColor;
            Console.Write($" in {item.League}{Environment.NewLine}");

            Console.WriteLine($"{ind}{price}");

            if (item.ImplicitMods != null)
                item.ImplicitMods.ForEach(Indented);

            if (item.ExplicitMods != null)
                item.ExplicitMods.ForEach(Indented);

            if (item.Sockets != null && item.Sockets.Count > 0)
                Indented($"{item.Sockets?.Count} sockets");

            Indented($"Seller: {lastCharacter} ({accountName})");

            Console.WriteLine();

            void Indented(string str)
            {
                str.Replace("\r", "").Split('\n').ToList().ForEach(line =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(ind);
                    Console.Write($"{line}{Environment.NewLine}");
                    Console.ForegroundColor = previousColor;
                });
            }
        }

        public virtual void Subscribe(IObservable<Stash> provider) =>
            _unsubsciber = provider.Subscribe(this);

        public virtual void Unsubscibe() =>
            _unsubsciber.Dispose();

        public virtual void OnCompleted() =>
            Console.WriteLine("Completed reporting on stash changes");

        public virtual void OnError(Exception ex)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"!! Error: {ex.Message}");
            Console.ForegroundColor = previousColor;
        }
    }
}
