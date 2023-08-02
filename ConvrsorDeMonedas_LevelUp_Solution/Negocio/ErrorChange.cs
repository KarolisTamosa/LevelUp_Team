using System.ComponentModel;

namespace Negocio
{
    public class ErrorChange
    {
        private string myString;
        public int idError { get; set; } = 0;

        public string MyStringProperty
        {
            get { return myString; }
            set
            {
                if (myString != value)
                {
                    myString = value;
                    OnPropertyChanged("MyStringProperty");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void NuevoError(string err)
        {
            Controller.err.idError += 1;
            Controller.err.MyStringProperty = Controller.err.idError+": "+err;
        }
    }
}
