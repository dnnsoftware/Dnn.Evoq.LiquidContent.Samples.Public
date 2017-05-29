using System;

namespace LiquidContentSync
{
    public class AppArguments
    {
        public enum Actions
        {
            List = 0,
            Download = 1,
            Upload = 2,
            Sync = 3
        }

        public Actions Action { get; set; }
        public string Arguments { get; set; }

        public static AppArguments ParseArguments(string[] args)
        {
            var result = new AppArguments();
            if (args.Length == 0)
            {
                return null;
            }

            if (!Enum.TryParse(args[0], true, out Actions action))
            {
                return null;
            }
            result.Action = action;
            result.Arguments = args.Length > 1 ? args[1] : "";

            return result;
        }
    }
}
