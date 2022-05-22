using System.Collections.Generic;

namespace PhotoCraft.Models
{
    public class SlicerModel : BaseModel
    {
        private Angle slicingAngle;
        public Angle SlicingAngle { 
            get { return slicingAngle; }
            set { slicingAngle = value;
                OnPropertyChanged(nameof(SlicingAngle));
            }
        }
        public int SlicesNumber { get; set; }

        public enum Angle
        {
            Vertical, Horizontal
        }

        public SlicerModel()
        {
            this.Stacks = new List<BaseModel.Pair>();
        }
    }
}
