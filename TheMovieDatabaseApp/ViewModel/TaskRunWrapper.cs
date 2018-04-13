using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TheMovieDatabaseApp.ViewModel
{
    public class TaskRunWrapper<T> : INotifyPropertyChanged
    {
        private readonly Task<T> _task;
        public event PropertyChangedEventHandler PropertyChanged;

        public T Result => _task.Status == TaskStatus.RanToCompletion ? _task.Result : default(T);
        public bool IsCompleted => _task.IsCompleted;
        public bool NotCompleted => !_task.IsCompleted;
        public bool IsFaulted => _task.IsFaulted;
        public Exception Exception => _task.Exception;


        public TaskRunWrapper(Task<T> task)
        {
            _task = task;
            RunTask(task);
        }

        private async Task RunTask(Task task)
        {
            try
            {
                await task;
            }
            catch { }

            OnPropertyChanged(nameof(Result));
            OnPropertyChanged(nameof(IsCompleted));
            OnPropertyChanged(nameof(NotCompleted));
            if (task.IsFaulted)
            {
                OnPropertyChanged(nameof(IsFaulted));
                OnPropertyChanged(nameof(Exception));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}