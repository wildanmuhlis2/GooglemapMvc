namespace Jmelosegui.Mvc.GoogleMap.Drawing
{    
    public struct Size
    {           
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Width == size2.Width && size1.Height == size2.Height;
        }
                
        public static bool operator !=(Size size1, Size size2)
        {
            return size1.Width != size2.Width || size1.Height != size2.Height;
        }
                
        public int Width
        {
            get; set;
        }
                
        public int Height
        {
            get; set;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            Size comp = (Size)obj;
                        
            return (comp.Width == this.Width) &&
                   (comp.Height == this.Height);
        }

        public override int GetHashCode()
        {
            return unchecked ( Width ^ Height );
        }
    }
}
