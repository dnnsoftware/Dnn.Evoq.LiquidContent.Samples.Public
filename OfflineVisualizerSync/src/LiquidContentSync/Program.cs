using System;
using System.IO;
using System.Net.Http;
using LiquidContentSync.Api;

namespace LiquidContentSync
{
    class Program
    {
        private const string VisualizerBasePath = ".";

        static void ListVisualizers(bool includeBuiltInVisualizers = false)
        {
            var visualizers = LiquidContentApi.GetVisualizers(includeBuiltInVisualizers);
            Console.WriteLine("\nVisualizer Id                                          Name");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            if (visualizers?.documents != null)
            {
                foreach (var visualizer in visualizers.documents)
                {
                    Console.WriteLine($"{visualizer.id}                   {visualizer.name}");
                }
            }
        }

        static void DownloadVisualizer(string vizId)
        {
            if (string.IsNullOrEmpty(vizId.Trim()))
            {
                PrintUsage();
                return;
            }
            Console.Write("Downloading visualizer...");
            var visualizer = LiquidContentApi.GetVisualizer(vizId);

            Console.Write("\nCreating folder...");
            var visualizerFolderPath = Path.Combine(VisualizerBasePath, visualizer.id);
            if (Directory.Exists(visualizerFolderPath))
            {
                Directory.Delete(visualizerFolderPath, true);
            }

            Console.Write("\nAdding visualizer files to folder...");
            Directory.CreateDirectory(visualizerFolderPath);
            File.WriteAllText(Path.Combine(visualizerFolderPath, "header.html"), !string.IsNullOrEmpty(visualizer.header.content) ? visualizer.header.content : "");
            File.WriteAllText(Path.Combine(visualizerFolderPath, "template.html"), !string.IsNullOrEmpty(visualizer.template.content) ? visualizer.template.content : "");
            File.WriteAllText(Path.Combine(visualizerFolderPath, "footer.html"), !string.IsNullOrEmpty(visualizer.footer.content) ? visualizer.footer.content : "");
            File.WriteAllText(Path.Combine(visualizerFolderPath, "styles.css"), visualizer.cssFiles != null && visualizer.cssFiles.Length > 0 ? visualizer.cssFiles[0].content : "");
            File.WriteAllText(Path.Combine(visualizerFolderPath, "scripts.js"), visualizer.scripts != null && visualizer.scripts.Length > 0 ? visualizer.scripts[0].content : "");

            Console.WriteLine("\nVisualizer downloaded successfully");
        }

        static string ReadFileContentsOrDefault(string filePath, string defaultContent = null)
        {
            var result = "";
            if (File.Exists(filePath))
            {
                result = File.ReadAllText(filePath);
            }

            return string.IsNullOrEmpty(result.Trim()) ? defaultContent : result;
        }
        static void UploadVisualizer(string vizId)
        {
            Console.Write("Getting visualizer metadata...");
            var visualizer = LiquidContentApi.GetVisualizer(vizId);
            var visualizerFolderPath = Path.Combine(VisualizerBasePath, visualizer.id);

            if (!Directory.Exists(visualizerFolderPath))
            {
                Console.WriteLine($"Cannot find the folder {vizId}");
            }

            Console.Write("\nUpdating HTML, CSS and Javascript files...");
            visualizer.header.content = ReadFileContentsOrDefault(Path.Combine(visualizerFolderPath, "header.html"), " ");
            visualizer.template.content = ReadFileContentsOrDefault(Path.Combine(visualizerFolderPath, "template.html"), " ");
            visualizer.footer.content = ReadFileContentsOrDefault(Path.Combine(visualizerFolderPath, "footer.html"), " ");
            var cssText = ReadFileContentsOrDefault(Path.Combine(visualizerFolderPath, "styles.css"));
            if (cssText != null)
            {
                visualizer.cssFiles = new Cssfile[1];
                visualizer.cssFiles[0] = new Cssfile
                {
                    content = cssText
                };
            }
            else
            {
                visualizer.cssFiles = null;
            }
            var jsText = ReadFileContentsOrDefault(Path.Combine(visualizerFolderPath, "scripts.js"));
            if (jsText != null)
            {
                visualizer.scripts = new Script[1];
                visualizer.scripts[0] = new Script
                {
                    content = jsText
                };
            }
            else
            {
                visualizer.scripts = null;
            }
            
            Console.Write("\nUploading visualizer...");
            LiquidContentApi.PutVisualizer(visualizer);

            Console.WriteLine("\nVisualizer uploaded successfully");
        }

        private static bool Syncing { get; set; }
        private static void OnFileChanged(object source, FileSystemEventArgs e)
        {
            ((FileSystemWatcher) source).EnableRaisingEvents = false;
            if (!Syncing)
            {
                Syncing = true;
                Console.WriteLine($"\n[{DateTime.Now:HH:mm:ss}] File: {e.Name} {e.ChangeType}");
                var vizId = Path.GetFileName(Path.GetDirectoryName(e.FullPath));
                UploadVisualizer(vizId);
                Syncing = false;
                ((FileSystemWatcher)source).EnableRaisingEvents = true;
            }
        }
        static void SyncVisualizer(string vizId)
        {
            DownloadVisualizer(vizId);
            var visualizerFolderPath = Path.Combine(VisualizerBasePath, vizId);
            var watcher = new FileSystemWatcher
            {
                Path = visualizerFolderPath,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.*"
            };
            watcher.Changed += OnFileChanged;
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("\nPress Ctrl-C to end monitoring\n");
            while (true)
            {
                System.Threading.Thread.Sleep(200);
            }
        }

        static void Main(string[] args)
        {
            var appArguments = AppArguments.ParseArguments(args);
            if (appArguments == null)
            {
                PrintUsage();
                return;
            }

            try
            {
                switch (appArguments.Action)
                {
                    case AppArguments.Actions.List:
                        ListVisualizers(appArguments.Arguments.ToLower().Trim() == "all");
                        break;
                    case AppArguments.Actions.Download:
                        DownloadVisualizer(appArguments.Arguments);
                        break;
                    case AppArguments.Actions.Upload:
                        UploadVisualizer(appArguments.Arguments);
                        break;
                    case AppArguments.Actions.Sync:
                        SyncVisualizer(appArguments.Arguments);
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
        }

        static void PrintUsage()
        {
            Console.WriteLine("Liquid Content Synchronizer v1.0\n");
            Console.WriteLine("Usage: dotnet liquid.dll [list [all]]");
            Console.WriteLine("                         [download <vizid>]");
            Console.WriteLine("                         [upload <vizid>]");
            Console.WriteLine("                         [sync <vizid>]\n");
        }
    }
}