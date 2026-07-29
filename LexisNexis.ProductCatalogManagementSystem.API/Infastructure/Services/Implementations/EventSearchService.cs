using Doctorly.EventManager.Api.Infastructure.Services.Interfaces;

namespace Doctorly.EventManager.Api.Infastructure.Services.Implementations
{
    public class EventSearchService : IEventSearchService
    {
        // Damerau–Levenshtein distance implementation
        public int GetDamerauLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 2, m + 2];

            int maxDist = n + m;
            d[0, 0] = maxDist;

            for (int i = 0; i <= n; i++)
            {
                d[i + 1, 1] = i;
                d[i + 1, 0] = maxDist;
            }

            for (int j = 0; j <= m; j++)
            {
                d[1, j + 1] = j;
                d[0, j + 1] = maxDist;
            }

            var da = new Dictionary<char, int>();
            foreach (char c in (s + t))
            {
                if (!da.ContainsKey(c))
                    da[c] = 0;
            }

            for (int i = 1; i <= n; i++)
            {
                int db = 0;
                for (int j = 1; j <= m; j++)
                {
                    int i1 = da[t[j - 1]];
                    int j1 = db;

                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    if (cost == 0) db = j;

                    d[i + 1, j + 1] = Math.Min(
                        Math.Min(d[i, j] + cost, Math.Min(d[i + 1, j] + 1, d[i, j + 1] + 1)),
                        d[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1)
                    );
                }
                da[s[i - 1]] = i;
            }

            return d[n + 1, m + 1];
        }

        public List<string> SearchOption(List<string> words, string query)
        {
            int maxDistance = 2; // still using 2, but now transpositions count as 1

            return words
                    .SelectMany(w => w.Split(' ')) // break "Coffee Maker" into ["Coffee","Maker"]
                    .Distinct()
                    .Where(w => GetDamerauLevenshteinDistance(w.ToLowerInvariant(), query.ToLowerInvariant()) <= maxDistance)
                    .ToList();
        }
    }
}
