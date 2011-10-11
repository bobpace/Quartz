using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Host
{
    public class FindFileByTodaysDateAndMatchingEndString : IFileFinder
    {
        readonly string _matchAtEnd;
        readonly Regex _regex;

        public FindFileByTodaysDateAndMatchingEndString(string matchAtEnd)
        {
            _matchAtEnd = matchAtEnd;
            var today = DateTime.UtcNow.Date.ToString("yyyyMMdd");
            var pattern = string.Format(@".*-{0}-.*{1}\.csv", today, _matchAtEnd);
            _regex = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public string GetFile(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath, "*.csv", SearchOption.AllDirectories)
                .Where(x => _regex.IsMatch(x))
                .FirstOrDefault();
        }
    }
}