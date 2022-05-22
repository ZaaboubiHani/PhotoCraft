using System.Collections.Generic;

namespace PhotoCraft.Models
{
    public class CropperModel : BaseModel
    {
        public CropperModel()
        {
            this.Stacks = new List<BaseModel.Pair>();
        }
    }
}