using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using TheMovieDatabaseApp.ViewModel;

namespace TheMovieDatabaseApp.Tests.ViewModel
{
    public class TaskRunWrapperTests
    {
        [Test]
        public void RaskRunWrapper_WhenTaskFinished_NotifyResultChanged()
        {
            BlockIfPropertyChangedNotRaised(nameof(TaskRunWrapper<int>.Result));
        }

        [Test]
        public void RaskRunWrapper_WhenTaskFinished_NotifyIsCompletedChanged()
        {
            BlockIfPropertyChangedNotRaised(nameof(TaskRunWrapper<int>.IsCompleted));
        }

        [Test]
        public void RaskRunWrapper_WhenTaskFinished_NotifyNotCompletedChanged()
        {
            BlockIfPropertyChangedNotRaised(nameof(TaskRunWrapper<int>.NotCompleted));
        }

        [Test]
        public void RaskRunWrapper_WhenTaskFaulted_NotifyIsFaultedChanged()
        {
            BlockIfPropertyChangedNotRaised(nameof(TaskRunWrapper<int>.IsFaulted), true);
        }

        [Test]
        public void RaskRunWrapper_WhenTaskFaulted_NotifyExceptionChanged()
        {
            BlockIfPropertyChangedNotRaised(nameof(TaskRunWrapper<int>.Exception), true);
        }

        private static void BlockIfPropertyChangedNotRaised(string property, bool fault = false)
        {
            var tcs = new TaskCompletionSource<int>();
            var wrapper = new TaskRunWrapper<int>(tcs.Task);
            var waitHandle = new ManualResetEventSlim(false);
            wrapper.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == property)
                {
                    waitHandle.Set();
                }
            };
            if (fault)
            {
                tcs.SetException(new Exception());
            }
            else
            {
                tcs.SetResult(1);
            }

            waitHandle.Wait();
        }
    }
}