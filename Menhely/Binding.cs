using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Menhely
{
    public class Binding : INotifyPropertyChanged
    {
        protected void OnChange([CallerMemberName] string n = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(n));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
