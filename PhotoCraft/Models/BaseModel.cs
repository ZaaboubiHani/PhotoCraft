using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PhotoCraft.Models
{


    public class BaseModel : INotifyPropertyChanged
    {
        public List<Pair> Stacks { get; set; }
        public string OutputName { get; set; }
        public string SaveLocation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Pair
        {
            public Pair(string name,Image image)
            {
                Name = name;
                Image = image;
            }

            public string Name { get; set; }
            public Image Image { get; set; }
        }
    }
}
