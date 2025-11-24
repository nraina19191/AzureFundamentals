using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IVersionControl
    {
        void commit(string message);
        void push();
        void pull();
    }

    public class GitVersion : IVersionControl
    {
        public void commit(string message)
        {
            Console.WriteLine($"Git commit - {message}");
        }

        public void pull()
        {
            Console.WriteLine($"Git pull");
        }

        public void push()
        {
            Console.WriteLine($"Git push");
        }
    }

    public class SVNVersion : IVersionControl
    {
        public void commit(string message)
        {
            Console.WriteLine($"SVN commit - {message}");
        }

        public void pull()
        {
            Console.WriteLine($"SVN pull");
        }

        public void push()
        {
            Console.WriteLine($"SVN push");
        }
    }

    public class Repository
    {
        private readonly IVersionControl _versionControl;
        public Repository(IVersionControl versionControl)
        {
            this._versionControl = versionControl;
        }

        public void CommitData(string message) { 
            this._versionControl.commit(message);
        }
    }
}
