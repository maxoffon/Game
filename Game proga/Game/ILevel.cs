using System.Windows.Forms;

namespace Game
{
    interface ILevel
    {
        Menu MainForm { get; }
        void Initialize();
    }
}
