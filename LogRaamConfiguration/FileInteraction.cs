// Code written by Gabriel Mailhot, 10/08/2023.

#region

using System;
using System.IO;

#endregion

namespace LogRaamConfiguration
{
   public class FileInteraction : IFileTimeStamp
   {
      private static DateTime _timeStamp;
      private string _filePath;
      private FileSystemWatcher _watcher;

      public FileInteraction(string filePath)
      {
         if (File.Exists(filePath)) Register(filePath);
      }

      ~FileInteraction()
      {
         // Stop monitoring
         _watcher.EnableRaisingEvents = false;
      }

      public bool IsOptionFileUpdated()
      {
         if (_filePath == null) throw new NullReferenceException("_filePath is null.");
         var ts = new FileInfo(_filePath).LastWriteTime;

         if (ts <= _timeStamp) return false;

         _timeStamp = ts;

         return true;
      }

      #region private

      private static void OnFileChanged(object sender, FileSystemEventArgs e)
      {
         if (e.ChangeType == WatcherChangeTypes.Changed) _timeStamp = new FileInfo(e.FullPath).LastWriteTime;
      }

      private void Register(string filePath)
      {
         _filePath = filePath;
         _timeStamp = new FileInfo(filePath).LastWriteTime;

         _watcher = new FileSystemWatcher(Path.GetDirectoryName(filePath)) {
            NotifyFilter = 0,
            EnableRaisingEvents = false,
            Filter = null,
            IncludeSubdirectories = false,
            InternalBufferSize = 0,
            Site = null,
            SynchronizingObject = null
         };

         _watcher.Filter = Path.GetFileName(filePath);
         _watcher.NotifyFilter = NotifyFilters.LastWrite;

         // Attach the event handler for file changed
         _watcher.Changed += OnFileChanged;

         // Start monitoring
         _watcher.EnableRaisingEvents = true;
      }

      #endregion
   }
}